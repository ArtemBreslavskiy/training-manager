using System;
using Microsoft.Extensions.DependencyInjection;
using TrainingManager.Models;
using TrainingManager.ViewModels;

namespace TrainingManager.Utils
{
    public class PagesUtils
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly IServiceProvider _serviceProvider;

        public PagesUtils(MainWindowViewModel mainWindowViewModel, IServiceProvider serviceProvider)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _serviceProvider = serviceProvider;
        }

        public void GoWelcomePage()
        {
            _mainWindowViewModel.CurrentPage = _serviceProvider.GetRequiredService<WelcomePageViewModel>();
        }

        public void GoTrainingProgramPage(int programId)
        {
            var viewModel = _serviceProvider.GetRequiredService<TrainingProgramPageViewModel>();
            viewModel.SelectedProgramId = programId;
            _mainWindowViewModel.CurrentPage = viewModel;
        }

        public void GoSessionPage(int sessionId)
        {
            var viewModel = _serviceProvider.GetRequiredService<SessionPageViewModel>();
            viewModel.SelectedSessionId = sessionId;
            _mainWindowViewModel.CurrentPage = viewModel;
        }

        public void GoChartsPage(int exerciseId)
        {
            var viewModel = _serviceProvider.GetRequiredService<ChartsPageViewModel>();
            viewModel.SelectedExerciseId = exerciseId;
            _mainWindowViewModel.CurrentPage = viewModel;
        }
    }
}

