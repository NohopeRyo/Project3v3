using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_3_v3.Models;

namespace Project_3_v3.Areas.AdminShop.Controllers
{
    public class OrdersController : Controller
    {
        private dataver2Entities db = new dataver2Entities();
        public JsonResult Get()
        {
            var list = db.Orders.ToList();
            var kq = (from x in list
                      select new
                      {
                          OrdID=x.OrdID,
                          CusID=x.CusID,
                          OrderDate=x.OrderDate,
                          DeliveryAddress=x.DeliveryAddress,
                          Status=x.Status,
                          TotalPay=x.TotalPay
                      });
            return Json(kq, JsonRequestBehavior.AllowGet);
        } 
        // GET: AdminShop/Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: AdminShop/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: AdminShop/Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminShop/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(Order l)
        {
            var add = db.Orders.Add(l);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: AdminShop/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: AdminShop/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public JsonResult GetByID(int id)
        {
            var all = db.Orders.ToList();
            var kq = (from x in all
                      where x.OrdID == id
                      select new
                      {
                          OrdID = x.OrdID,
                          CusID = x.CusID,
                          OrderDate = x.OrderDate,
                          DeliveryAddress = x.DeliveryAddress,
                          Status = x.Status,
                          TotalPay = x.TotalPay
                      }).FirstOrDefault();
            return Json(kq, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit(Order loaimoi)
        {
            var loaicu = db.Orders.Where(x => x.OrdID == loaimoi.OrdID).FirstOrDefault();

            db.Entry(loaicu).CurrentValues.SetValues(loaimoi);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpDelete]
        public JsonResult DeleteObj(int id)
        {
            var loaicu = db.Orders.Where(x => x.OrdID == id).FirstOrDefault();
            db.Orders.Remove(loaicu);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        // GET: AdminShop/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: AdminShop/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
