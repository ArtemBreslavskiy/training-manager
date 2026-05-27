using Avalonia.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public async Task<PlannedWorkoutSet> AddPlainedWorkoutSetAsync(int dayExerciseId, int orderInExercise, int? plannedRepsCount = null, double? plannedWeight = null)
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

        public async Task<WorkoutSession?> GetWorkoutSessionByIdAsync(int sessionId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.WorkoutSessions
                .Include(s => s.Day)
                .Include(s => s.WorkoutSets)
                    .ThenInclude(ws => ws.DayExercise)
                        .ThenInclude(de => de.Exercise)
                .FirstOrDefaultAsync(s => s.Id == sessionId);
        }

        public async Task<WorkoutSession> GetOrCreateWorkoutSessionAsync(int dayId, DateTime date)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var session = await context.WorkoutSessions
                .Include(s => s.WorkoutSets)
                .FirstOrDefaultAsync(s => s.DayId == dayId && s.Date.Date == date.Date);

            if (session == null)
            {
                var day = await context.Days
                    .Include(d => d.DayExercises)
                        .ThenInclude(de => de.PlannedWorkoutSets)
                    .FirstOrDefaultAsync(d => d.Id == dayId);
                if (day == null)
                    throw new ArgumentException($"Day with Id={dayId} not found", nameof(dayId));

                session = new WorkoutSession()
                {
                    DayId = dayId,
                    Date = date.Date,
                    WorkoutSets = new List<WorkoutSet>()
                };

                foreach (var dayExercise in day.DayExercises)
                {
                    foreach (var planned in dayExercise.PlannedWorkoutSets)
                    {
                        session.WorkoutSets.Add(new WorkoutSet
                        {
                            OrderInExercise = planned.OrderInExercise,
                            RepsCount = null,
                            Weight = null,
                            IsComplited = false,
                            DayExerciseId = dayExercise.Id
                        });
                    }
                }

                context.WorkoutSessions.Add(session);
                await context.SaveChangesAsync();
            }

            return session;
        }

        public async Task<WorkoutSet> CreateWorkoutSetAsync(
            int sessionId,
            int dayExerciseId,
            int orderInExercise,
            double? weight,
            int reps,
            bool isCompleted = true
        )
        {
            await using var context = await _factory.CreateDbContextAsync();
            var dayExercise = await context.DayExercises.FindAsync(dayExerciseId);
            if (dayExercise == null)
                throw new ArgumentException($"DayExercises with Id={dayExerciseId} not found");

            var session = await context.WorkoutSessions.FindAsync(sessionId);
            if (session == null)
                throw new ArgumentException($"WorkoutSession with Id={sessionId} not found");

            if (dayExercise.DayId != session.DayId)
                throw new InvalidOperationException("DayExercise does not belong to the day of this session.");

            var set = new WorkoutSet()
            {
                OrderInExercise = orderInExercise,
                RepsCount = reps,
                Weight = weight,
                IsComplited = isCompleted,
                DayExerciseId = dayExerciseId,
                WorkoutSessionId = sessionId
            };

            context.WorkoutSets.Add(set);
            await context.SaveChangesAsync();

            return set;
        }

        public async Task<WorkoutSet> UpdateWorkoutSetAsync(int workoutSetId, int? repsCount, double? weight)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var workoutSet = await context.WorkoutSets.FindAsync(workoutSetId);
            if (workoutSet == null)
                throw new ArgumentException($"WorkoutSet with Id={workoutSetId} not found");

            workoutSet.RepsCount = repsCount;
            workoutSet.Weight = weight;
            await context.SaveChangesAsync();

            return await context.WorkoutSets
                .Include(x => x.WorkoutSession)
                .FirstAsync(pws => pws.Id == workoutSetId);
        }

        public async Task DeleteWorkoutSetAsync(int setId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var set = await context.WorkoutSets.FindAsync(setId);
            if (set != null)
            {
                context.WorkoutSets.Remove(set);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<WorkoutSet>> GetWorkoutSetAsync(int sessionId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.WorkoutSets
                .Where(s => s.WorkoutSessionId == sessionId)
                .Include(s => s.DayExercise)
                .ThenInclude(de => de.Exercise)
                .OrderBy(s => s.DayExercise.OrderInDay)
                    .ThenBy(s => s.OrderInExercise)
                .ToListAsync();
        }

        public async Task<PlannedWorkoutSet?> GetPlannedWorkoutSets(int workoutSetId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var workoutSet = await context.WorkoutSets
            .Include(ws => ws.DayExercise)
                .ThenInclude(de => de.PlannedWorkoutSets)
            .FirstOrDefaultAsync(ws => ws.Id == workoutSetId);

            if (workoutSet?.DayExercise == null) return null;
            return workoutSet.DayExercise.PlannedWorkoutSets.FirstOrDefault(pws => pws.OrderInExercise == workoutSet.OrderInExercise);
        }

        public async Task<WorkoutSet> GetLastWorkoutSetsForExercise(int workoutSetId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var workoutSet = await context.WorkoutSets
                .Include(ws => ws.DayExercise)
                .Include(ws => ws.WorkoutSession)
                .FirstOrDefaultAsync(ws => ws.Id == workoutSetId);

            if (workoutSet?.DayExercise == null) return null;

            var lastSessionDate = await context.WorkoutSets
                .Where(ws => ws.DayExercise.ExerciseId == workoutSet.DayExercise.ExerciseId
                          && ws.WorkoutSession.Date.Date != workoutSet.WorkoutSession.Date.Date)
                .MaxAsync(ws => (DateTime?)ws.WorkoutSession.Date);

            if (lastSessionDate == null) return null;

            return await context.WorkoutSets
                .FirstOrDefaultAsync(ws => ws.DayExercise.ExerciseId == workoutSet.DayExercise.ExerciseId
                                       && ws.WorkoutSession.Date.Date == lastSessionDate.Value.Date
                                       && ws.OrderInExercise == workoutSet.OrderInExercise);
        }

        public async Task<List<WorkoutSet>> GetProgressDataAsync(int exerciseId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.WorkoutSets
                .Where(ws => ws.DayExercise.ExerciseId == exerciseId)
                .Include(ws => ws.WorkoutSession)
                .Include(ws => ws.DayExercise)
                .OrderBy(ws => ws.WorkoutSession.Date)
                    .ThenBy(ws => ws.OrderInExercise)
                .ToListAsync();
        }
    }
}