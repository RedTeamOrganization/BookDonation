using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookDonation.DB.Models;

namespace BookDonation.Web.Controllers
{
    public class ActionsController : Controller
    {
        private BookDonationDB db = new BookDonationDB();

        // GET: Actions
        public ActionResult Index()
        {
            return View(db.Action.ToList());
        }

        // GET: Actions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actions actions = db.Action.Find(id);
            if (actions == null)
            {
                return HttpNotFound();
            }
            return View(actions);
        }

        // GET: Actions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ActionName")] Actions actions)
        {
            if (ModelState.IsValid)
            {
                db.Action.Add(actions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(actions);
        }

        // GET: Actions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actions actions = db.Action.Find(id);
            if (actions == null)
            {
                return HttpNotFound();
            }
            return View(actions);
        }

        // POST: Actions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ActionName")] Actions actions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actions);
        }

        // GET: Actions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actions actions = db.Action.Find(id);
            if (actions == null)
            {
                return HttpNotFound();
            }
            return View(actions);
        }

        // POST: Actions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Actions actions = db.Action.Find(id);
            db.Action.Remove(actions);
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
