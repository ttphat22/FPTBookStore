using FPTBOOK_1670_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FPTBOOK_1670_.Controllers
{
    public class ShoppingCartController : Controller
    {
        private Model1 db = new Model1();

        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult AddtoCart(string id)
        {
            if (Session["Username"] != null)
            {
                var pro = db.Books.SingleOrDefault(s => s.BookID == id);
                if (pro != null)
                {
                    GetCart().Add(pro);
                }
                return RedirectToAction("ShowToCart", "ShoppingCart");
            }

            return View("ErrorCart");
        }

        public ActionResult UpdateQuantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            string id_pro = form["ID_Product"];

            int quantity = int.Parse(form["Quantity"]);

            Book qStock = db.Books.FirstOrDefault(a => a.BookID == id_pro);

            if (quantity > qStock.Quantity)
            {
                return Content("<script>alert('Quantity is larger than our stock');window.location.replace('/');</script>");
            }
            else
            {
                cart.Update_Quantity_Shopping(id_pro, quantity);

            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        public ActionResult Delete(string id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.DeleteCart(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        public ActionResult ShowToCart()
        {
            if (Session["Username"] != null)
            {
                if (Session["Cart"] == null)
                {
                    Response.Write("<script>alert('Cart is empty');window.location='/'</script>");
                }
                Cart cart = Session["Cart"] as Cart;
                return View(cart);
            }
            return View("ErrorCart");
        }

        public PartialViewResult QuantityCart()
        {
            int total_item = 0;

            Cart cart = Session["Cart"] as Cart;

            if (cart != null)
                total_item = cart.TotalQuantity();
            ViewBag.TotalItem = total_item;

            return PartialView("QuantityCart");
        }

        public ActionResult Checkout(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Order _order = new Order();
                _order.OrderDate = DateTime.Now;
                _order.Username = form["checkoutUsername"];
                _order.Address = form["checkoutAddress"];
                _order.Phone = form["checkoutPhone"];
                _order.Status = "Progressing";
                _order.TotalPrice = Convert.ToInt32(form["checkoutTotal"]);
                if (_order.TotalPrice != 0)
                {
                    db.Orders.Add(_order);

                    foreach (var item in cart.Items)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderID = _order.OrderID;
                        orderDetail.BookID = item._shopping_product.BookID;
                        orderDetail.Quantity = item._shopping_quantity;
                        orderDetail.AmountPrice = item._shopping_product.Price * item._shopping_quantity;

                        var pro = db.Books.SingleOrDefault(s => s.BookID == orderDetail.BookID);

                        pro.Quantity -= orderDetail.Quantity;
                        db.Books.Attach(pro);
                        db.Entry(pro).Property(a => a.Quantity).IsModified = true;

                        db.OrderDetails.Add(orderDetail);
                    }
                    db.SaveChanges();
                    cart.ClearCart();
                    return RedirectToAction("orderSuccess", "ShoppingCart");
                }
                else
                {
                    return Content("<script>alert('Please choose book before checkout');window.location.replace('/');</script>");
                }
            }
            catch
            {
                return Content("Error checkout, Check information again");
            }
        }

        public ActionResult orderSuccess()
        {


            return View();

        }


        public ActionResult OrderHistory(string id)
        {
            if (Session["Username"] != null)
            {
                var orderHis = db.Orders.ToList().Where(s => s.Username == id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (orderHis == null)
                {
                    return HttpNotFound();
                }
                return View(orderHis);
            }
            return View("ErrorCart");
        }

        public ActionResult OrderDetail(int? id)
        {
            if (Session["Username"] != null)
            {
                var order = db.Orders.Find(id);
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
            }
            return View("ErrorCart");
        }
    }
}