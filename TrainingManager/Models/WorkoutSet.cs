using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManager.Models
{
    [Table("workoutSets")]
    public class WorkoutSet
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("orderInExercise")]
        public int OrderInExercise { get; set; }
        [Column("repsCount")]
        public int? RepsCount { get; set; }
        [Column("weigth")]
        public double? Weight { get; set; }
        [Column("isCompleted")]
        public bool IsComplited { get; set; }
        [Column("dayExercisesId")]
        public int DayExercisesId { get; set; }
        [Column("WorkoutSessionId")]
        public int WorkoutSessionId { get; set; }
        public DayExercise DayExercise { get; set; }
        public WorkoutSession WorkoutSession { get; set; }
    }
}
