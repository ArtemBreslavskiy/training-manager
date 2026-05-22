using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManager.Models;

namespace TrainingManager.ViewModels
{
    public partial class DayViewModel : ObservableObject
    {
        [ObservableProperty] private Day day;
        [ObservableProperty] private ObservableCollection<DayExercise> limitedExercises = new();

        public DayViewModel(Day day, int maxExercises = 7)
        {
            Day = day;
            LimitedExercises = new ObservableCollection<DayExercise>(Day.DayExercises?
                .OrderBy(de => de.OrderInDay)
                .Take(maxExercises)
                .ToList() ?? new List<DayExercise>());
        }

        public void UpdateLimitedExercises(int maxExercises = 7)
        {
            LimitedExercises = new ObservableCollection<DayExercise>(Day.DayExercises?
                .OrderBy(de => de.OrderInDay)
                .Take(maxExercises)
                .ToList() ?? new List<DayExercise>());
        }
    }
}
