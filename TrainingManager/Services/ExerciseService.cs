using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingManager.Data;
using TrainingManager.Models;

namespace TrainingManager.Services
{
    public class ExerciseService
    {
        private readonly IDbContextFactory<TrainingContext> _factory;

        public ExerciseService(IDbContextFactory<TrainingContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Exercise>> GetAllExercisesAsync()
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.Exercises
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
        
        public async Task<Exercise?> GetExerciseByIdAsync(int exerciseId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.Exercises
                .FirstOrDefaultAsync(e => e.Id == exerciseId);
        }

        public async Task<Exercise?> GetExerciseByNameAsync(string name)
        {
            await using var ctx = await _factory.CreateDbContextAsync();
            return await ctx.Exercises
                .FirstOrDefaultAsync(e => e.Name == name);
        }

        public async Task<Exercise> CreateExerciseAsync(string name)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var exercise = new Exercise()
            {
                Name = name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            context.Exercises.Add(exercise);
            await context.SaveChangesAsync();
            return exercise;
        }

        public async Task<Exercise> FindOrCreateExercise(string name)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var exercise = await context.Exercises.FirstOrDefaultAsync(e => e.Name == name);

            if (exercise == null )
            {
                exercise = await CreateExerciseAsync(name);
            }
            return exercise;
        }

        public async Task DeleteExerciseAsync(int exerciseId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var exercise = await context.Exercises.FindAsync(exerciseId);
            if (exercise == null)
                return;

            bool isUsed = await context.DayExercises.AnyAsync(de => de.ExerciseId == exerciseId);
            if (isUsed)
                throw new InvalidOperationException("You cannot delete an exercise that is used in training programs.");

            context.Exercises.Remove(exercise);
            await context.SaveChangesAsync();
        }

        public async Task<Exercise> UpdateExerciseAsync(Exercise newExercise)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var exercise = await context.Exercises.FirstOrDefaultAsync(e => e.Id == newExercise.Id);

            if (exercise != null)
            {
                exercise.Name = newExercise.Name;
                exercise.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync();
            }
            return exercise;
        }

        public async Task<DayExercise> AddExerciseToDayAsync(
            int dayId,
            int exerciseId,
            int orderInDay,
            List<PlannedWorkoutSet> plainedWorkoutSets
        )
        {
            await using var context = await _factory.CreateDbContextAsync();
            var day = await context.Days.FindAsync(dayId);
            if (day == null)
                throw new ArgumentException($"Day with Id={dayId} not found", nameof(dayId));

            var exercise = await context.Exercises.FindAsync(exerciseId);
            if (exercise == null)
                throw new ArgumentException($"Exercise with Id={exerciseId} not found", nameof(exerciseId));

            var dayExercise = new DayExercise
            {
                DayId = dayId,
                ExerciseId = exerciseId,
                OrderInDay = orderInDay,
                PlannedWorkoutSets = plainedWorkoutSets,
            };

            context.DayExercises.Add(dayExercise);
            await context.SaveChangesAsync();

            return await context.DayExercises
                .Include(de => de.Exercise)
                .Include(d => d.PlannedWorkoutSets)
                .FirstAsync(de => de.Id == dayExercise.Id);
        }

        public async Task RemoveExerciseFromDayAsync(int dayExerciseId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var dayExercise = await context.DayExercises.FindAsync(dayExerciseId);
            if (dayExercise != null)
            {
                context.DayExercises.Remove(dayExercise);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<DayExercise>> GetExercisesForDayAsync(int dayId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.DayExercises
                .Where(de => de.DayId == dayId)
                .Include(de => de.Exercise)
                .OrderBy(de => de.OrderInDay)
                .ToListAsync();
        }

        public async Task UpdatePlannedParametersAsync(
            int dayExerciseId,
            List<PlannedWorkoutSet> plainedWorkoutSets
        )
        {
            await using var context = await _factory.CreateDbContextAsync();
            var dayExercise = await context.DayExercises.FindAsync(dayExerciseId);
            if (dayExercise == null)
                throw new ArgumentException($"The DayExercises entry with Id={dayExerciseId} was not found");

            dayExercise.PlannedWorkoutSets = plainedWorkoutSets;

            await context.SaveChangesAsync();
        }

        public async Task ReorderExerciseAsync(int dayExerciseId, int newOrder)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var dayExercise = await context.DayExercises.FindAsync(dayExerciseId);
            if (dayExercise == null)
                throw new ArgumentException($"DayExercises with Id={dayExerciseId} not found");

            dayExercise.OrderInDay = newOrder;
            await context.SaveChangesAsync();
        }
    }
}
