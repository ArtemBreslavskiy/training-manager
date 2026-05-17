using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using TrainingManager.Services;

namespace TrainingManager.ViewModels
{
    public partial class SessionPageViewModel : ViewModelBase
    {
        private readonly TrainingProgramService _trainingProgramService;
        private readonly ExerciseService _exersiceService;
        private readonly WorkoutService _workoutService;

        public SessionPageViewModel(
            TrainingProgramService trainingProgramService,
            ExerciseService exersiceService,
            WorkoutService workoutService)
        {
            _trainingProgramService = trainingProgramService;
            _exersiceService = exersiceService;
            _workoutService = workoutService;
        }

        [ObservableProperty]
        private int selectedSessionId;
    }
}
