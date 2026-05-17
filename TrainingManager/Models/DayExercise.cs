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
        [Column("plainedSetsCount")]
        public int? PlainedSetsCount { get; set; }
        [Column("plainedRepsCount")]
        public int? PlainedRepsCount { get; set; }
        [Column("plainedSetsWeight")]
        public double? PlainedWeight { get; set; }
        public Day Day { get; set; }
        public Exercise Exercises { get; set; }
        public ICollection<WorkoutSet> WorkoutSet { get; set; }
    }
}
