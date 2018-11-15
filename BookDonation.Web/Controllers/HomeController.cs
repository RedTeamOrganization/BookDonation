using BookDonation.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookDonation.Web.ViewModels;
using BookDonation.Web.Repository;
using System.Net;

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
            Books books = db.Book.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }

           DonateVM vm = new DonateVM();
            vm.GenreId = books.GenreId;
            vm.AuthorId = vm.AuthorId;
            vm.Title = books.Title;
            vm.ISBN = vm.ISBN;


            return View(vm);


            //Calculator cl = new Calculator();
            //ViewBag.Tax = cl.CalTax(movie.Price);
            //ViewBag.Total = cl.CalTotal(movie.Price);
            //return View("~/Views/Movies/Cart.cshtml", movie);
        }




        public ActionResult Contact()
        {
            ViewBag.Message = "CONTACT US";

            return View();
        }
    }
}