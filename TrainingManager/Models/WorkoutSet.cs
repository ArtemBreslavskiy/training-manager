using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManager.Models
{
    public class WorkoutSet
    {
        public int Id { get; set; }
        public int OrderInExercises { get; set; }
        public int Reps { get; set; }
        public double? Weight { get; set; }
        public bool IsComplited { get; set; }
        public DayExercises DayExercises { get; set; }
        public WorkoutSession WorkoutSession { get; set; }
    }
}
