using BookDonation.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookDonation.DB.Models;
using BookDonation.Web.ViewModels;
using BookDonation.Web.Repository;

namespace BookDonation.Web.Controllers
{
    [RoutePrefix("Home")]
    [ValidateInput(false)]
    public class HomeController : Controller
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
                GenreId= item.GenreId,
                AuthorId=item.AuthorId
                
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
        [HttpGet]
        public ActionResult DonateBook()
        {
            ViewBag.GenreID = new SelectList(db.Genre, "Id", "Name");
            return View();
        }

        [Route("Donate")]
        [HttpPost]
        public ActionResult DonateBook([Bind(Include = "Title,GenreId,AuthorId,ISBN,Image,NumBookDonated")] DonateVM model)
        {
            if (ModelState.IsValid)
            {
                //var userId = db.Book.Find(User.Identity.Name);

                var book = new Books
                {

                    GenreId = model.GenreId,
                    AuthorId = model.AuthorId,
                    Title = model.Title,
                    ISBN = model.ISBN,
                    Image = model.Image,
                    QuantityAvailable = model.NumBookDonated

                };

                //db..Add(donatevm);
                db.Book.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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



        public ActionResult About()
        {
            ViewBag.Message = "Red Team Organization Book Exchange.";

            return View();
        }


        // GET: Books/RequestABook
        public ActionResult RequestABook()
        {
            return View();
        }

        // POST: Books/RequestABook
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestABook([Bind(Include = "GenreId,AuthorId,Title,ISBN")] Books books)
        {
            if (ModelState.IsValid)
            {
                db.Book.Add(books);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(books);
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "CONTACT US";

            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string searchString)
        {
            var books = from b in db.Book select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }
            return View(books);
        }

        //public ActionResult DonateBook()
        //{
        //    ViewBag.Message = "Donate a Book";
        //    ViewBag.GenreID = new SelectList(db.Genre, "Id", "Name");


        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DonateBook([Bind(Include = "Id,UserId,GenreId,AuthorId,Title,ISBN,Image,NumBookDonated")] DonateVM donatevm)
        //{
        //    //var userId = db.Users.Find(User.Identity.GetUserId());

        //    //var product = new Product
        //    //{
        //    //    SKU = addProduct.Sku,
        //    //    Name = addProduct.Name,
        //    //    AlertThreshHold = addProduct.AlertThreshHold,
        //    //    CreatedByDate = DateTime.Now,
        //    //    CreatedById = userId,
        //    //    ProductInventories = new List<ProductInventory>()
        //    //};
        //    if (ModelState.IsValid)
        //    {
        //        var userId = db.Book.Find(User.Identity.Name);
        //        var book = new Books
        //        {
        //            UserId = donatevm.UserId,
        //            GenreId = donatevm.GenreId,
        //            AuthorId = donatevm.AuthorId,
        //            Title = donatevm.Title,
        //            ISBN = donatevm.ISBN,
        //            Image = donatevm.Image

        //        };

        //        //db..Add(donatevm);
        //        db.Book.Add(book);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(donatevm);
        //}

            public ActionResult DonateSuccess()
        {
            ViewBag.Message = "Your Donation is successful...Thank you for Donating";

            return View();
        }



    }
}