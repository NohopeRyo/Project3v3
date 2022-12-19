using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_3_v3.Models;

namespace Project_3_v3.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        dataver2Entities db = new dataver2Entities();
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Session["giohang"] == null)
            {
                ViewBag.giohangcount = 0;
            }
            else
            {
                List<Giohang> gh = (List<Giohang>)Session["giohang"];
                ViewBag.giohangcount = gh.Count;
            }
            return View();
        }
        public ActionResult Category()
        {
            var ds = db.Products.ToList();
            if (Session["giohang"] == null)
            {
                ViewBag.giohangcount = 0;
            }
            else
            {
                List<Giohang> gh = (List<Giohang>)Session["giohang"];
                ViewBag.giohangcount = gh.Count;
            }
            return View(ds);
        }
        public ActionResult Detail(int id)
        {
            var ds = db.Products.FirstOrDefault(s=>s.ProID==id);
            if (Session["giohang"] == null)
            {
                ViewBag.giohangcount = 0;
            }
            else
            {
                List<Giohang> gh = (List<Giohang>)Session["giohang"];
                ViewBag.giohangcount = gh.Count;
            }
            return View(ds);
        }
        public ActionResult Checkout() {
            return View();
        }
        //[HttpPost]
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
        public ActionResult Buy(int ID)
        {
            if (Session["giohang"]==null)
            {
                Giohang a = new Giohang();
                var sp = db.Products.Where(s=>s.ProID==ID).Single();
                a.ID = sp.ProID;
                a.Name = sp.Title;
                a.Quantity = 1;
                a.Price =(int)sp.Price;
                a.Image = sp.Image;
                List<Giohang> gh=new List<Giohang>();
                gh.Add(a);
                Session["giohang"]=gh;
            }   
            else
            {
                List<Giohang> gh = (List<Giohang>)Session["giohang"];
                var test =gh.Find(s=>s.ID==ID);
                if (test == null)
                {
                    Giohang a = new Giohang();
                    var sp = db.Products.Where(s => s.ProID == ID).Single();
                    a.ID = sp.ProID;
                    a.Name = sp.Title;
                    a.Quantity = 1;
                    a.Price = (int)sp.Price;
                    a.Image = sp.Image;
                    gh.Add(a);
                }
                else
                {
                    test.Quantity = test.Quantity++;
                }
                Session["giohang"] = gh;
            }    
            return RedirectToAction("Index");
        }
        public ActionResult viewCart()
        {
            List<Giohang> gh = (List<Giohang>)Session["giohang"];
            return View(gh);
        }
        private int TotalQuantity()
        {
            int total = 0;
            List<Giohang> lstGiohang = Session["giohang"] as List<Giohang>;
            if(lstGiohang!=null)
            {
                total = (int)lstGiohang.Sum(n=>n.Quantity);
            }
            return total;
        }
         private double TotalPay()
        {
            double total = 0;
            List<Giohang>lstgiohang = Session["Giohang"] as List<Giohang>;
            if(lstgiohang!=null)
            {

                total = (double)lstgiohang.Sum(n => n.Total);
            }
            return total;
        }

    }
    
}