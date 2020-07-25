using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetShop.Models;

namespace PetShop.Controllers
{
    public class ShoppingCartsController : Controller
    {
        DBPetShopEntities db = new DBPetShopEntities();
        // GET: ShoppingCarts
        public ActionResult Index()
        {
            List<Cart> cart = Session["cart"] as List<Cart>;
            return View(cart);
        }
        public ActionResult AddItem(int? id)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Cart>();
                Session["count"] = "Trống";
            }

            List<Cart> cart = Session["cart"] as List<Cart>;

            if (cart.FirstOrDefault(t => t.IDPet == id) == null)
            {
                Pet pet = db.Pets.Find(id);
                if (Session["count"].ToString() == "Trống")
                {
                    Session["count"] = 0;
                }
                Session["count"] = Int32.Parse(Session["count"].ToString()) + 1;
                Cart newItem = new Cart()
                {
                    IDPet = pet.IDPet,
                    PetName = pet.PetName,
                    Quanity = 1,
                    Image = pet.Image,
                    Price = (decimal)pet.Price
                };
                cart.Add(newItem);
            }
            else
            {
                Cart crt = cart.FirstOrDefault(t => t.IDPet == id);
                crt.Quanity++;
            }
            Session["GiaTien"] = cart.Sum(m => m.Total).ToString("#,##0").Replace(',', '.');
            return Redirect(Request.UrlReferrer.ToString());
        }
        public RedirectToRouteResult EditQuanity(int? id, int newQuanity)
        {
            List<Cart> cart = Session["cart"] as List<Cart>;
            if (cart.FirstOrDefault(t => t.IDPet == id) != null)
            {
                cart.FirstOrDefault(t => t.IDPet == id).Quanity = newQuanity;
            }
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult RemoveItem(int? id)
        {
            List<Cart> cart = Session["cart"] as List<Cart>;
            if (cart.FirstOrDefault(t => t.IDPet == id) != null)
            {
                cart.Remove(cart.FirstOrDefault(t => t.IDPet == id));
                Session["count"] = Int32.Parse(Session["count"].ToString()) - 1;
                if (Int32.Parse(Session["count"].ToString()) == 0)
                {
                    Session["count"] = "Trống";
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Pay()
        {
            List<Cart> cart = Session["cart"] as List<Cart>;
            User cus = (User)Session["user"];
            Order dh = new Order();
            dh.User = cus;
            dh.PhoneNumber = cus.PhoneNumber;
            dh.Address = cus.Address;
            dh.DateOrder = DateTime.Now;
            var i = cart;
            dh.TotalMoney = (decimal)i.Sum(t => t.Total);
            return View(dh);
        }
        [HttpPost]
        public RedirectToRouteResult Pay(Order dh)
        {
            if (ModelState.IsValid)
            {
                dh.Status = false;
                User kh = (User)Session["flag"];
                dh.IDUser = kh.IDUser;
                db.Orders.Add(dh);
                db.SaveChanges(); 
                return RedirectToAction("Order", new { id = dh.IDOrder });
            }
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult Order(int id)
        {
            List<Cart> cart = Session["cart"] as List<Cart>;
            foreach (var i in cart)
            {
                OrderDetail odd = new OrderDetail();
                odd.IDOrder = id;
                odd.IDPet = i.IDPet;
                odd.Quantity = i.Quanity;
                odd.TotalMoney = (decimal)i.Total;
                db.OrderDetails.Add(odd);
                bool tmp = db.Tops.FirstOrDefault(t => t.IDPet == i.IDPet) != null;
                if(tmp == false)
                {
                    Top top = new Top();
                    top.IDPet = i.IDPet;
                    top.Quanity = i.Quanity;
                    db.Tops.Add(top);
                }
                else
                {
                    db.Tops.FirstOrDefault(t => t.IDPet == i.IDPet).Quanity += i.Quanity;
                }
            }
            db.SaveChanges();
            Session["cart"] = null;
            Session["count"] = "Trống";
            return RedirectToAction("Index");
        }
    }
}