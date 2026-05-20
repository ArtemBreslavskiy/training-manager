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
using TrainingManager.Utils;

namespace TrainingManager.ViewModels
{
    public partial class TrainingProgramPageViewModel : ViewModelBase
    {
        private readonly TrainingProgramService _trainingProgramService;
        private readonly ExerciseService _exerciseService;
        private readonly WorkoutService _workoutService;
        private readonly PagesUtils _pagesUtils;
        private TrainingProgram? _program;

        [ObservableProperty] private int selectedProgramId;
        [ObservableProperty] private int selectedDayOrder;
        [ObservableProperty] private int selectedExerciseOrder;
        [ObservableProperty] private int inputDaysCount;
        [ObservableProperty] private string inputExerciseName;
        [ObservableProperty] private int? inputPlannedSetsCount;
        [ObservableProperty] private int? inputPlannedRepsCount;
        [ObservableProperty] private double? inputPlannedWeigths;

        public TrainingProgramPageViewModel(
            TrainingProgramService trainingProgramService,
            ExerciseService exersiceService,
            WorkoutService workoutService,
            PagesUtils pagesUtils,
            int programId)
        {
            _trainingProgramService = trainingProgramService;
            _exerciseService = exersiceService;
            _workoutService = workoutService;
            _pagesUtils = pagesUtils;
            SelectedProgramId = programId;

            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            _program = await _trainingProgramService.GetProgramByIdAsync(SelectedProgramId);
        }

        private async Task<Exercise> FindOrCreateExercise(string name)
        {
            Exercise exersice = await _exerciseService.GetExerciseByNameAsync(name);
            if (exersice == null)
            {
                exersice = await _exerciseService.CreateExerciseAsync(name);
            }

            return exersice;
        }

        [RelayCommand]
        public async Task AddExerciseToDayAsync()
        {
            Exercise exercise = await FindOrCreateExercise(InputExerciseName);
            Day day = _program.ProgramDays.ElementAt(SelectedDayOrder);
            int orderInDay = day.DayExercises.Any() ? day.DayExercises.Max(de => de.OrderInDay) + 1 : 1;

            await _exerciseService.AddExerciseToDayAsync(
                day.Id,
                exercise.Id,
                orderInDay,
                InputPlannedSetsCount,
                InputPlannedRepsCount,
                InputPlannedWeigths
            );
        }

        [RelayCommand]
        public async Task RemoveExerciseFromDayAsync()
        {
            int dayExerciseId = _program.ProgramDays.ElementAt(SelectedDayOrder)
                .DayExercises.ElementAt(SelectedExerciseOrder).Id;

            await _exerciseService.RemoveExerciseFromDayAsync(dayExerciseId);
        }

        [RelayCommand]
        public async Task UpdateTrainingProgramDaysCountAsync()
        {
            var newProgram = await _trainingProgramService.GetProgramByIdAsync(SelectedProgramId);
            newProgram.DaysCount = InputDaysCount;

            _program = await _trainingProgramService.UpdateProgramAsync(newProgram);
        }

        [RelayCommand]
        public async Task UpdatePlannedParametersAsync()
        {
            var newProgram = await _trainingProgramService.GetProgramByIdAsync(SelectedProgramId);
            int dayExerciseId = newProgram.ProgramDays.ElementAt(SelectedDayOrder)
                .DayExercises.ElementAt(SelectedExerciseOrder).Id;

            await _exerciseService.UpdatePlannedParametersAsync(
                dayExerciseId,
                InputPlannedSetsCount,
                InputPlannedRepsCount,
                InputPlannedWeigths
            );
        }

        [RelayCommand]
        public async Task GoTodaySessionPage()
        {
            int dayId = _program.ProgramDays.ElementAt(SelectedDayOrder).Id;
            WorkoutSession session = await _workoutService.GetOrCreateSessionAsync(dayId, DateTime.Today);

            _pagesUtils.GoSessionPage(session.Id);
        }

        [RelayCommand]
        public async Task GoChartsPage()
        {
            int exerciseId = _program.ProgramDays.ElementAt(SelectedDayOrder).DayExercises.ElementAt(SelectedExerciseOrder).ExerciseId;
            _pagesUtils.GoChartsPage(exerciseId);
        }
    }
}
