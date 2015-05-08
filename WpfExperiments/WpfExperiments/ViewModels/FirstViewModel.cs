using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Windows;
using System.Windows.Input;
using WpfExperiments.Interceptors;

namespace WpfExperiments.ViewModels
{
    public class FirstViewModel : BindableBase
    {
        private string _name;

        private readonly Lazy<ICommand> _sendCommand;

        public FirstViewModel()
        {
            _sendCommand = new Lazy<ICommand>(() => new DelegateCommand(Send));
        }

        public string Name
        {
            get { return _name; }
            set 
            {
                if (_name != value)
                {
                    _name = value;
                    base.OnPropertyChanged(() => Name);
                    // Don't use SetProperty as it have bug related to interception
                }
            }
        }

        public ICommand SendCommand
        {
            get { return _sendCommand.Value; }
        }

        [ShitHappened]
        protected virtual void Send()
        {
            MessageBox.Show(Name);
            throw new Exception("Fuck you people!");
        }
    }
}
