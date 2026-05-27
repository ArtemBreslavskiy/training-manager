using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManager.Models;
using TrainingManager.Services;
using TrainingManager.Utils;

namespace TrainingManager.ViewModels
{
    public partial class SessionPageViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TrainingProgramService _trainingProgramService;
        private readonly ExerciseService _exersiceService;
        private readonly WorkoutService _workoutService;
        private readonly PagesUtils _pagesUtils;

        [ObservableProperty] private WorkoutSessionViewModel workoutSessionViewModel;
        [ObservableProperty] private string dayName;
        [ObservableProperty] private int selectedSessionId;

        public SessionPageViewModel(
            IServiceProvider serviceProvider,
            TrainingProgramService trainingProgramService,
            ExerciseService exersiceService,
            WorkoutService workoutService,
            PagesUtils pagesUtils)
        {
            _serviceProvider = serviceProvider;
            _trainingProgramService = trainingProgramService;
            _exersiceService = exersiceService;
            _workoutService = workoutService;
            _pagesUtils = pagesUtils;
        }

        partial void OnSelectedSessionIdChanged(int value)
        {
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var workoutSession = await _workoutService.GetWorkoutSessionByIdAsync(SelectedSessionId);
            WorkoutSessionViewModel = _serviceProvider.GetRequiredService<WorkoutSessionViewModel>();
            WorkoutSessionViewModel.LoadData(workoutSession);

            DayName = workoutSession.Day.Name;
        }

        [RelayCommand]
        public void GoWelcomePage()
        {
            _pagesUtils.GoWelcomePage();
        }
    }
}

