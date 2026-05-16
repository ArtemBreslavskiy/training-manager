using Avalonia.Utilities;
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
    public class WorkoutService
    {
        private readonly IDbContextFactory<TrainingContext> _factory;

        public WorkoutService(IDbContextFactory<TrainingContext> factory)
        {
            _factory = factory;
        }

        public async Task<WorkoutSession> GetOrCreateSessionAsync(int dayId, DateTime date)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var session = await context.WorkoutSession
                .Include(s => s.WorkoutSets)
                .FirstOrDefaultAsync(s => s.DayId == dayId && s.Date.Date == date.Date);

            if (session == null)
            {
                var dayExists = await context.Days.AnyAsync(d => d.Id == dayId);
                if (!dayExists)
                    throw new ArgumentException($"Day with Id={dayId} not found", nameof(dayId));

                session = new WorkoutSession()
                {
                    DayId = dayId,
                    Date = date.Date
                };
                context.WorkoutSession.Add(session);
                await context.SaveChangesAsync();
            }

            return session;
        }

        public async Task<WorkoutSet> AddSetAsync(
            int sessionId,
            int dayExerciseId,
            int order,
            double? weight,
            int reps,
            bool isCompleted = true
        )
        {
            await using var context = await _factory.CreateDbContextAsync();
            var dayExercise = await context.DayExercises.FindAsync(dayExerciseId);
            if (dayExercise == null)
                throw new ArgumentException($"DayExercises with Id={dayExerciseId} not found");

            var session = await context.WorkoutSession.FindAsync(sessionId);
            if (session == null)
                throw new ArgumentException($"WorkoutSession with Id={sessionId} not found");

            if (dayExercise.DayId != session.DayId)
                throw new InvalidOperationException("DayExercise does not belong to the day of this session.");

            var set = new WorkoutSet()
            {
                OrderInExercises = order,
                Reps = reps,
                Weight = weight,
                IsComplited = isCompleted,
                DayExercisesId = dayExerciseId,
                WorkoutSessionId = sessionId
            };

            context.WorkoutSet.Add(set);
            await context.SaveChangesAsync();

            return set;
        }

        public async Task UpdateSetAsync(int setId, int reps, double? weight, bool isCompleted)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var set = await context.WorkoutSet.FindAsync(setId);
            if (set == null)
                throw new ArgumentException($"WorkoutSet with Id={setId} not found");

            set.Reps = reps;
            set.Weight = weight;
            set.IsComplited = isCompleted;
            await context.SaveChangesAsync();
        }

        public async Task DeleteSetAsync(int setId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var set = await context.WorkoutSet.FindAsync(setId);
            if (set != null)
            {
                context.WorkoutSet.Remove(set);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<WorkoutSet>> GetSetsForSessionAsync(int sessionId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.WorkoutSet
                .Where(s => s.WorkoutSessionId == sessionId)
                .Include(s => s.DayExercises)
                .ThenInclude(de => de.Exercises)
                .OrderBy(s => s.DayExercises.OrderInDay)
                .ThenBy(s => s.OrderInExercises)
                .ToListAsync();
        }

        public async Task<List<WorkoutSet>> GetProgressDataAsync(int exerciseId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.WorkoutSet
                .Where(ws => ws.DayExercises.ExercisesId == exerciseId)
                .Include(ws => ws.WorkoutSession)
                .Include(ws => ws.DayExercises)
                .OrderBy(ws => ws.WorkoutSession.Date)
                .ThenBy(ws => ws.OrderInExercises)
                .ToListAsync();
        }

        public async Task<WorkoutSession?> GetSessionByIdAsync(int sessionId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.WorkoutSession
                .Include(s => s.WorkoutSets)
                    .ThenInclude(ws => ws.DayExercises)
                        .ThenInclude(de => de.Exercises)
                .FirstOrDefaultAsync(s => s.Id == sessionId);
        }
    }
}