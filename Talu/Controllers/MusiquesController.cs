﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Talu.Models;

namespace Talu.Controllers
{
    [Authorize]
    public class MusiquesController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: Musiques
        public ActionResult Index()
        {
            var musique = db.Musique.Include(m => m.Album);
            return View(musique.ToList());
        }

        // GET: Musiques/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musique musique = db.Musique.Find(id);
            if (musique == null)
            {
                return HttpNotFound();
            }
            return View(musique);
        }

        // GET: Musiques/Create
        public ActionResult Create()
        {
            ViewBag.IdAlbum = new SelectList(db.Album, "Id", "Nom");
            return View();
        }

        // POST: Musiques/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,Style,LienTelechargement,LienEcoute,IdAlbum")] Musique musique)
        {
            if (ModelState.IsValid)
            {
                db.Musique.Add(musique);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAlbum = new SelectList(db.Album, "Id", "Nom", musique.IdAlbum);
            return View(musique);
        }

        // GET: Musiques/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musique musique = db.Musique.Find(id);
            if (musique == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAlbum = new SelectList(db.Album, "Id", "Nom", musique.IdAlbum);
            return View(musique);
        }

        // POST: Musiques/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,Style,LienTelechargement,LienEcoute,IdAlbum")] Musique musique)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musique).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAlbum = new SelectList(db.Album, "Id", "Nom", musique.IdAlbum);
            return View(musique);
        }

        // GET: Musiques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musique musique = db.Musique.Find(id);
            if (musique == null)
            {
                return HttpNotFound();
            }
            return View(musique);
        }

        // POST: Musiques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Musique musique = db.Musique.Find(id);
            db.Musique.Remove(musique);
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