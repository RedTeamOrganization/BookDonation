using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookDonation.DB.Models;
using BookDonation.Web.ViewModels;
using BookDonation.Web.Repository;

namespace BookDonation.Web.Controllers
{
    public class BooksController : Controller
    {
        private BookDonationDB db = new BookDonationDB();
        [Route("Index")]
        [HttpGet]
        public ActionResult Index()
        {
            var content = db.Book.Select(s => new
            {
                s.Id,
                s.UserId,
                s.GenreId,
                s.AuthorId,
                s.Title,
                s.ISBN,
                s.Image,
                s.QuantityAvailable,
                s.QuantityReserved

            });
            List<DonateVM> donateModel = content.Select(item => new DonateVM()
            {
                Id = item.Id,
                //UserId =item.UserId,
                Title = item.Title,
                Image = item.Image,
                ISBN = item.ISBN,
                GenreId = item.GenreId,

                AuthorId = item.AuthorId,
                QuantityAvailable = item.QuantityAvailable,
                QuantityReserved = item.QuantityReserved

            }).ToList();
            return View(donateModel);
        }

        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
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
            var q = from temp in db.Book where temp.Id == Id select temp.Image;
            byte[] cover = q.First();
            return cover;
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Book.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }




        // GET: Books/Create
        [HttpGet]
    public ActionResult Create()
    {
        ViewBag.GenreID = new SelectList(db.Genre, "Id", "Name");
        return View();
    }

    [Route("Create")]
    [HttpPost]
    public ActionResult Create(/*[Bind(Include = "Title,GenreId,AuthorId,ISBN,Image,NumBookDonated")]*/ DonateVM model)
    {
        

            //return View(donatevm);
            HttpPostedFileBase file = Request.Files["ImageData"];
        HomeRepository service = new HomeRepository();
        int i = service.UploadImageInDataBase(file, model);
        if (i == 1)
        {
            return RedirectToAction("Index");
        }
        return View(model);

    }



        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Book.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,GenreId,AuthorId,Title,ISBN,Image,QuantityAvailable,QuantityReserved")] Books books)
        {
            if (ModelState.IsValid)
            {
                db.Entry(books).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(books);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Book.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Books books = db.Book.Find(id);
            db.Book.Remove(books);
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
