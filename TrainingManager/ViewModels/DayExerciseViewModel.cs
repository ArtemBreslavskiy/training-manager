using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManager.Models;
using TrainingManager.Services;

namespace TrainingManager.ViewModels
{
    public partial class DayExerciseViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ExerciseService _exerciseService;

        [ObservableProperty] private DayExercise dayExercise;
        [ObservableProperty] private ObservableCollection<PlannedWorkoutSetViewModel> plainedWorkoutSetViewModels = new();
        [ObservableProperty] private string? inputDayExerciseName;

        public event Action<DayExercise, string>? DayExerciseNameChanged;

        public DayExerciseViewModel(IServiceProvider serviceProvider, ExerciseService exerciseServise)
        {
            _serviceProvider = serviceProvider;
            _exerciseService = exerciseServise;
        }

        partial void OnInputDayExerciseNameChanged(string? value)
        {
            RenameDayExercise();
        }

        public void LoadData(DayExercise dayExercise)
        {
            DayExercise = dayExercise;
            InputDayExerciseName = dayExercise.Exercise.Name;
            PlainedWorkoutSetViewModels.Clear();

            foreach (var plainedWorkoutSet in DayExercise.PlannedWorkoutSets)
            {
                var plainedWorkoutSetViewModel = _serviceProvider.GetRequiredService<PlannedWorkoutSetViewModel>();
                plainedWorkoutSetViewModel.LoadData(plainedWorkoutSet);

                PlainedWorkoutSetViewModels.Add(plainedWorkoutSetViewModel);
            }
            PlainedWorkoutSetViewModels = new ObservableCollection<PlannedWorkoutSetViewModel>(PlainedWorkoutSetViewModels.OrderBy(vm => vm.PlainedWorkoutSet.OrderInExercise));
        }

        public async Task RenameDayExercise()
        {
            if (DayExercise != null)
            {
                DayExercise.Exercise.Name = InputDayExerciseName;
                await _exerciseService.UpdateExerciseAsync(DayExercise.Exercise);
                DayExerciseNameChanged?.Invoke(DayExercise, inputDayExerciseName);
            }
        }
    }
}
