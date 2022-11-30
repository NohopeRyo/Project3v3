using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_3_v3.Models;

namespace Project_3_v3.Controllers
{
    public class HomeController : Controller
    {
        dataver2Entities db = new dataver2Entities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Category()
        {
            var ds = db.Products.ToList();
            return View(ds);
        }
        public ActionResult Detail(int id)
        {
            var ds = db.Products.FirstOrDefault(s=>s.ProID==id);
            return View(ds);
        }
        [HttpPost]
        //public JsonResult Create(int id)
        //{
        //    var cart = (List<Order>)Session["SessionCart"] ?? new List<Order>();
        //    if (cart.Any(x => x.OrderDetail == id))
        //    {
        //        foreach (var item in cart)
        //        {
        //            if (item.ProductID == id)
        //            {
        //                item.Quantity += 1;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Cart newItem = new Cart();
        //        newItem.ProductID = id;
        //        newItem.Product = _productService.GetByID(id);
        //        newItem.Quantity = 1;
        //        cart.Add(newItem);
        //    }
        //    Session["SessionCart"] = cart;
        //    return Json("OK", JsonRequestBehavior.AllowGet);
        //}
        public ActionResult AddCart(int id)
        {
            var spt = db.Products.FirstOrDefault(s => s.ProID== id);
            if (Session["cart"] != null)
            {
                List<OrderDetail> cart = (List<OrderDetail>)Session["cart"];

                var kt = cart.FirstOrDefault(s => s.ProID == id);
                if (kt == null)
                {
                    OrderDetail sp = new OrderDetail() { ProID = id, Quantity = 1, Product = spt };
                    cart.Add(sp);
                    // Session["cart"] = cart;
                }
                else
                {
                    kt.Quantity = kt.Quantity + 1;
                }
                Session["cart"] = cart;
            }
            else
            {
                List<OrderDetail> cart = new List<OrderDetail>();
                OrderDetail sp = new OrderDetail() { ProID = id, Quantity = 1, Product = spt };
                cart.Add(sp);
                Session["cart"] = cart;
            }

            return RedirectToAction("Index");
        }


        public ActionResult viewCart()
        {
            List<Order> ds = (List<Order>)Session["cart"]?? new List<Order>();
            List<Order> ds1 = new List<Order>();
            foreach (var d in ds)
            {
                ds1.Add(d);
            }
            return View(ds1);
        }
    }
}