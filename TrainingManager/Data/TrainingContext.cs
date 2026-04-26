using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainingManager.Models;

namespace TrainingManager.Data
{
    public class TrainingContext : DbContext
    {
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<DayExercises> DayExercises { get; set; }
        public DbSet<WorkoutSet> WorkoutSet { get; set; }
        public DbSet<WorkoutSession> WorkoutSession { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Username=postgres;Password=postgres;Database=training");
        }
    }
}
