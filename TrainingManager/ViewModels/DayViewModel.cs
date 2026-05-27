using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
        private readonly TrainingProgramService _trainingProgramService;

        [ObservableProperty] private Day day;
        [ObservableProperty] private ObservableCollection<DayExercise> limitedDayExercises = new();
        [ObservableProperty] private string? inputDayName;

        public event Action<Day, string>? DayNameChanged;

        public DayViewModel(TrainingProgramService trainingProgramService)
        {
            _trainingProgramService = trainingProgramService;
        }

        public void LoadData(Day day, int maxExercises)
        {
            Day = day;
            InputDayName = day.Name;
            UpdateLimitedExercises(maxExercises);
        }

        public void UpdateLimitedExercises(int maxExercises)
        {
            var dayExercises = Day.DayExercises
            .OrderBy(de => de.OrderInDay)
            .Take(maxExercises)
            .ToList();

            LimitedDayExercises.Clear();
            foreach (var dayExercise in dayExercises)
            {
                LimitedDayExercises.Add(dayExercise);
            }
        }

        private async Task RenameDay()
        {
            if (Day != null)
            {
                Day.Name = InputDayName;
                await _trainingProgramService.UpdateDayAsync(Day);
            }
        }

        partial void OnInputDayNameChanged(string? value)
        {
            RenameDay();
            DayNameChanged?.Invoke(Day, value);
        }
    }
}

