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

        public async void LoadData(WorkoutSet workoutSet)
        {
            WorkoutSet = workoutSet;

            InputRepsCount = workoutSet.RepsCount;
            InputWeight = workoutSet.Weight;

            var plainedWorkoutSet = await _workoutService.GetPlannedWorkoutSets(workoutSet.Id);
            var lastWorkoutSet = await _workoutService.GetLastWorkoutSetsForExercise(workoutSet.Id);

            if (plainedWorkoutSet != null)
            {
                if (plainedWorkoutSet.PlannedRepsCount != null)
                    PlainedRepsCount = plainedWorkoutSet.PlannedRepsCount;
                else
                    PlainedRepsCount = 0;

                if (plainedWorkoutSet.PlannedWeight != null)
                    PlainedWeight = plainedWorkoutSet.PlannedWeight;
                else
                    PlainedWeight = 0;
            }
            else
            {
                PlainedRepsCount = 0;
                PlainedWeight = 0;
            }

            if (lastWorkoutSet != null)
            {
                if (lastWorkoutSet.RepsCount != null)
                    LastRepsCount = lastWorkoutSet.RepsCount;
                else
                    LastRepsCount = 0;

                if (lastWorkoutSet.Weight != null)
                    LastWeight = lastWorkoutSet.Weight;
                else
                    LastWeight = 0;
            }
            else
            {
                LastRepsCount = 0;
                LastWeight = 0;
            }
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

