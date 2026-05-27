using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManager.Models
{
    [Table("dayExercises")]
    public class DayExercise
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("dayId")]
        public int DayId { get; set; }
        [Column("exerciseId")]
        public int ExerciseId { get; set; }
        [Column("orderInDay")]
        public int OrderInDay { get; set; }
        public Day Day { get; set; }
        public Exercise Exercise { get; set; }
        public ICollection<WorkoutSet> WorkoutSets { get; set; } = new List<WorkoutSet>();
        public ICollection<PlannedWorkoutSet> PlannedWorkoutSets { get; set; } = new List<PlannedWorkoutSet>();
    }
}

