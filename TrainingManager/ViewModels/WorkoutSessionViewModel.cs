using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrainingManager.Models;

namespace TrainingManager.ViewModels
{
    public partial class WorkoutSessionViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty] private WorkoutSession workoutSession;
        [ObservableProperty] private ObservableCollection<ExerciseSessionViewModel> exerciseSessionViewModels = new();

        public WorkoutSessionViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task LoadData(WorkoutSession workoutSession)
        {
            WorkoutSession = workoutSession;
            ExerciseSessionViewModels.Clear();

            var groups = workoutSession.WorkoutSets
                .GroupBy(ws => ws.DayExercisesId);

            foreach (var group in groups)
            {
                var exerciseSessionViewModel = _serviceProvider.GetRequiredService<ExerciseSessionViewModel>();
                exerciseSessionViewModel.LoadData(group.ToList());
                ExerciseSessionViewModels.Add(exerciseSessionViewModel);
            }
        }
    }
}
