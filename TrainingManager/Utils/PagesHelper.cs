using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManager.ViewModels;

namespace TrainingManager.Utils
{
    internal class PagesHelper
    {
        private MainWindowViewModel _mainWindowViewModel;

        public PagesHelper()
        {
            _mainWindowViewModel = new MainWindowViewModel();
        }

        public void GoWelcomePage()
        {
            _mainWindowViewModel.CurrentPage = new WelcomePageViewModel();
        }

        public void GoTrainingProgramPage(int programId)
        {
            TrainingProgramPageViewModel viewModel = new();
            viewModel.SelectedProgramId = programId;
            _mainWindowViewModel.CurrentPage = viewModel;
        }

        public void GoChartsPage(int exerciseId)
        {
            ChartsPageViewModel viewModel = new();
            viewModel.SelectedExerciseId = exerciseId;
            _mainWindowViewModel.CurrentPage = viewModel;
        }
    }
}
