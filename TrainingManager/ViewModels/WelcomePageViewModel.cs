using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TrainingManager.Models;
using TrainingManager.Services;
using TrainingManager.Utils;

namespace TrainingManager.ViewModels
{
    public partial class WelcomePageViewModel : ViewModelBase
    {
        private readonly TrainingProgramService _trainingProgramService;
        private readonly PagesUtils _pagesHelper;
        private List<TrainingProgram> _allPrograms = new();

        public ObservableCollection<TrainingProgram> Programs { get; set; } = new();
        [ObservableProperty] private string searchedName;
        [ObservableProperty] private string inputName;
        [ObservableProperty] private int? inputDaysCount;

        public WelcomePageViewModel(
            TrainingProgramService trainingProgramService,
            PagesUtils pagesHelper)
        {
            _trainingProgramService = trainingProgramService;
            _pagesHelper = pagesHelper;

            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            Programs.Clear();
            _allPrograms.Clear();

            foreach (var program in await _trainingProgramService.GetAllProgramsAsync())
            {
                Programs.Add(program);
                _allPrograms.Add(program);
            }
        }

        [RelayCommand]
        public async Task CreateProgram()
        {
            if (InputDaysCount != null && InputDaysCount.Value > 0)
            {
                var program = await _trainingProgramService.CreateProgramAsync(InputName, (int)InputDaysCount);
                Programs.Add(program);
                _allPrograms.Add(program);
            }
        }

        [RelayCommand]
        public async Task DeleteProgram(int programId)
        {
            var programToRemove = Programs.FirstOrDefault(p => p.Id == programId);
            if (programToRemove != null)
            {
                Programs.Remove(programToRemove);
                _allPrograms.Remove(programToRemove);
            }
            await _trainingProgramService.DeleteProgramAsync(programId);
        }

        [RelayCommand]
        public void GoSelectedTrainingProgramPage(int programId)
        {
            _pagesHelper.GoTrainingProgramPage(programId);
        }

        public void ApplyFilter()
        {
            var quary = _allPrograms.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchedName))
            {
                quary = quary.Where(p => p.Name.ToLower().Contains(SearchedName.ToLower()));
            }

            Programs.Clear();
            foreach (var program in quary)
            {
                Programs.Add(program);
            }
        }

        partial void OnSearchedNameChanged(string value)
        {
            ApplyFilter();
        }
    }
}
