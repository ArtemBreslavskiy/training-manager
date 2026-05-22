using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
using TrainingManager.Utils;

namespace TrainingManager.ViewModels
{
    public partial class TrainingProgramPageViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TrainingProgramService _trainingProgramService;
        private readonly ExerciseService _exerciseService;
        private readonly WorkoutService _workoutService;
        private readonly PagesUtils _pagesUtils;
        private TrainingProgram? _program;
        private int maxExercises = 5;

        public IAsyncRelayCommand AddExerciseToDayAsyncCommand { get; }
        public IAsyncRelayCommand<DayExerciseViewModel> AddEmptyPlainedSetCommand { get; }

        [ObservableProperty] private ObservableCollection<DayViewModel> dayViewModels = new();
        [ObservableProperty] private ObservableCollection<DayExerciseViewModel> dayExerciseViewModels = new();

        [ObservableProperty] private int selectedProgramId;
        [ObservableProperty] private string selectedProgramName;
        [ObservableProperty] private int selectedDayId;
        [ObservableProperty] private string selectedDayName;
        [ObservableProperty] private int inputDaysCount;
        [ObservableProperty] private string inputExerciseName;

        public TrainingProgramPageViewModel(
            IServiceProvider serviceProvider,
            TrainingProgramService trainingProgramService,
            ExerciseService exersiceService,
            WorkoutService workoutService,
            PagesUtils pagesUtils)
        {
            _serviceProvider = serviceProvider;
            _trainingProgramService = trainingProgramService;
            _exerciseService = exersiceService;
            _workoutService = workoutService;
            _pagesUtils = pagesUtils;

            AddExerciseToDayAsyncCommand = new AsyncRelayCommand(AddExerciseToDayAsync);
            AddEmptyPlainedSetCommand = new AsyncRelayCommand<DayExerciseViewModel>(AddEmptyPlainedSet);
        }

        partial void OnSelectedProgramIdChanged(int value)
        {
            LoadProgramAsync();
        }

        partial void OnSelectedDayIdChanged(int value)
        {
            LoadDayExercisesViewModels();
        }

        private async Task LoadProgramAsync()
        {
            DayViewModels.Clear();

            _program = await _trainingProgramService.GetProgramByIdAsync(SelectedProgramId);
            SelectedProgramName = _program.Name;

            var sortedDays = _program.ProgramDays.OrderBy(d => d.OrderInProgram).ToList();
            foreach (var day in _program.ProgramDays.OrderBy(d => d.OrderInProgram))
            {
                DayViewModels.Add(new DayViewModel(day, maxExercises));
            }

            if (sortedDays.Any())
            {
                var firstDay = sortedDays.First();
                SelectedDayId = firstDay.Id;
                SelectedDayName = firstDay.Name;
            }
            else
            {
                SelectedDayId = 0;
                SelectedDayName = string.Empty;
            }

            LoadDayExercisesViewModels();
        }

        private void LoadDayExercisesViewModels()
        {
            DayExerciseViewModels.Clear();

            foreach (var dayExercise in _program.ProgramDays.FirstOrDefault(d => d.Id == SelectedDayId).DayExercises)
            {
                var dayExerciseViewModel = _serviceProvider.GetRequiredService<DayExerciseViewModel>();
                dayExerciseViewModel.LoadData(dayExercise);
                DayExerciseViewModels.Add(dayExerciseViewModel);
            }
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
        public async Task UpdateTrainingProgramDaysCountAsync()
        {
            var newProgram = await _trainingProgramService.GetProgramByIdAsync(SelectedProgramId);
            newProgram.DaysCount = InputDaysCount;

            _program = await _trainingProgramService.UpdateProgramAsync(newProgram);
        }

        [RelayCommand]
        public void SelectDay(Day day)
        {
            SelectedDayId = day.Id;
            SelectedDayName = day.Name;
        }

        public async Task AddExerciseToDayAsync()
        {
            Exercise exercise = await FindOrCreateExercise(InputExerciseName);
            var day = _program.ProgramDays.FirstOrDefault(d => d.Id == SelectedDayId);
            int orderInDay = day.DayExercises.Any() ? day.DayExercises.Max(de => de.OrderInDay) + 1 : 1;

            var newDayExercise = await _exerciseService.AddExerciseToDayAsync(
                day.Id,
                exercise.Id,
                orderInDay,
                new List<PlainedWorkoutSet>()
            );

            day.DayExercises.Add(newDayExercise);
            LoadDayExercisesViewModels();
        }

        [RelayCommand]
        public async Task RemoveExerciseFromDayAsync(DayExerciseViewModel dayExerciseViewModel)
        {
            await _exerciseService.RemoveExerciseFromDayAsync(dayExerciseViewModel.DayExercise.Id);
        }

        public async Task AddEmptyPlainedSet(DayExerciseViewModel dayExerciseViewModel)
        {
            var plainedWorkoutSet = await _workoutService.AddPlainedWorkoutSetAsync(dayExerciseViewModel.DayExercise.Id);
            var day = _program.ProgramDays.FirstOrDefault(d => d.Id == SelectedDayId);
            var dayExercise = day?.DayExercises.FirstOrDefault(de => de.Id == dayExerciseViewModel.DayExercise.Id);

            dayExercise.PlainedWorkoutSets.Add(plainedWorkoutSet);

            LoadDayExercisesViewModels();
        }

        [RelayCommand]
        public async Task GoTodaySessionPage()
        {
            WorkoutSession session = await _workoutService.GetOrCreateSessionAsync(SelectedDayId, DateTime.Today);

            _pagesUtils.GoSessionPage(session.Id);
        }
    }
}
