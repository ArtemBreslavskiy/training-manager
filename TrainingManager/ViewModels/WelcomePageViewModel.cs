using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManager.Data;
using TrainingManager.Models;
using TrainingManager.Services;
using TrainingManager.Utils;

namespace TrainingManager.ViewModels
{
    public partial class WelcomePageViewModel : ViewModelBase
    {
        private readonly TrainingProgramService _trainingProgramService = new(new IDbContextFactory<TrainingContext>());
        private readonly PagesHelper _pagesHelper = new();
        private List<TrainingProgram> _allPrograms = new();
        public ObservableCollection<TrainingProgram> Programs { get; set; } = new(); 

        [ObservableProperty]
        private string searchedName;

        [ObservableProperty]
        private string inputName;

        [ObservableProperty]
        private int inputDaysCount;

        [ObservableProperty]
        private int selectedProgramId;

        [RelayCommand]
        public async Task CreateProgram()
        {
            await _trainingProgramService.CreateProgramAsync(InputName, InputDaysCount);
        }

        [RelayCommand]
        public async Task DeleteProgram()
        {
            await _trainingProgramService.DeleteProgramAsync(SelectedProgramId);
        }

        [RelayCommand]
        public void GoSelectedTrainingProgramPage()
        {
            _pagesHelper.GoTrainingProgramPage(SelectedProgramId);
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
    }
}
