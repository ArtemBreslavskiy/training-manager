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
        private readonly TrainingProgramService _trainingProgramService = new();
        private readonly ExerciseService _exersiceService = new();
        private readonly WorkoutService _workoutService = new();
        public ObservableCollection<Day> days = new();

        [ObservableProperty]
        private int daysCount;

        [ObservableProperty]
        private int daysCount;

        public int SelectedProgramId { get; set; }
    }
}
