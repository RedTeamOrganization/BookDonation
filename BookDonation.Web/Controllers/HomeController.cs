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
        public ActionResult Index()
        {
            return View(db.Action.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
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
    }
}