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
        [ObservableProperty] private ObservableCollection<PlainedWorkoutSetViewModel> plainedWorkoutSetViewModels = new();

        public DayExerciseViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void LoadData(DayExercise dayExercise)
        {
            this.dayExercise = dayExercise;
            PlainedWorkoutSetViewModels.Clear();

            foreach (var plainedWorkoutSet in this.dayExercise.PlainedWorkoutSets)
            {
                var plainedWorkoutSetViewModel = _serviceProvider.GetRequiredService<PlainedWorkoutSetViewModel>();
                plainedWorkoutSetViewModel.PlainedWorkoutSet = plainedWorkoutSet;

                PlainedWorkoutSetViewModels.Add(plainedWorkoutSetViewModel);
            }
        }
    }
}
