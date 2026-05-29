using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;
using TrainingManager.Models;
using TrainingManager.Services;
using TrainingManager.Utils;

namespace TrainingManager.ViewModels
{
    public partial class WelcomePageViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TrainingProgramService _trainingProgramService;
        private readonly WorkoutService _workoutService;
        private readonly PagesUtils _pagesHelper;
        private List<TrainingProgramViewModel> _allTrainingProgramsViewModels = new();

        [ObservableProperty] private ObservableCollection<TrainingProgramViewModel> trainingProgramViewModels = new();
        [ObservableProperty] private string? searchedName;
        [ObservableProperty] private string? inputName;
        [ObservableProperty] private string? inputDaysCount;

        public WelcomePageViewModel(
            TrainingProgramService trainingProgramService,
            WorkoutService workoutService,
            PagesUtils pagesHelper,
            IServiceProvider serviceProvider)
        {
            _trainingProgramService = trainingProgramService;
            _workoutService = workoutService;
            _pagesHelper = pagesHelper;

            LoadDataAsync();
            _serviceProvider = serviceProvider;
        }

        private async Task LoadDataAsync()
        {
            TrainingProgramViewModels.Clear();
            _allTrainingProgramsViewModels.Clear();

            foreach (var program in await _trainingProgramService.GetAllProgramsAsync())
            {
                var programViewModel = _serviceProvider.GetRequiredService<TrainingProgramViewModel>();
                programViewModel.LoadData(program);

                TrainingProgramViewModels.Add(programViewModel);
                _allTrainingProgramsViewModels.Add(programViewModel);
            }
        }

        [RelayCommand]
        public async Task CreateProgram()
        {
            if (!string.IsNullOrWhiteSpace(InputName) && InputDaysCount != null && 
                int.TryParse(InputDaysCount, out int daysCount) &&
                daysCount > 0)
            {
                var program = await _trainingProgramService.CreateProgramAsync(InputName, daysCount);

                var programViewModel = _serviceProvider.GetRequiredService<TrainingProgramViewModel>();
                programViewModel.LoadData(program);

                TrainingProgramViewModels.Add(programViewModel);
                _allTrainingProgramsViewModels.Add(programViewModel);

                InputDaysCount = null;
                InputName = null;
            }
        }

        [RelayCommand]
        public async Task DeleteProgram(int programId)
        {
            var programToRemove = TrainingProgramViewModels.FirstOrDefault(p => p.TrainingProgram.Id == programId);
            if (programToRemove != null)
            {
                TrainingProgramViewModels.Remove(programToRemove);
                _allTrainingProgramsViewModels.Remove(programToRemove);
            }
            await _trainingProgramService.DeleteProgramAsync(programId);
        }

        [RelayCommand]
        public void GoSelectedTrainingProgramPage(int programId)
        {
            _pagesHelper.GoTrainingProgramPage(programId);
        }

        [RelayCommand]
        public async Task GoTodaySessionPage(int programId)
        {
            var program = await _trainingProgramService.GetProgramByIdAsync(programId);

            int dayOrder = await _trainingProgramService.GetTodayDayOrder(programId);
            int dayId = program.ProgramDays.ElementAt(dayOrder - 1).Id;

            var session = await _workoutService.GetOrCreateWorkoutSessionAsync(dayId, DateTime.UtcNow.Date);

            _pagesHelper.GoSessionPage(session.Id);
        }

        public void ApplyFilter()
        {
            var quary = _allTrainingProgramsViewModels.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchedName))
            {
                quary = quary.Where(p => p.TrainingProgram.Name.ToLower().Contains(SearchedName.ToLower()));
            }

            TrainingProgramViewModels.Clear();
            foreach (var program in quary)
            {
                TrainingProgramViewModels.Add(program);
            }
        }

        partial void OnSearchedNameChanged(string value)
        {
            ApplyFilter();
        }
    }
}

