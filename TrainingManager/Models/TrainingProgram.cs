using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManager.Models
{
    public class TrainingProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Days { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Day> ProgramDays { get; set; }
    }
}
