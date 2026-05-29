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
        [ObservableProperty] private string? inputRepsCount;
        [ObservableProperty] private string? inputWeight;

        public WorkoutSetViewModel(WorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        public async void LoadData(WorkoutSet workoutSet)
        {
            WorkoutSet = workoutSet;

            InputRepsCount = Convert.ToString(workoutSet.RepsCount);
            InputWeight = Convert.ToString(workoutSet.Weight);

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
            if (!int.TryParse(InputRepsCount, out int repsCount) || repsCount <= 0)
            {
                repsCount = 0;
            }
            if (!double.TryParse(InputWeight, out double weigth) || weigth <= 0)
            {
                weigth = 0;
            }

            WorkoutSet = await _workoutService.UpdateWorkoutSetAsync(
                WorkoutSet.Id,
                repsCount,
                weigth
            );
        }

        partial void OnInputRepsCountChanged(string? value)
        {
            UpdateWorkoutSetAsync();
        }

        partial void OnInputWeightChanged(string? value)
        {
            UpdateWorkoutSetAsync();
        }
    }
}

