using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
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
        private readonly TrainingProgramService _trainingProgramService;
        private readonly ExerciseService _exersiceService;
        private readonly WorkoutService _workoutService;
        private readonly PagesUtils _pagesHelper;
        private TrainingProgram _program;

        [ObservableProperty]
        private int selectedSessionId;

        [ObservableProperty]
        private int selectedSetId;

        [ObservableProperty]
        private int inputRepsCount;

        [ObservableProperty]
        private double inputWeight;

        public SessionPageViewModel(
            TrainingProgramService trainingProgramService,
            ExerciseService exersiceService,
            WorkoutService workoutService)
        {
            _trainingProgramService = trainingProgramService;
            _exersiceService = exersiceService;
            _workoutService = workoutService;
        }

        private async Task LoadDataAsync()
        {
            WorkoutSession session  = await _workoutService.GetSessionByIdAsync(selectedSessionId);
            int dayId = session.DayId;
        }
    }
}
