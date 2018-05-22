﻿using OnTrak.Models.Data.Repository;
using OnTrak.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTrak.Models.Data.EFRepo
{
    public class EFBodyAreaRepository : IBodyAreaRepository
    {
        private ApplicationDbContext context;

        public EFBodyAreaRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public BodyArea getBodyAreaById(int? id)
        {
            var bodyArea = context.BodyAreas.Find(id);
            return bodyArea;
        }

        public void SaveBodyArea(BodyArea bodyArea)
        {
            if (bodyArea.Id == 0)
            {
                context.BodyAreas.Add(bodyArea);
            }
            else
            {
                BodyArea dbEntry = context.BodyAreas.FirstOrDefault(ba => ba.Id == bodyArea.Id);
                if (dbEntry != null)
                {
                    dbEntry.Image = bodyArea.Image;
                    dbEntry.Name = bodyArea.Name;
                    dbEntry.NumberOfParts = bodyArea.NumberOfParts;
                    dbEntry.Description = bodyArea.Description;
                    dbEntry.NumberOfParts = bodyArea.NumberOfParts;
                }
                
            }
            context.SaveChanges();

        }

        public IQueryable<BodyArea> BodyAreas => context.BodyAreas;
    }
}
