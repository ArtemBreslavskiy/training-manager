using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManager.Services;
using TrainingManager.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TrainingManager.ViewModels
{
    public partial class TrainingProgramPageViewModel : ViewModelBase
    {
        private readonly TrainingProgramService _trainingProgramService;
        private readonly ExerciseService _exersiceService;
        private readonly WorkoutService _workoutService;
        private TrainingProgram _program;

        [ObservableProperty]
        private int selectedProgramId;

        [ObservableProperty]
        private int selectedDayOrder;

        [ObservableProperty]
        private int selectedExerciseOrder;

        [ObservableProperty]
        private int inputDaysCount;

        [ObservableProperty]
        private string inputExerciseName;

        [ObservableProperty]
        private int? inputPlannedSetsCount;

        [ObservableProperty]
        private int? inputPlannedRepsCount;

        [ObservableProperty]
        private double? inputPlannedWeigths;

        public TrainingProgramPageViewModel(
            TrainingProgramService trainingProgramService,
            ExerciseService exersiceService,
            WorkoutService workoutService)
        {
            _trainingProgramService = trainingProgramService;
            _exersiceService = exersiceService;
            _workoutService = workoutService;

            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            _program = await _trainingProgramService.GetProgramByIdAsync(SelectedProgramId);
        }

        [RelayCommand]
        public async void UpdateTrainingProgramDaysCountAsync()
        {
            TrainingProgram newTrainingProgram = _program;
            newTrainingProgram.DaysCount = InputDaysCount;

            _program = await _trainingProgramService.UpdateProgramAsync(newTrainingProgram);
        }

        private async Task<Exercise> FindOrCreateExercise(string name)
        {
            List<Exercise> allExercises = await _exersiceService.GetAllExercisesAsync();
            Exercise exersice = allExercises.FirstOrDefault(e => e.Name == name);
            if (exersice == null)
            {
                exersice = await _exersiceService.CreateExerciseAsync(name);
            }

            return exersice;
        }

        [RelayCommand]
        public async void AddExerciseToDayAsync()
        {
            Exercise exercise = await FindOrCreateExercise(InputExerciseName);
            Day day = _program.ProgramDays.ElementAt(SelectedDayOrder);
            int orderInDay = _program.ProgramDays.ElementAt(SelectedDayOrder).DayExercises.Max(de => de.OrderInDay) + 1;

            await _exersiceService.AddExerciseToDayAsync(
                day.Id,
                exercise.Id,
                orderInDay,
                InputPlannedSetsCount,
                InputPlannedRepsCount,
                InputPlannedWeigths
            );
        }

        [RelayCommand]
        public async void RemoveExersiceFromDayAsync()
        {
            int dayExerciseId = _program.ProgramDays.ElementAt(SelectedDayOrder).DayExercises.ElementAt(SelectedExerciseOrder).Id;
            await _exersiceService.RemoveExerciseFromDayAsync(dayExerciseId);
        }
    }
}
