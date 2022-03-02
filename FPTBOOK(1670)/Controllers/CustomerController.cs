using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPTBOOK_1670_.Models;

namespace MVC_FPT.Controllers
    {
        public class UserController : Controller
        {
            //Tao 1 doi tuong quan ly CSDL
            Model1 db = new Model1();

            // GET: User
            public ActionResult Index()
            {
                return View();
            }

            [HttpGet]
            public ActionResult Register()
            {
                return View();
            }

            [HttpPost]
            public ActionResult Register(FormCollection collection, Account kh)
            {
                //gan gia tri vao form
                var username = collection["Username"];
                var fullname = collection["FullName"];
                var email = collection["Email"];
                var password = collection["Password"];
                var address = collection["Address"];
                var phone =  collection["Phone"];

                if (String.IsNullOrEmpty(username))
                {
                    ViewData["Error1"] = "Full name must be not blank ";
                }
                if (String.IsNullOrEmpty(fullname))
                {
                    ViewData["Error1"] = "Full name must be not blank ";
                }
                if (String.IsNullOrEmpty(email))
                {
                    ViewData["Error2"] = "Email must be not blank ";
                }
                if (String.IsNullOrEmpty(password))
                {
                    ViewData["Error3"] = "Password must be not blank ";
                }
                if (String.IsNullOrEmpty(address))
                {
                    ViewData["Error4"] = "Address must be not blank ";
                }
                if (String.IsNullOrEmpty(phone))
                {
                     ViewData["Error5"] = "Phone must be not blank ";
                }
            else
                {
                //gan gia tri vao Data
                kh.Fullname = fullname;
                kh.Username = username;
                    kh.Email = email;
                    kh.Password = password;
                    kh.Address = address;
                    kh.Phone = phone;
                    db.Accounts.Add(kh);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }

                return this.Register();
            }

            [HttpGet]
            public ActionResult Login()
            {
                return View();
            }
            [HttpPost]
            public ActionResult Login(FormCollection collection)
            {
                var username = collection["Username"];
                var password = collection["Password"];
                if (String.IsNullOrEmpty(username))
                {
                    ViewData["Error1"] = "Email must be not blank ";
                }
                else if (String.IsNullOrEmpty(password))
                {
                    ViewData["Error2"] = "Password must be not blank ";
                }
                else
                {
                    //gan gia tri va lay session
                    Account kh = db.Accounts.SingleOrDefault(x => x.Username == username && x.Password == password);
                    
                    //if (kh != null)
                    //{
                    //    ViewBag.Notification = "You Login successfully";
                    //    Session["email"] = kh.email;
                    //    return RedirectToAction("Index", "Home");
                    //}
                    //else
                    //{
                        
                    //    ViewBag.Notification = "Incorrect email or password";
                    //}

                if (kh!= null)
                {
                    if (kh.State == 0)
                    {
                        Session["Username"] = kh.Username;

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Session["Admin"] = kh.Username;                       

                        return RedirectToAction("Index", "AdminAccount");
                    }
                }
            }
                return View();
            }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        }
    }
