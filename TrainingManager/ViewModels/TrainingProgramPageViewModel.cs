using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManager.Services;
using TrainingManager.Models;

namespace TrainingManager.ViewModels
{
    public partial class TrainingProgramPageViewModel : ViewModelBase
    {
        private readonly TrainingProgramService _trainingProgramService;
        private readonly ExerciseService _exersiceService;
        private readonly WorkoutService _workoutService;

        public TrainingProgramPageViewModel(
            TrainingProgramService trainingProgramService,
            ExerciseService exersiceService,
            WorkoutService workoutService)
        {
            _trainingProgramService = trainingProgramService;
            _exersiceService = exersiceService;
            _workoutService = workoutService;
        }

        public ObservableCollection<Day> days = new();

        [ObservableProperty]
        private int daysCount;

        public int SelectedProgramId { get; set; }
    }
}
