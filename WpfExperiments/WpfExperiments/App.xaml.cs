using Microsoft.Practices.Unity;
using System.Windows;
using WpfExperiments.Services;
using WpfExperiments.Views;

namespace WpfExperiments
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IUnityContainer _container = new UnityContainer();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            UnityConfiguration.Configure(_container);
            var window = UnityConfiguration.GetWindow<MainWindow>(_container);
            var navigationService = _container.Resolve<INavigationService>();
            navigationService.NavigateTo(ViewType.FirstView);
            window.Show();
        }
    }
}
