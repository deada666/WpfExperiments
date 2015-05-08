using WpfExperiments.ViewModels;

namespace WpfExperiments.Views
{
    /// <summary>
    /// Interaction logic for FirstView.xaml
    /// </summary>
    public partial class FirstView
    {
        public FirstView(FirstViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
