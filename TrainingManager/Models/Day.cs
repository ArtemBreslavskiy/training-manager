using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManager.Models
{
    [Table("days")]
    public class Day
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = "Untitled";
        [Column("notes")]
        public string? Notes { get; set; }
        [Column("orderInProgram")]
        public int OrderInProgram { get; set; }
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }
        [Column("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public ICollection<DayExercise> DayExercises { get; set; } = new List<DayExercise>();
        public ICollection<WorkoutSession> WorkoutSessions { get; set; } = new List<WorkoutSession>();
    }
}

