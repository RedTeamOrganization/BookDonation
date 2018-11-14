using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookDonation.DB.Models;

namespace BookDonation.Web.Controllers
{
    public class HomeController : Controller 
    {
        private BookDonationDB db = new BookDonationDB();

        // GET: Actions
        public ActionResult Index()
        {
            return View(db.Action.ToList());
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
            var books = from b in db.Book select b;

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    books = books.Where(s => s.Title.Contains(searchString));
            //}

            return View(books);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string searchString)
        {
            //Add code here
            return View();
        }
    }
}