using FPTBOOK_1670_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBOOK_1670_.Controllers
{
    
    public class HomeController : Controller
    {
        private Model1 db = new Model1();
        public ActionResult Index()
        {
          var book = db.Books.ToList();
            return View(book);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Detail(string id)
        {
            var book = db.Books.ToList().Find(b=>b.BookID == id);
            return View(book);
        }

        public ActionResult Search(string Search)
        {
            var books = db.Books.ToList().Where(s => s.BookName.ToUpper().Contains(Search.ToUpper()) ||
                 s.Author.AuthorName.ToUpper().Contains(Search.ToUpper()) ||
                 s.Category.CategoryName.ToUpper().Contains(Search.ToUpper()));

            return View(books);

        }
    }
}