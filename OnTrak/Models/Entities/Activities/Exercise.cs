using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnTrak.Models.Entities.Activities
{
    public class Exercise
    {
        [Required]
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ExerciseCategory ExerciseCategory{ get; set; }
        public string Instructions { get; set; }
        public Byte[] Image { get; set; }
    }
}
