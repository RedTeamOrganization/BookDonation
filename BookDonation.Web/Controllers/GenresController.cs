﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookDonation.DB.Models;
using BookDonation.DB;

namespace BookDonation.Web.Controllers
{
    public class GenresController : Controller
    {
        private BookDonationDB db = new BookDonationDB();

        // GET: Genres
        public ActionResult Index()
        {
            return View(db.Genre.ToList());
        }

        // GET: Genres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genres genres = db.Genre.Find(id);
            if (genres == null)
            {
                return HttpNotFound();
            }
            return View(genres);
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Genres genres)
        {
            if (ModelState.IsValid)
            {
                db.Genre.Add(genres);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genres);
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genres genres = db.Genre.Find(id);
            if (genres == null)
            {
                return HttpNotFound();
            }
            return View(genres);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Genres genres)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genres).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genres);
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genres genres = db.Genre.Find(id);
            if (genres == null)
            {
                return HttpNotFound();
            }
            return View(genres);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Genres genres = db.Genre.Find(id);
            db.Genre.Remove(genres);
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
