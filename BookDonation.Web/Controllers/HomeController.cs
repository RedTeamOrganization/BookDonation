using BookDonation.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookDonation.Web.ViewModels;
using BookDonation.Web.Repository;
using System.Net;
using BookDonation.Business;
using System.Data.Entity;
using static BookDonation.Web.ViewModels.DonateVM;

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
                genreName = s.Genres.Name,
                authorName = s.Authors.Name,
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
                AuthorId=item.AuthorId,
                GenreName = item.genreName,
                AuthorName = item.authorName,
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


        [HttpGet]
        public ActionResult DonateBook()
        {
            ViewBag.GenreName = new SelectList(db.Genre, "Id", "Name");
            return View();
        }

        [Route("Donate")]
        [HttpPost]
        public ActionResult DonateBook(DonateVM model)
        {
            
            HttpPostedFileBase file = Request.Files["ImageData"];
            HomeRepository service = new HomeRepository();
            int i = service.UploadImageInDataBase(file, model);
            if (i == 1)
            {
                return RedirectToAction("DonateSuccess");
            }
            return View(model);
            
        }

        public ActionResult ClickDonate()
        {
            return View();
        }
        public ActionResult DonateSuccess()
        {
            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Red Team Organization Book Exchange.";

            return View();
        }

        //[HttpPost]
        //public ActionResult PickUpDate()
        //{ DateTime today = DateTime.Now.Date;
        // DateTime DueDate = DateTime.Now.AddDays(3);
        //    ViewBag.Message = "Please Pick Up Your Book By: " + DueDate;
        //    return View();
        //}


       

        public ActionResult Search()
        {
            return View(new BookDonation.DB.Models.Books());
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Search(Books model)
        {
            //List<Books> content = new List<Books>();

            //if (string.IsNullOrWhiteSpace(model.ISBN) == false)
            //{
            //   content  = db.Book.Where(b => b.ISBN == model.ISBN.Trim()).ToList();
            //}

            var content = db.Book.Where(b => b.Title == model.Title.Trim()).Select(s => new
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

                AuthorId = item.AuthorId

            }).ToList();



            return View("SearchResults", donateModel);
        }



        [Route("RequestABook")]
        [HttpGet]
        public ActionResult RequestABook()
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
                AuthorId = item.AuthorId

            }).ToList();
            return View(donateModel);
            }



        // GET: Books/Cart/5
        public ActionResult Cart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //FIND book by bookID
            Books bookRec = db.Book.Find(id);
            if (bookRec == null)
            {
                return HttpNotFound();  //replace with user friendly message, NOT 404 NOT FOUND
            }

            DonateVM vm = new DonateVM();
            vm.GenreId = bookRec.GenreId;
            vm.AuthorId = bookRec.AuthorId;
            vm.Title = bookRec.Title;
            vm.ISBN = bookRec.ISBN;
            vm.Id = bookRec.Id;

            //Decrment the QTY Available
            bookRec.QuantityAvailable -= 1;
            vm.QuantityAvailable = bookRec.QuantityAvailable;
            //vm.QuantityAvailable -= vm.QuantityAvailable;

            //INcrement the QTY Reserved
           // bookRec.QuantityReserved.ToString +=;

            vm.QuantityReserved = bookRec.QuantityReserved;
            //Update the record
            db.Entry(bookRec).State = EntityState.Modified;
            db.SaveChanges();
           
            //ViewBag.PickUpDueDate = BusinessDays.GetDueDate(receivedDatedTime, workdays, PickUpDueDate);
            return View(vm);           
       }

     





        public ActionResult CheckOut()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "CONTACT US";

            return View();
        }
    }
}