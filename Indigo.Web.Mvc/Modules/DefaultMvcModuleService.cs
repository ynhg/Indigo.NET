using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Indigo.Infrastructure.Util;
using Indigo.Modules.Attributes;
using Spring.Stereotype;
using Spring.Transaction.Interceptor;
using ComponentAttribute = Indigo.Modules.Attributes.ComponentAttribute;

namespace Indigo.Modules
{
    [Service]
    public class DefaultMvcModuleService : BaseModuleService, IMvcModuleService
    {
        private const string DefaultModuleName = "default";
        private const string DefaultModuleTitle = "默认";
        private const string DefaultArea = "default";

        private readonly IDictionary<string, IDictionary<string, Component>> _componentDict =
            new Dictionary<string, IDictionary<string, Component>>();

        private readonly object _lockObject = new object();

        [Transaction]
        public override void BuildModules(params Assembly[] assemblies)
        {
            lock (_lockObject)
            {
                Module defaultModule = GetModule(DefaultModuleName);
                if (defaultModule == null)
                {
                    defaultModule = new Module
                    {
                        Name = DefaultModuleName,
                        Title = DefaultModuleTitle
                    };

                    ModuleDao.Save(defaultModule);
                }

                _componentDict.Clear();

                IList<Component> allComponents = ComponentDao.FindAll();

                foreach (Assembly assembly in assemblies)
                {
                    Type[] allTypes = assembly.GetTypes();

                    IDictionary<string, string> namespaceAreas = new Dictionary<string, string>();
                    IEnumerable<Type> areaRegistrationTypes =
                        allTypes.Where(t => t.IsSubclassOf(typeof (AreaRegistration)));
                    foreach (Type areaRegistrationType in areaRegistrationTypes)
                    {
                        string ns = areaRegistrationType.Namespace;
                        string area = ((AreaRegistration) Activator.CreateInstance(areaRegistrationType)).AreaName;

                        namespaceAreas.Add(ns, area);
                    }

                    IEnumerable<Type> controllerTypes =
                        allTypes.Where(t => t.IsSubclassOf(typeof (Controller)) && !t.IsAbstract);
                    foreach (Type controllerType in controllerTypes)
                    {
                        ControllerDescriptor controllerDescriptor = new ReflectedControllerDescriptor(controllerType);
                        string typeName = controllerType.FullName;
                        Component component = allComponents.SingleOrDefault(c => c.Name == typeName);
                        if (component == null)
                        {
                            component = new Component
                            {
                                Name = typeName
                            };
                            defaultModule.AddComponent(component);

                            ComponentDao.Save(component);

                            allComponents.Add(component);
                        }

                        string area =
                            StringUtils.ToLowerCase(
                                (from nsa in namespaceAreas where typeName.StartsWith(nsa.Key) select nsa.Value)
                                    .SingleOrDefault()) ?? DefaultArea;
                        if (!_componentDict.ContainsKey(area))
                            _componentDict.Add(area, new Dictionary<string, Component>());

                        _componentDict[area].Add(StringUtils.ToLowerCase(controllerDescriptor.ControllerName), component);

                        bool componentProtect = false;

                        if (controllerDescriptor.IsDefined(typeof (ComponentAttribute), false))
                        {
                            var componentAttribute =
                                (ComponentAttribute)
                                    controllerDescriptor.GetCustomAttributes(typeof (ComponentAttribute), false)[0];
                            component.Title = componentAttribute.Title;
                            component.Description = componentAttribute.Description;
                            component.Ordinal = componentAttribute.Ordinal;
                            componentProtect = componentAttribute.Protect;
                        }

                        int defaultOrdinal = 1;

                        foreach (ActionDescriptor actionDescriptor in controllerDescriptor.GetCanonicalActions())
                        {
                            string functionName = actionDescriptor.ActionName;
                            if (actionDescriptor.IsDefined(typeof (ActionNameAttribute), false))
                            {
                                var actionNameAttribute =
                                    (ActionNameAttribute)
                                        actionDescriptor.GetCustomAttributes(typeof (ActionNameAttribute), false)[0];
                                functionName = actionNameAttribute.Name;
                            }

                            Function function = component.GetFunction(functionName);
                            if (function == null)
                            {
                                function = new Function
                                {
                                    Name = functionName
                                };

                                component.AddFunction(function);
                            }

                            if (actionDescriptor.IsDefined(typeof (FunctionAttribute), false))
                            {
                                var functionAttribute =
                                    (FunctionAttribute)
                                        actionDescriptor.GetCustomAttributes(typeof (FunctionAttribute), false)[0];

                                function.Protect = functionAttribute.Protect.HasValue
                                    ? functionAttribute.Protect.Value
                                    : componentProtect;

                                if (functionAttribute.Title != null)
                                    function.Title = functionAttribute.Title;

                                if (functionAttribute.Description != null)
                                    function.Description = functionAttribute.Description;

                                function.Ordinal = functionAttribute.Ordinal > 0
                                    ? functionAttribute.Ordinal
                                    : defaultOrdinal++;
                            }
                        }
                    }
                }
            }
        }

        [Transaction(ReadOnly = true)]
        public Component GetComponent(string controllerName, string area)
        {
            lock (_lockObject)
            {
                area = StringUtils.ToLowerCase(area) ?? DefaultArea;
                if (_componentDict.ContainsKey(area))
                {
                    IDictionary<string, Component> subComponentDict = _componentDict[area];
                    controllerName = StringUtils.ToLowerCase(controllerName);

                    if (subComponentDict.ContainsKey(controllerName))
                        return subComponentDict[controllerName];
                }

                return null;
            }
        }
    }
}