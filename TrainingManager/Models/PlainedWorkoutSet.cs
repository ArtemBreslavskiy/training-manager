using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManager.Models
{
    [Table("plainedWorkoutSet")]
    public class PlainedWorkoutSet
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("plainedRepsCount")]
        public int? PlainedRepsCount { get; set; }
        [Column("plainedSetsWeight")]
        public double? PlainedWeight { get; set; }
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }
        [Column("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        public DayExercise DayExercise { get; set; }
    }
}
