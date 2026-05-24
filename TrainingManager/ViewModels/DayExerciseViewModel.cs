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
    public partial class DayExerciseViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty] private DayExercise dayExercise;
        [ObservableProperty] private ObservableCollection<PlannedWorkoutSetViewModel> plainedWorkoutSetViewModels = new();

        public DayExerciseViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void LoadData(DayExercise dayExercise)
        {
            DayExercise = dayExercise;
            PlainedWorkoutSetViewModels.Clear();

            foreach (var plainedWorkoutSet in DayExercise.PlannedWorkoutSets)
            {
                var plainedWorkoutSetViewModel = _serviceProvider.GetRequiredService<PlannedWorkoutSetViewModel>();
                plainedWorkoutSetViewModel.LoadData(plainedWorkoutSet);

                PlainedWorkoutSetViewModels.Add(plainedWorkoutSetViewModel);
            }
            PlainedWorkoutSetViewModels = new ObservableCollection<PlannedWorkoutSetViewModel>(PlainedWorkoutSetViewModels.OrderBy(vm => vm.PlainedWorkoutSet.OrderInExercise));
        }
    }
}
