using OnTrak.Models.Entities.Body;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTrak.Models.ViewModel
{
    public class ExerciseViewModel
    {
        public int BodyPartId { get; set; }
        public int BodyAreaId { get; set; }
        public int ExerciseId { get; set; }
        public int MuscleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Byte[] Image { get; set; }
        public List<string> Instructions { get; set; }
        public ICollection<BodyArea> BodyAreas { get; set; }
        public ICollection<Muscle> Muscles { get; set; }
        public ICollection<BodyPart> BodyParts { get; set; }
    }
}
