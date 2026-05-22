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
    public partial class PlainedWorkoutSetViewModel : ObservableObject
    {
        private readonly WorkoutService _workoutService;

        [ObservableProperty] private PlainedWorkoutSet plainedWorkoutSet;
        [ObservableProperty] private int? inputPlannedRepsCount;
        [ObservableProperty] private double? inputPlannedWeight;

        public PlainedWorkoutSetViewModel(WorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        private async Task UpdatePlainedWorkoutSetAsync()
        {
            plainedWorkoutSet = await _workoutService.UpdatePlainedWorkoutSetAsync(
                plainedWorkoutSet.Id,
                InputPlannedRepsCount,
                InputPlannedWeight
            );
        }

        partial void OnInputPlannedRepsCountChanged(int? value)
        {
            UpdatePlainedWorkoutSetAsync();
        }

        partial void OnInputPlannedWeightChanged(double? value)
        {
            UpdatePlainedWorkoutSetAsync();
        }
    }
}
