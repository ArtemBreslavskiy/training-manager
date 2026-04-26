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
        public int Order_index { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public ICollection<DayExercises> DayExercises { get; set; }
        public ICollection<WorkoutSession> WorkoutSession { get; set; }
    }
}
