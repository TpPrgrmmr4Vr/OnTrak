﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnTrak.Models.Data.EFRepo;
using OnTrak.Models.Data.Repository;
using OnTrak.Models.Entities;
using OnTrak.Models.Entities.Body;
using OnTrak.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnTrak.Controllers
{
    public class BodyPartController : Controller
    {

        private IBodyAreaRepository bodyAreaRepository;
        private IBodyPartRepository bodyPartRepository;
        private IMuscleRepository muscleRepository;
        public DBGetter dbGetter = new DBGetter();
        public BodyPartController(IBodyAreaRepository bAreaRepo, IBodyPartRepository bPartRepo, IMuscleRepository muscleRepo)
        {
            bodyAreaRepository = bAreaRepo;
            bodyPartRepository = bPartRepo;
            muscleRepository = muscleRepo;
            dbGetter.AssignData(muscleRepository.Muscles.ToList(), bodyAreaRepository.BodyAreas.ToList(), bodyPartRepository.BodyParts.ToList());
        }

        public ViewResult Create()
        {
            BodyPartsViewModel bPartVM = new BodyPartsViewModel
            {
                BodyAreas = bodyAreaRepository.BodyAreas.ToList()
            };
            return View(bPartVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BodyPartsViewModel bodyPart, IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            BodyPart bPart = new BodyPart
            {
                BodyPartId = bodyPart.BodyPartId,
                Name = bodyPart.Name,
                Description = bodyPart.Description,
                BodyAreaId = bodyPart.BodyAreaId,
            };
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    bPart.Image = memoryStream.ToArray();
                }
            }

            if (ModelState.IsValid)
            {
                bodyPartRepository.SaveBodyPart(bPart);
                TempData["Message"] = $"{bodyPart.Name} has been created";
                return RedirectToAction("Index", "BodyArea", bodyAreaRepository.BodyAreas.ToList().CreateListBAreaVM(bodyPartRepository, bodyAreaRepository, dbGetter));
            }
            else
                return RedirectToAction("Index", "BodyArea", bodyAreaRepository.BodyAreas.ToList().CreateListBAreaVM(bodyPartRepository, bodyAreaRepository, dbGetter));
        }


        public ViewResult Edit(int? Id)
        {
            return View(bodyPartRepository.CreateBPartViewModel(bodyAreaRepository, Id, dbGetter));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BodyPartsViewModel bodyPartVM, IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            BodyPart bPart = new BodyPart
            {
                BodyPartId = bodyPartVM.BodyPartId,
                Name = bodyPartVM.Name,
                Description = bodyPartVM.Description,
                BodyAreaId = bodyPartVM.BodyAreaId,
            };
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    bPart.Image = memoryStream.ToArray();
                }
            }
            else
            {
                bPart.Image = bodyPartRepository.GetBodyPartById(bPart.BodyPartId).Image;
            }


            if (ModelState.IsValid)
            {
                bodyPartRepository.SaveBodyPart(bPart);
                TempData["Message"] = $"{bPart.Name} has been saved";
                return RedirectToAction("Index", "BodyArea");
            }
            else
                return View(bodyPartVM);
        }

        public ActionResult RetrieveImage(int Id)
        {

            byte[] cover = GetImageFromDataBase(Id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public byte[] GetImageFromDataBase(int Id)
        {
            var photo = bodyPartRepository.GetBodyPartById(Id).Image;
            byte[] cover = photo;
            return cover;
        }
    }
}
