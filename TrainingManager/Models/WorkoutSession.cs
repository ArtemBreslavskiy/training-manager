using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManager.Models
{
    [Table("workoutSessions")]
    public class WorkoutSession
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("dayId")]
        public int DayId { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("notes")]
        public string? Notes { get; set; }
        public Day Day { get; set; }
        public ICollection<WorkoutSet> WorkoutSets { get; set; }
    }
}
