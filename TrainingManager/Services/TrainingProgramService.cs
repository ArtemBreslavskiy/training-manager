using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManager.Data;
using TrainingManager.Models;

namespace TrainingManager.Services
{
    public class TrainingProgramService
    {
        private readonly IDbContextFactory<TrainingContext> _factory;

        public TrainingProgramService(IDbContextFactory<TrainingContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<TrainingProgram>> GetAllProgramsAsync()
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.TrainingPrograms
                .Include(p => p.ProgramDays)
                .ThenInclude(d => d.DayExercises)
                .ThenInclude(de => de.Exercise)
                .OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<TrainingProgram?> GetProgramByIdAsync(int programId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.TrainingPrograms
                .Include(p => p.ProgramDays)
                .ThenInclude(d => d.DayExercises)
                .ThenInclude(de => de.Exercise)
                .FirstOrDefaultAsync(p => p.Id == programId);
        }

        public async Task<TrainingProgram> CreateProgramAsync(string name, int daysCount)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var program = new TrainingProgram
            {
                Name = name,
                DaysCount = daysCount,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ProgramDays = new List<Day>()
            };

            for (int i = 0; i < daysCount; i++)
            {
                program.ProgramDays.Add(new Day
                {
                    Name = $"Day {i + 1}",
                    OrderIndex = i + 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                });
            }

            context.TrainingPrograms.Add(program);
            await context.SaveChangesAsync();
            return program;
        }

        public async Task DeleteProgramAsync(int programId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var program = await context.TrainingPrograms.FindAsync(programId);
            if (program != null)
            {
                context.TrainingPrograms.Remove(program);
                await context.SaveChangesAsync();
            }
        }

        public async Task<TrainingProgram> UpdateProgramAsync(TrainingProgram updatedProgram)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var program = await context.TrainingPrograms
                .Include(p => p.ProgramDays)
                .ThenInclude(d => d.DayExercises)
                .ThenInclude(de => de.Exercise)
                .FirstOrDefaultAsync(p => p.Id == updatedProgram.Id);

            if (program != null)
            {
                program.Name = updatedProgram.Name;
                program.UpdatedAt = DateTime.UtcNow;

                var currentDays = program.ProgramDays.OrderBy(d => d.OrderIndex).ToList();
                if (currentDays.Count < updatedProgram.DaysCount)
                {
                    for (int i = currentDays.Count; i < updatedProgram.DaysCount; i++)
                    {
                        program.ProgramDays.Add(new Day
                        {
                            Name = $"Day {i + 1}",
                            OrderIndex = i + 1,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                        });
                    }
                }
                else if (currentDays.Count > updatedProgram.DaysCount)
                {
                    var daysToRemove = currentDays.OrderByDescending(d => d.OrderIndex).Take(currentDays.Count - updatedProgram.DaysCount);
                    foreach (var day in daysToRemove)
                        context.Days.Remove(day);
                }

                await context.SaveChangesAsync();
            }
            return program;
        }
    }
}

