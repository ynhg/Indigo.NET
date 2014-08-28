using Indigo.Infrastructure.Util;
using Indigo.Modules.Attributes;
using Spring.Transaction.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Indigo.Modules
{
    [Spring.Stereotype.Service]
    public class DefaultMvcModuleService : BaseModuleService, IMvcModuleService
    {
        private const string DEFAULT_MODULE_NAME = "default";
        private const string DEFAULT_MODULE_TITLE = "默认";
        private const string DEFAULT_AREA = "default";

        private readonly object lockObject = new object();
        private IDictionary<string, IDictionary<string, Component>> componentDict = new Dictionary<string, IDictionary<string, Component>>();

        [Transaction]
        public override void BuildModules(params Assembly[] assemblies)
        {
            lock (lockObject)
            {
                Module defaultModule = GetModule(DEFAULT_MODULE_NAME);
                if (defaultModule == null)
                {
                    defaultModule = new Module();
                    defaultModule.Name = DEFAULT_MODULE_NAME;
                    defaultModule.Title = DEFAULT_MODULE_TITLE;

                    ModuleDao.Save(defaultModule);
                }

                componentDict.Clear();

                IList<Component> allComponents = ComponentDao.FindAll();

                foreach (Assembly assembly in assemblies)
                {
                    Type[] allTypes = assembly.GetTypes();

                    IDictionary<string, string> namespaceAreas = new Dictionary<string, string>();
                    var areaRegistrationTypes = allTypes.Where(t => t.IsSubclassOf(typeof(AreaRegistration)));
                    foreach (Type areaRegistrationType in areaRegistrationTypes)
                    {
                        string ns = areaRegistrationType.Namespace;
                        string area = ((AreaRegistration)Activator.CreateInstance(areaRegistrationType)).AreaName;

                        namespaceAreas.Add(ns, area);
                    }

                    var controllerTypes = allTypes.Where(t => t.IsSubclassOf(typeof(Controller)) && !t.IsAbstract);
                    foreach (Type controllerType in controllerTypes)
                    {
                        ControllerDescriptor controllerDescriptor = new ReflectedControllerDescriptor(controllerType);
                        string typeName = controllerType.FullName;
                        var component = allComponents.SingleOrDefault(c => c.Name == typeName);
                        if (component == null)
                        {
                            component = new Component();
                            component.Name = typeName;
                            defaultModule.AddComponent(component);

                            ComponentDao.Save(component);

                            allComponents.Add(component);
                        }

                        string area = StringUtils.ToLowerCase((from nsa in namespaceAreas where typeName.StartsWith(nsa.Key) select nsa.Value).SingleOrDefault()) ?? DEFAULT_AREA;
                        if (!componentDict.ContainsKey(area))
                            componentDict.Add(area, new Dictionary<string, Component>());

                        componentDict[area].Add(StringUtils.ToLowerCase(controllerDescriptor.ControllerName), component);

                        bool componentProtect = false;

                        if (controllerDescriptor.IsDefined(typeof(ComponentAttribute), false))
                        {
                            var componentAttribute = (ComponentAttribute)controllerDescriptor.GetCustomAttributes(typeof(ComponentAttribute), false)[0];
                            component.Title = componentAttribute.Title;
                            component.Description = componentAttribute.Description;
                            component.Ordinal = componentAttribute.Ordinal;
                            componentProtect = componentAttribute.Protect;
                        }

                        int defaultOrdinal = 1;

                        foreach (var actionDescriptor in controllerDescriptor.GetCanonicalActions())
                        {
                            string functionName = actionDescriptor.ActionName;
                            if (actionDescriptor.IsDefined(typeof(ActionNameAttribute), false))
                            {
                                var actionNameAttribute = (ActionNameAttribute)actionDescriptor.GetCustomAttributes(typeof(ActionNameAttribute), false)[0];
                                functionName = actionNameAttribute.Name;
                            }

                            var function = component.GetFunction(functionName);
                            if (function == null)
                            {
                                function = new Function();
                                function.Name = functionName;

                                component.AddFunction(function);
                            }

                            if (actionDescriptor.IsDefined(typeof(FunctionAttribute), false))
                            {
                                var functionAttribute = (FunctionAttribute)actionDescriptor.GetCustomAttributes(typeof(FunctionAttribute), false)[0];

                                function.Protect = functionAttribute.Protect.HasValue ? functionAttribute.Protect.Value : componentProtect;

                                if (functionAttribute.Title != null)
                                    function.Title = functionAttribute.Title;

                                if (functionAttribute.Description != null)
                                    function.Description = functionAttribute.Description;

                                if (functionAttribute.Ordinal > 0)
                                    function.Ordinal = functionAttribute.Ordinal;
                                else
                                    function.Ordinal = defaultOrdinal++;
                            }
                        }
                    }
                }
            }
        }

        [Transaction(ReadOnly = true)]
        public Component GetComponent(string controllerName, string area)
        {
            lock (lockObject)
            {
                area = StringUtils.ToLowerCase(area) ?? DEFAULT_AREA;
                if (componentDict.ContainsKey(area))
                {
                    var subComponentDict = componentDict[area];
                    controllerName = StringUtils.ToLowerCase(controllerName);

                    if (subComponentDict.ContainsKey(controllerName))
                        return subComponentDict[controllerName];
                }

                return null;
            }
        }
    }
}
