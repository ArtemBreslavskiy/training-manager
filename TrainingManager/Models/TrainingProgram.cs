using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManager.Models
{
    [Table("trainingPrograms")]
    public class TrainingProgram
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("daysCount")]
        public int DaysCount { get; set; }
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }
        [Column("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        public ICollection<Day> ProgramDays { get; set; }
    }
}
