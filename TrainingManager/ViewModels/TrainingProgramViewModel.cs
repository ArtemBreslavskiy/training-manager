using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManager.Services;
using TrainingManager.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TrainingManager.ViewModels
{
    public partial class TrainingProgramViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TrainingProgramService _trainingProgramService;

        [ObservableProperty] private TrainingProgram trainingProgram;
        [ObservableProperty] private string? inputProgramName;

        public TrainingProgramViewModel(IServiceProvider serviceProvider, TrainingProgramService trainingProgramService)
        {
            _serviceProvider = serviceProvider;
            _trainingProgramService = trainingProgramService;
        }

        public void LoadData(TrainingProgram program)
        {
            TrainingProgram = program;
            InputProgramName = program.Name;
        }

        private async Task RenameProgramAsync()
        {
            TrainingProgram.Name = InputProgramName;
            await _trainingProgramService.UpdateProgramAsync(TrainingProgram);
        }

        partial void OnInputProgramNameChanged(string value)
        {
            RenameProgramAsync();
        }
    }
}
