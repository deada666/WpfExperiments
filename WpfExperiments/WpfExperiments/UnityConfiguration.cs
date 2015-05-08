using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using System;
using System.Linq;
using WpfExperiments.ViewModels;
using WpfExperiments.Views;
using WpfExperiments.Extensions;
using Microsoft.Practices.Unity.InterceptionExtension;
using WpfExperiments.Services;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Regions.Behaviors;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows;

namespace WpfExperiments
{
    internal static class UnityConfiguration
    {
        public static void Configure(IUnityContainer container)
        {
            container.AddNewExtension<Interception>();
            // Register event aggregator as singletone
            container.RegisterSingleton<IEventAggregator, EventAggregator>();
            container.RegisterSingleton<INavigationService, NavigationService>();

            // Register all view models
            var viewModelNamespace = typeof(MainWindowViewModel).Namespace;
            container.RegisterTypesWithInterceptors(AllClasses.FromLoadedAssemblies()
                .Where(x => x.Namespace.StartsWith(viewModelNamespace)),
                WithMappings.None,
                WithName.Default,
                WithLifetime.PerResolve);

            // Register all views
            var viewNamespace = typeof(MainWindow).Namespace;
            container.RegisterTypes(AllClasses.FromLoadedAssemblies()
                .Where(x => x.Namespace.StartsWith(viewNamespace)),
                WithMappings.None,
                WithName.Default,
                WithLifetime.PerResolve);

            configureRegionManager(container);
        }

        // This is sample how to setup region manager without Prism bootstrapper.
        // Referencing prism bootstrapper for unity encapsulates all this shit inside
        private static void configureRegionManager(IUnityContainer container)
        {
            container.RegisterSingleton<RegionAdapterMappings>();
            container.RegisterSingleton<IRegionManager, RegionManager>();;
            container.RegisterSingleton<IRegionViewRegistry, RegionViewRegistry>();
            container.RegisterSingleton<IRegionBehaviorFactory, RegionBehaviorFactory>();
            container.RegisterSingleton<IRegionNavigationContentLoader, RegionNavigationContentLoader>();
            container.RegisterSingleton<IServiceLocator, UnityServiceLocator>();

            // Set service provider for region manager
            ServiceLocator.SetLocatorProvider(() => container.Resolve<IServiceLocator>());

            var defaultRegionBehaviorTypesDictionary = container.Resolve<IRegionBehaviorFactory>();
            Action<string, Type> addIfMissing = defaultRegionBehaviorTypesDictionary.AddIfMissing;

            addIfMissing(AutoPopulateRegionBehavior.BehaviorKey, typeof(AutoPopulateRegionBehavior));
            addIfMissing(BindRegionContextToDependencyObjectBehavior.BehaviorKey, typeof(BindRegionContextToDependencyObjectBehavior));
            addIfMissing(RegionActiveAwareBehavior.BehaviorKey, typeof(RegionActiveAwareBehavior));
            addIfMissing(SyncRegionContextWithHostBehavior.BehaviorKey, typeof(SyncRegionContextWithHostBehavior));
            addIfMissing(RegionManagerRegistrationBehavior.BehaviorKey, typeof(RegionManagerRegistrationBehavior));

            var regionAdapterMappings = container.Resolve<RegionAdapterMappings>();
            regionAdapterMappings.RegisterMapping(typeof(Selector), container.Resolve<SelectorRegionAdapter>());
            regionAdapterMappings.RegisterMapping(typeof(ItemsControl), container.Resolve<ItemsControlRegionAdapter>());
            regionAdapterMappings.RegisterMapping(typeof(ContentControl), container.Resolve<ContentControlRegionAdapter>());
        }

        public static T GetWindow<T>(IUnityContainer container)
            where T : Window
        {
            var window = container.Resolve<T>();
            RegionManager.SetRegionManager(window, container.Resolve<IRegionManager>());
            RegionManager.UpdateRegions();
            return window;
        }
    }
}
