using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManager.Models
{
    public class DayExercises
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public Day Day { get; set; }
        public int ExercisesId { get; set; }
        public Exercise Exercises { get; set; }
        public int OrderInDay { get; set; }
        public int? PlainedSets { get; set; }
        public int? PlainedReps { get; set; }
        public double? PlainedWeight { get; set; }
        public ICollection<WorkoutSet> WorkoutSet { get; set; }
    }
}
