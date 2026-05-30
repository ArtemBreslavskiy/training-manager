using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private readonly TrainingProgramService _trainingProgramService;
        private readonly ExerciseService _exerciseService;
        private readonly WorkoutService _workoutService;
        private readonly PagesUtils _pagesUtils;
        private int maxExercises = 5;

        [ObservableProperty] private TrainingProgramViewModel trainingProgramViewModel;
        [ObservableProperty] private ObservableCollection<DayViewModel> dayViewModels = new();
        [ObservableProperty] private ObservableCollection<DayExerciseViewModel> dayExerciseViewModels = new();

        [ObservableProperty] private int selectedProgramId;
        [ObservableProperty] private int selectedDayId;
        [ObservableProperty] private string? inputDayName;
        [ObservableProperty] private string? inputDaysCount;
        [ObservableProperty] private string? inputExerciseName;

        private readonly IServiceProvider _serviceProvider;
        public IAsyncRelayCommand UpdateTrainingProgramDaysCountAsyncCommand { get; }
        public IAsyncRelayCommand AddExerciseToDayAsyncCommand { get; }
        public IAsyncRelayCommand<DayExercise> RemoveExerciseFromDayAsyncCommand { get; }

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

            UpdateTrainingProgramDaysCountAsyncCommand = new AsyncRelayCommand(UpdateTrainingProgramDaysCountAsync);
            AddExerciseToDayAsyncCommand = new AsyncRelayCommand(AddExerciseToDayAsync);
            RemoveExerciseFromDayAsyncCommand = new AsyncRelayCommand<DayExercise>(RemoveExerciseFromDayAsync);
        }

        partial void OnSelectedProgramIdChanged(int value)
        {
            LoadProgramAsync();
        }

        partial void OnSelectedDayIdChanged(int value)
        {
            LoadDayExercisesViewModels();
        }

        partial void OnInputDayNameChanged(string value)
        {
            var dayViewModel = DayViewModels.FirstOrDefault(vm => vm.Day.Id == SelectedDayId);
            if (dayViewModel != null && dayViewModel.InputDayName != InputDayName)
            {
                dayViewModel.InputDayName = InputDayName;
            }
        }

        private async Task LoadProgramAsync()
        {
            DayViewModels.Clear();

            var program = await _trainingProgramService.GetProgramByIdAsync(SelectedProgramId);
            InputDaysCount = Convert.ToString(program.DaysCount);
            TrainingProgramViewModel = _serviceProvider.GetRequiredService<TrainingProgramViewModel>();
            TrainingProgramViewModel.LoadData(program);

            var sortedDays = program.ProgramDays.OrderBy(d => d.OrderInProgram).ToList();
            foreach (var day in program.ProgramDays.OrderBy(d => d.OrderInProgram))
            {
                var dayViewModel = _serviceProvider.GetRequiredService<DayViewModel>();
                dayViewModel.LoadData(day, maxExercises);

                dayViewModel.DayNameChanged += (changedDay, newName) =>
                {
                    if (changedDay.Id == SelectedDayId)
                        InputDayName = newName;
                };

                DayViewModels.Add(dayViewModel);
            }

            if (sortedDays.Any())
            {
                var firstDay = sortedDays.First();
                SelectedDayId = firstDay.Id;
                InputDayName = firstDay.Name;
            }
            else
            {
                SelectedDayId = 0;
                InputDayName = string.Empty;
            }

            LoadDayExercisesViewModels();
            foreach (var dayViewModel in DayViewModels)
                SubscribeDayViewModel(dayViewModel);
        }

        private void LoadDayExercisesViewModels()
        {
            DayExerciseViewModels.Clear();

            foreach (var dayExercise in TrainingProgramViewModel.TrainingProgram.ProgramDays.FirstOrDefault(d => d.Id == SelectedDayId).DayExercises)
            {
                var dayExerciseViewModel = _serviceProvider.GetRequiredService<DayExerciseViewModel>();
                dayExerciseViewModel.LoadData(dayExercise);

                dayExerciseViewModel.DayExerciseNameChanged += (changedDayExercise, newName) =>
                {
                    foreach (var dayViewModel in DayViewModels)
                    {
                        var DayExerciseViewModel = dayViewModel.LimitedDayExerciseViewModels.FirstOrDefault(vm => vm.DayExercise.Id == changedDayExercise.Id);
                        if (DayExerciseViewModel != null && DayExerciseViewModel.InputDayExerciseName != newName)
                            DayExerciseViewModel.InputDayExerciseName = newName;
                    }
                };

                DayExerciseViewModels.Add(dayExerciseViewModel);
            }
        }

        private void SubscribeDayViewModel(DayViewModel dayViewModel)
        {
            foreach (var exerciseViewModel in dayViewModel.LimitedDayExerciseViewModels)
            {
                exerciseViewModel.DayExerciseNameChanged -= OnExerciseNameChanged;
                exerciseViewModel.DayExerciseNameChanged += OnExerciseNameChanged;
            }
        }

        private void OnExerciseNameChanged(DayExercise changedDayExercise, string newName)
        {
            var dayExerciseViewModel = DayExerciseViewModels.FirstOrDefault(vm => vm.DayExercise.Id == changedDayExercise.Id);
            if (dayExerciseViewModel != null && dayExerciseViewModel.InputDayExerciseName != newName)
                dayExerciseViewModel.InputDayExerciseName = newName;
        }

        public async Task UpdateTrainingProgramDaysCountAsync()
        {
            if (int.TryParse(InputDaysCount, out int daysCount) && daysCount > 0)
            {
                var newProgram = await _trainingProgramService.GetProgramByIdAsync(SelectedProgramId);
                newProgram.DaysCount = daysCount;

                TrainingProgramViewModel.TrainingProgram = await _trainingProgramService.UpdateProgramAsync(newProgram);

                await LoadProgramAsync();
            }
        }

        [RelayCommand]
        public void SelectDay(Day day)
        {
            SelectedDayId = day.Id;
            InputDayName = day.Name;
        }

        public async Task AddExerciseToDayAsync()
        {
            if (InputExerciseName != null)
            {
                Exercise exercise = await _exerciseService.FindOrCreateExercise(InputExerciseName);
                InputExerciseName = null;
                var day = TrainingProgramViewModel.TrainingProgram.ProgramDays.FirstOrDefault(d => d.Id == SelectedDayId);
                int orderInDay = day.DayExercises.Any() ? day.DayExercises.Max(de => de.OrderInDay) + 1 : 1;

                var newDayExercise = await _exerciseService.AddExerciseToDayAsync(
                    day.Id,
                    exercise.Id,
                    orderInDay,
                    new List<PlannedWorkoutSet>()
                );

                day.DayExercises.Add(newDayExercise);
                LoadDayExercisesViewModels();

                var dayViewModel = DayViewModels.FirstOrDefault(d => d.Day.Id == day.Id);
                dayViewModel.UpdateLimitedDayExerciseViewModels(maxExercises);
                SubscribeDayViewModel(dayViewModel);
            }
        }

        public async Task RemoveExerciseFromDayAsync(DayExercise dayExercise)
        {
            await _exerciseService.RemoveExerciseFromDayAsync(dayExercise.Id);
            foreach (var day in TrainingProgramViewModel.TrainingProgram.ProgramDays)
            {
                var dayExerciseToRemove = day.DayExercises.FirstOrDefault(de => de.Id == dayExercise.Id);
                if (dayExerciseToRemove != null)
                {
                    day.DayExercises.Remove(dayExerciseToRemove);
                    break;
                }
            }

            LoadDayExercisesViewModels();

            var dayViewModel = DayViewModels.FirstOrDefault(d => d.Day.Id == dayExercise.DayId);
            dayViewModel.UpdateLimitedDayExerciseViewModels(maxExercises);
            SubscribeDayViewModel(dayViewModel);
        }

        [RelayCommand]
        public async Task AddEmptyPlainedSet(DayExercise dayExercise)
        {
            int orderInExercise = dayExercise.PlannedWorkoutSets.Count + 1;
            var plainedWorkoutSet = await _workoutService.AddPlainedWorkoutSetAsync(dayExercise.Id, orderInExercise);

            dayExercise.PlannedWorkoutSets.Add(plainedWorkoutSet);

            LoadDayExercisesViewModels();
        }

        [RelayCommand]
        public async Task RemovePlainedSet(PlannedWorkoutSet plannedWorkoutSet)
        {
            await _workoutService.DeletePlainedWorkoutSetAsync(plannedWorkoutSet.Id);
            foreach (var day in TrainingProgramViewModel.TrainingProgram.ProgramDays)
            {
                foreach (var dayExercise in day.DayExercises)
                {
                    var setToRemove = dayExercise.PlannedWorkoutSets.FirstOrDefault(p => p.Id == plannedWorkoutSet.Id);
                    if (setToRemove != null)
                    {
                        dayExercise.PlannedWorkoutSets.Remove(setToRemove);
                        goto done;
                    }
                }
            }
            done:

            LoadDayExercisesViewModels();
        }

        [RelayCommand]
        public void GoWelcomePage()
        {
            _pagesUtils.GoWelcomePage();
        }

        [RelayCommand]
        public async Task GoTodaySessionPage()
        {
            WorkoutSession session = await _workoutService.GetOrCreateWorkoutSessionAsync(SelectedDayId, DateTime.UtcNow.Date);
            _pagesUtils.GoSessionPage(session.Id);
        }
    }
}

