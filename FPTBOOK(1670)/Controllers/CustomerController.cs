using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            var phone = collection["Phone"];

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
                kh.Password = HashMD5(password);
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
            var password = HashMD5(collection["Password"]);
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

                if (kh != null)
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

        public ActionResult Update()
        {
            var user = Session["Username"];
            Account objAccount = db.Accounts.ToList().Find(a => a.Username.Equals(user));
            if (objAccount == null)
            {
                return HttpNotFound();
            }
            return View(objAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Account _account)
        {
            
                db.Accounts.Attach(_account);

                db.Entry(_account).Property(a => a.Fullname).IsModified = true;
                db.Entry(_account).Property(a => a.Email).IsModified = true;
                db.Entry(_account).Property(a => a.Phone).IsModified = true;
                db.Entry(_account).Property(a => a.Address).IsModified = true;

                db.SaveChanges();                
            
            return RedirectToAction("Index","Home");
        }

        public ActionResult ChangePass()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(Account account)
        {
            var user = Session["Username"];

            Account objAccount = db.Accounts.ToList().Find(p => p.Username.Equals(user) && p.Password.Equals(HashMD5(account.CurrentPassword)));
            if (objAccount == null)
            {
                ViewBag.Error = "Current Password is incorrect";
                return View();
            }
            if (account.NewPassword != account.ConfirmNewPassword)
            {
                ViewBag.Confirm = "The new password and confirmation new password do not match.";
            }

            else
            {
                objAccount.Password = HashMD5(account.NewPassword);
                

                db.Accounts.Attach(objAccount);
                db.Entry(objAccount).Property(a => a.Password).IsModified = true;
                db.SaveChanges();

            
            }
            return RedirectToAction("Index","Home");
        }

        public static string HashMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }

            return byte2String;
        }
    }
}
