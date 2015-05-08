using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using WpfExperiments.Views;

namespace WpfExperiments.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IRegionManager _regionManager;

        private readonly IUnityContainer _unityContainer;

        public NavigationService(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            _regionManager = regionManager;
            _unityContainer = unityContainer;
        }

        public void NavigateTo(ViewType viewType)
        {
            // Refactor, automate view resolving to avoid this switch case thing.
            object view;
            switch (viewType)
            {
                case ViewType.FirstView:
                    view = _unityContainer.Resolve<FirstView>();
                    break;
                case ViewType.SecondView:
                    view = _unityContainer.Resolve<SecondView>();
                    break;
                default:
                    throw new Exception("Unknown view type!");
            }

            _regionManager.RegisterViewWithRegion("MainRegion", () => view);
            _regionManager.Regions["MainRegion"].Activate(view);
        }
    }

    public enum ViewType
    {
        FirstView,
        SecondView,
        ThirdView
    }
}
