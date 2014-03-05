using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EquiMarket.Models;
using EquiMarket.DAL;
using System.IO;
using System.Data.Entity.Infrastructure;
using PagedList;
using EquiMarket.Models.SearchModels;

namespace EquiMarket.Controllers
{
    public class HorseController : Controller
    {
        private EquiContext db = new EquiContext();

        // GET: /Horse/
        public ActionResult Index()
        {
            // TODO: load only limited number of images (Explicit loading?)
            return View(db.Horses.ToList());
        }

        public ActionResult List(HorseSearchModel searchModel, string sortOrder, int? page)
        {
            var horses = db.Horses.Include(x => x.Images);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.CreatedSortParm = String.IsNullOrEmpty(sortOrder) ? "Created_desc" : "";
            ViewBag.AgeSortParm = sortOrder == "Age" ? "Age_desc" : "Age";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.KVHSortParm = sortOrder == "KVH" ? "KVH_desc" : "KVH";
            ViewBag.SearchModel = searchModel;

            switch (sortOrder)
            {
                case "Created_desc":
                    horses = horses.OrderByDescending(i => i.Created);
                    break;
                case "Age":
                    horses = horses.OrderBy(i => i.BirthDate);
                    break;
                case "Age_desc":
                    horses = horses.OrderByDescending(i => i.BirthDate);
                    break;
                case "Price":
                    horses = horses.OrderBy(i => i.Price);
                    break;
                case "Price_desc":
                    horses = horses.OrderByDescending(i => i.Price);
                    break;
                case "KVH":
                    horses = horses.OrderBy(i => i.KVH);
                    break;
                case "KVH_desc":
                    horses = horses.OrderByDescending(i => i.KVH);
                    break;
                default:
                    horses = horses.OrderBy(i => i.Created);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(horses.ToPagedList(pageNumber, pageSize));
        }


        // GET: /Horse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Horse horse = db.Horses.Find(id);
            if (horse == null)
            {
                return HttpNotFound();
            }
            return View(horse);
        }

        // GET: /Horse/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Horse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Name,BirthDate,Sex,BreedID,FathersName,MothersName,KVH,KVP,Description,Price")] Horse horse) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    horse.Created = DateTime.Now;
                    db.Horses.Add(horse);

                    db.SaveChanges();

                    List<String> list = Common.ImageHelper.SaveImages(Request, horse.ID);

                    foreach (var item in list)
                    {
                        var image = new Image() {
                            FileName = item,
                            HorseID = horse.ID
                        };

                        horse.Images.Add(image);
                    }

                    db.Entry(horse).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(horse);
        }

        // GET: /Horse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Horse horse = db.Horses
                .Include(i => i.Images)
                .Where(i => i.ID == id)
                .Single();
            
            if (horse == null)
                return HttpNotFound();

            return View(horse);
        }

        // POST: /Horse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, FormCollection formCollection)
        {
                
            Horse horseToUpdate = db.Horses
                .Include(i => i.Images)
                .Where(i => i.ID == id)
                .Single();

            if (TryUpdateModel(horseToUpdate, "",
                new string[] { "Name", "BirthDate", "Sex", "BreedID", "FathersName", "MothersName", "KVH", "KVP", "Description", "Price", "Location" }))
            {
                try
                {
                    var list = Common.ImageHelper.SaveImages(Request, horseToUpdate.ID);

                    foreach (var item in list) 
                    {
                        var image = new Image()
                        {
                            FileName = item,
                            HorseID = horseToUpdate.ID
                        };

                        horseToUpdate.Images.Add(image);
                    }

                    db.Entry(horseToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(horseToUpdate);
        }

        // GET: /Horse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Horse horse = db.Horses.Find(id);

            if (horse == null) 
                return HttpNotFound();

            return View(horse);
        }

        // POST: /Horse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Horse horse = db.Horses.Find(id);
            db.Horses.Remove(horse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
