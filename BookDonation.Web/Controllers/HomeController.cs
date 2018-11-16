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
                return RedirectToAction("Index");
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

        [HttpPost]
        public DateTime PickUpDate(DateTime date)
        { DateTime today = DateTime.Today.AddDays(3);

            /*  return DateTime.Now.AddDays(3)*/
            return date.AddDays(3);
        //       //View();
        }


       

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

            var content = db.Book.Where(b => b.ISBN == model.ISBN.Trim()).Select(s => new
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