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

namespace EquiMarket.Controllers
{
    public class HorseController : Controller
    {
        private EquiContext db = new EquiContext();

        // GET: /Horse/
        public ActionResult Index()
        {
            return View(db.Horses.ToList());
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
        public ActionResult Create([Bind(Include="Name,BirthDate,Sex,Breed,FathersName,MothersName,KVH,KVP,Description,Price")] Horse horse) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    horse.Images = Common.ImageHelper.SaveImages(Request, horse.ID);

                    db.Horses.Add(horse);

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
            
            Horse horse = db.Horses.Find(id);
            
            if (horse == null)
                return HttpNotFound();

            return View(horse);
        }

        // POST: /Horse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,BirthDate,Sex,BreedID,FathersName,MothersName,KVH,KVP,Description,Price")] Horse horse)
        {
            if (ModelState.IsValid)
            {
                horse.Images = Common.ImageHelper.SaveImages(Request, horse.ID);

                db.Entry(horse).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(horse);
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
