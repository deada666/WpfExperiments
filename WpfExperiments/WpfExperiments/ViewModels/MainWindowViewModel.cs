using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Windows;
using System.Windows.Input;
using WpfExperiments.Events;
using WpfExperiments.Interceptors;
using WpfExperiments.Services;

namespace WpfExperiments.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        private readonly INavigationService _navigationService;

        private readonly Lazy<ICommand> _goToFirstViewCommand;

        private readonly Lazy<ICommand> _goToSecondViewCommand;

        private readonly Lazy<ICommand> _goToThirdViewCommand;

        public MainWindowViewModel(IEventAggregator eventAggregator, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _goToFirstViewCommand = new Lazy<ICommand>(() => new DelegateCommand(() => NavigateTo(ViewType.FirstView)));
            _goToSecondViewCommand = new Lazy<ICommand>(() => new DelegateCommand(() => NavigateTo(ViewType.SecondView)));
            _goToThirdViewCommand = new Lazy<ICommand>(() => new DelegateCommand(() => NavigateTo(ViewType.ThirdView)));
            _eventAggregator.GetEvent<ShitHappenedEvent>()
                .Subscribe(x => MessageBox.Show(x.StackTrace, x.Message), ThreadOption.UIThread);
        }

        public ICommand GoToFirstViewCommand
        {
            get { return _goToFirstViewCommand.Value; }
        }

        public ICommand GoToSecondViewCommand
        {
            get { return _goToSecondViewCommand.Value; }
        }

        public ICommand GoToThirdViewCommand
        {
            get { return _goToThirdViewCommand.Value; }
        }

        [ShitHappened]
        protected virtual void NavigateTo(ViewType view)
        {
            _navigationService.NavigateTo(view);
        }
    }
}
