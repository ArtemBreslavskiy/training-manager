using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManager.Models;
using TrainingManager.Services;

namespace TrainingManager.ViewModels
{
    public partial class PlannedWorkoutSetViewModel : ObservableObject
    {
        private readonly WorkoutService _workoutService;

        [ObservableProperty] private PlannedWorkoutSet plainedWorkoutSet;
        [ObservableProperty] private string? inputPlannedRepsCount;
        [ObservableProperty] private string? inputPlannedWeight;

        public PlannedWorkoutSetViewModel(WorkoutService WorkoutService)
        {
            _workoutService = WorkoutService;
        }

        public void LoadData(PlannedWorkoutSet plainedWorkoutSet)
        {
            PlainedWorkoutSet = plainedWorkoutSet;
            InputPlannedRepsCount = Convert.ToString(plainedWorkoutSet.PlannedRepsCount);
            InputPlannedWeight = Convert.ToString(plainedWorkoutSet.PlannedWeight);
        }

        private async Task UpdatePlainedWorkoutSetAsync()
        {
            if (!int.TryParse(InputPlannedRepsCount, out int plannedRepsCount) || plannedRepsCount <= 0)
            {
                plannedRepsCount = 0;
            }
            if (!double.TryParse(InputPlannedWeight, out double plannedWeigth) || plannedWeigth <= 0)
            {
                plannedWeigth = 0;
            }

            plainedWorkoutSet = await _workoutService.UpdatePlainedWorkoutSetAsync(
                plainedWorkoutSet.Id,
                plannedRepsCount,
                plannedWeigth
            );
        }

        partial void OnInputPlannedRepsCountChanged(string? value)
        {
            UpdatePlainedWorkoutSetAsync();
        }

        partial void OnInputPlannedWeightChanged(string? value)
        {
            UpdatePlainedWorkoutSetAsync();
        }
    }
}
