using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManager.Data;
using TrainingManager.Models;

namespace TrainingManager.Services
{
    public class PlannedWorkoutSetService
    {
        private readonly IDbContextFactory<TrainingContext> _factory;

        public PlannedWorkoutSetService(IDbContextFactory<TrainingContext> factory)
        {
            _factory = factory;
        }

        public async Task<PlannedWorkoutSet> CreatePlainedWorkoutSetAsync(int dayExerciseId, int orderInExercise, int? plannedRepsCount = null, double? plannedWeight = null)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var dayExercise = await context.DayExercises
                .Include(d => d.PlannedWorkoutSets)
                .FirstOrDefaultAsync(d => d.Id == dayExerciseId);

            if (dayExercise == null)
                throw new ArgumentException($"DayExercises with Id={dayExerciseId} not found");

            PlannedWorkoutSet plannedWorkoutSet = new()
            {
                OrderInExercise = orderInExercise,
                PlannedRepsCount = plannedRepsCount,
                PlannedWeight = plannedWeight,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            dayExercise.PlannedWorkoutSets.Add(plannedWorkoutSet);
            await context.SaveChangesAsync();

            return await context.PlannedWorkoutSets
                .Include(pws => pws.DayExercise)
                    .ThenInclude(de => de.Exercise)
                .FirstAsync(pws => pws.Id == plannedWorkoutSet.Id);
        }

        public async Task<PlannedWorkoutSet> UpdatePlainedWorkoutSetAsync(int plannedWorkoutSetId, int? newPlannedRepsCount, double? newPlannedWeight)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var plannedWorkoutSet = await context.PlannedWorkoutSets.FindAsync(plannedWorkoutSetId);

            if (plannedWorkoutSet == null)
                throw new ArgumentException($"PlannedWorkoutSet with Id={plannedWorkoutSetId} not found");

            plannedWorkoutSet.PlannedRepsCount = newPlannedRepsCount;
            plannedWorkoutSet.PlannedWeight = newPlannedWeight;
            plannedWorkoutSet.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();

            return await context.PlannedWorkoutSets
                .Include(pws => pws.DayExercise)
                    .ThenInclude(de => de.Exercise)
                .FirstAsync(pws => pws.Id == plannedWorkoutSetId);
        }

        public async Task DeletePlainedWorkoutSetAsync(int plannedWorkoutSetId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var plannedSet = await context.PlannedWorkoutSets.FindAsync(plannedWorkoutSetId);
            if (plannedSet != null)
            {
                context.PlannedWorkoutSets.Remove(plannedSet);
                await context.SaveChangesAsync();
            }
        }
    }
}
