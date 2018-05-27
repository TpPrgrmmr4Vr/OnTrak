﻿using OnTrak.Models.Entities;
using OnTrak.Models.Entities.Body;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTrak.Models.Data.Repository
{
    public interface IBodyAreaRepository
    {
        IQueryable<BodyArea> BodyAreas { get; }
        BodyArea getBodyAreaById(int? Id);
        void SaveBodyArea(BodyArea bodyArea);
    }
}
