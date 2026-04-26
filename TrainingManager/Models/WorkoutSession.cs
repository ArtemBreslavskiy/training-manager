using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManager.Models
{
    public class WorkoutSession
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public Day Day { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public ICollection<WorkoutSet> WorkoutSets { get; set; }
    }
}
