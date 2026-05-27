using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManager.Models
{
    [Table("plannedWorkoutSets")]
    public class PlannedWorkoutSet
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("dayExerciseId")]
        public int DayExerciseId { get; set; }
        [Column("orderInExercise")]
        public int OrderInExercise { get; set; }
        [Column("plannedRepsCount")]
        public int? PlannedRepsCount { get; set; }
        [Column("plannedSetsWeight")]
        public double? PlannedWeight { get; set; }
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }
        [Column("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        public DayExercise DayExercise { get; set; }
    }
}
