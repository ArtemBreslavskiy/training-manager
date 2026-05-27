using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManager.Models;

namespace TrainingManager.ViewModels
{
    public partial class ExerciseSessionViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty] private ObservableCollection<WorkoutSetViewModel> workoutSetViewModels = new();
        [ObservableProperty] private string exerciseName;

        public ExerciseSessionViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void LoadData(List<WorkoutSet> workoutSets)
        {
            WorkoutSetViewModels.Clear();
            ExerciseName = workoutSets.FirstOrDefault().DayExercise.Exercise.Name;

            foreach (var workoutSet in workoutSets)
            {
                var workoutSetViewModel = _serviceProvider.GetRequiredService<WorkoutSetViewModel>();
                workoutSetViewModel.LoadData(workoutSet);
                WorkoutSetViewModels.Add(workoutSetViewModel);
            }

            WorkoutSetViewModels = new ObservableCollection<WorkoutSetViewModel>(WorkoutSetViewModels.OrderBy(vm => vm.WorkoutSet.OrderInExercise));
        }
    }
}
