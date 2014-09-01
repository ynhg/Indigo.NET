namespace Indigo.Modules
{
    public interface IMvcModuleService : IModuleService
    {
        Component GetComponent(string controllerName, string area);
    }
}