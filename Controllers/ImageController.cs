﻿using System;
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
using EquiMarket.Common;

namespace EquiMarket.Controllers
{
    public class ImageController : Controller
    {
        private EquiContext db = new EquiContext();

        // GET: /Image/
        public ActionResult Index()
        {
            var images = db.Images.Include(i => i.Horse);
            return View(images.ToList());
        }

        // GET: /Image/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // GET: /Image/Create
        public ActionResult Create()
        {
            ViewBag.HorseID = new SelectList(db.Horses, "ID", "Name");
            return View();
        }

        // POST: /Image/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,FilePath,HorseID")] Image image)
        {
            if (ModelState.IsValid)
            {
                db.Images.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HorseID = new SelectList(db.Horses, "ID", "Name", image.HorseID);
            return View(image);
        }

        // GET: /Image/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            ViewBag.HorseID = new SelectList(db.Horses, "ID", "Name", image.HorseID);
            return View(image);
        }

        // POST: /Image/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,FilePath,HorseID")] Image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HorseID = new SelectList(db.Horses, "ID", "Name", image.HorseID);
            return View(image);
        }

        // GET: /Image/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: /Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Download(int id, ImageFormat format)
        {
            try 
            {
                Image img = db.Images.Find(id);

                if(img == null)
                    return File(new FileStream(ImageHelper.DefaultImagePath(format), FileMode.Open), ImageHelper.ContentType);

                switch (format)
                {
                    case ImageFormat.Thumbnail:
                        return File(new FileStream(img.ThumbnailFullPath, FileMode.Open), ImageHelper.ContentType);
                    case ImageFormat.Medium:
                        return File(new FileStream(img.MediumFullPath, FileMode.Open), ImageHelper.ContentType);
                    case ImageFormat.Large:
                        return File(new FileStream(img.LargeFullPath, FileMode.Open), ImageHelper.ContentType);
                    default:
                        return File(new FileStream(ImageHelper.DefaultImagePath(format), FileMode.Open), ImageHelper.ContentType);
                }
            }
            catch
            {
                return File(new FileStream(ImageHelper.DefaultImagePath(format), FileMode.Open), ImageHelper.ContentType);
                //TODO : Log
            }
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
