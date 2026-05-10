using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManager.Models
{
    public class Day
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int OrderIndex { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public ICollection<DayExercises> DayExercises { get; set; }
        public ICollection<WorkoutSession> WorkoutSession { get; set; }
    }
}
