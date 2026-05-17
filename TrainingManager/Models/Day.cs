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
        public string? Name { get; set; }
        [Column("orderIndex")]
        public int OrderIndex { get; set; }
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }
        [Column("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public ICollection<DayExercise> DayExercises { get; set; }
        public ICollection<WorkoutSession> WorkoutSession { get; set; }
    }
}
