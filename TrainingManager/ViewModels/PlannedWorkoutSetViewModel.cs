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
    public partial class PlannedWorkoutSetViewModel : ObservableObject
    {
        private readonly PlannedWorkoutSetService _plannedWorkoutSetService;

        [ObservableProperty] private PlannedWorkoutSet plainedWorkoutSet;
        [ObservableProperty] private int? inputPlannedRepsCount;
        [ObservableProperty] private double? inputPlannedWeight;

        public PlannedWorkoutSetViewModel(PlannedWorkoutSetService plannedWorkoutSetService)
        {
            _plannedWorkoutSetService = plannedWorkoutSetService;
        }

        public void LoadData(PlannedWorkoutSet plainedWorkoutSet)
        {
            PlainedWorkoutSet = plainedWorkoutSet;
            InputPlannedRepsCount = plainedWorkoutSet.PlannedRepsCount;
            InputPlannedWeight = plainedWorkoutSet.PlannedWeight;
        }

        private async Task UpdatePlainedWorkoutSetAsync()
        {
            plainedWorkoutSet = await _plannedWorkoutSetService.UpdatePlainedWorkoutSetAsync(
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
