using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
    public partial class DayViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TrainingProgramService _trainingProgramService;

        [ObservableProperty] private Day day;
        [ObservableProperty] private ObservableCollection<DayExerciseViewModel> limitedDayExerciseViewModels = new();
        [ObservableProperty] private string? inputDayName;

        public event Action<Day, string>? DayNameChanged;

        public DayViewModel(IServiceProvider serviceProvider, TrainingProgramService trainingProgramService)
        {
            _serviceProvider = serviceProvider;
            _trainingProgramService = trainingProgramService;
        }

        partial void OnInputDayNameChanged(string? value)
        {
            RenameDay();
        }

        public void LoadData(Day day, int maxExercises)
        {
            Day = day;
            InputDayName = day.Name;
            UpdateLimitedDayExerciseViewModels(maxExercises);
        }

        public void UpdateLimitedDayExerciseViewModels(int maxExercises)
        {
            var dayExercises = Day.DayExercises
            .OrderBy(de => de.OrderInDay)
            .Take(maxExercises)
            .ToList();

            LimitedDayExerciseViewModels.Clear();
            foreach (var dayExercise in dayExercises)
            {
                var dayExerciseViewModel = _serviceProvider.GetRequiredService<DayExerciseViewModel>();
                dayExerciseViewModel.LoadData(dayExercise);
                LimitedDayExerciseViewModels.Add(dayExerciseViewModel);
            }
        }

        private async Task RenameDay()
        {
            if (Day != null)
            {
                Day.Name = InputDayName;
                await _trainingProgramService.UpdateDayAsync(Day);
                DayNameChanged?.Invoke(Day, InputDayName);
            }
        }
    }
}

