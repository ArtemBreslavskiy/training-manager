using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManager.Models;
using TrainingManager.Services;

namespace TrainingManager.ViewModels
{
    public partial class WorkoutSetViewModel : ObservableObject
    {
        private readonly WorkoutService _workoutService;

        [ObservableProperty] private WorkoutSet workoutSet;
        [ObservableProperty] private int? plainedRepsCount;
        [ObservableProperty] private double? plainedWeight;
        [ObservableProperty] private int? lastRepsCount;
        [ObservableProperty] private double? lastWeight;
        [ObservableProperty] private int? inputRepsCount;
        [ObservableProperty] private double? inputWeight;

        public WorkoutSetViewModel(WorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        public async void LoadDataAsync(WorkoutSet workoutSet)
        {
            WorkoutSet = workoutSet;

            InputRepsCount = workoutSet.RepsCount;
            InputWeight = workoutSet.Weight;

            var plainedWorkoutSet = await _workoutService.GetPlannedWorkoutSets(workoutSet.Id);
            var lastWorkoutSet = await _workoutService.GetLastWorkoutSetsForExercise(workoutSet.Id);

            PlainedRepsCount = plainedWorkoutSet.PlannedRepsCount;
            PlainedWeight = plainedWorkoutSet.PlannedWeight;

            LastRepsCount = lastWorkoutSet.RepsCount;
            LastWeight = lastWorkoutSet.Weight;
        }

        private async Task UpdateWorkoutSetAsync()
        {
            WorkoutSet = await _workoutService.UpdateWorkoutSetAsync(
                WorkoutSet.Id,
                InputRepsCount,
                InputWeight
            );
        }

        partial void OnInputRepsCountChanged(int? value)
        {
            UpdateWorkoutSetAsync();
        }

        partial void OnInputWeightChanged(double? value)
        {
            UpdateWorkoutSetAsync();
        }
    }
}
