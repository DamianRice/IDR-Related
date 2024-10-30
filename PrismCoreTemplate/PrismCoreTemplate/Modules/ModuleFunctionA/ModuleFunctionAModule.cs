using ModuleFunctionA.ViewModels;
using ModuleFunctionA.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleFunctionA
{
    public class ModuleFunctionAModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewNew>();
        }
    }
}