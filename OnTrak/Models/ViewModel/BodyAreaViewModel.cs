﻿using OnTrak.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTrak.Models.ViewModels
{
    public class BodyAreaViewModel
    {

        public int Id { get; set; }
        public int NumberOfParts { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Byte[] Image { get; set; }

        public ICollection<BodyPart> BodyParts { get; set; }
    }
}
