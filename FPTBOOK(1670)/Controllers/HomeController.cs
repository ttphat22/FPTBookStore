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

        
    }
}