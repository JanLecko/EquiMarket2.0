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

namespace EquiMarket.Controllers
{
    public class VideoController : Controller
    {
        private EquiContext db = new EquiContext();

        // GET: /Video/
        public ActionResult Index()
        {
            var videos = db.Videos.Include(v => v.Horse);
            return View(videos.ToList());
        }

        // GET: /Video/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // GET: /Video/Create
        public ActionResult Create()
        {
            ViewBag.HorseID = new SelectList(db.Horses, "ID", "Name");
            return View();
        }

        // POST: /Video/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,URL,HorseID")] Video video)
        {
            if (ModelState.IsValid)
            {
                db.Videos.Add(video);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HorseID = new SelectList(db.Horses, "ID", "Name", video.HorseID);
            return View(video);
        }

        // GET: /Video/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            ViewBag.HorseID = new SelectList(db.Horses, "ID", "Name", video.HorseID);
            return View(video);
        }

        // POST: /Video/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,URL,HorseID")] Video video)
        {
            if (ModelState.IsValid)
            {
                db.Entry(video).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HorseID = new SelectList(db.Horses, "ID", "Name", video.HorseID);
            return View(video);
        }

        // GET: /Video/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // POST: /Video/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Video video = db.Videos.Find(id);
            db.Videos.Remove(video);
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
