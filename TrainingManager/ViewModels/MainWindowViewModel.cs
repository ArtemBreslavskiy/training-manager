using CommunityToolkit.Mvvm.ComponentModel;

namespace TrainingManager.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        public ViewModelBase currentPage;
    }
}
