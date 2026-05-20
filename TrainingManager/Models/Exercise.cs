using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManager.Models
{
    [Table("exercises")]
    public class Exercise
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("notes")]
        public string Notes { get; set; }
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }
        [Column("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        public ICollection<DayExercise> DayExercises { get; set; }
    }
}
