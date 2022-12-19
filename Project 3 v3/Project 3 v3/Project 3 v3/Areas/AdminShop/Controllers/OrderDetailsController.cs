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
    public class OrderDetailsController : Controller
    {
        private dataver2Entities db = new dataver2Entities();
        public JsonResult Get()
        {
            var list = db.OrdersDetails.ToList();
            var kq = (from x in list
                      select new
                      {
                          OrdID=x.OrdID,
                          ProID=x.ProID,
                          Quantity=x.Quantity
                      });
            return Json(kq, JsonRequestBehavior.AllowGet);
        }
        // GET: AdminShop/OrderDetails
        public ActionResult Index()
        {
            return View(db.OrdersDetails.ToList());
        }

        // GET: AdminShop/OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersDetail orderDetail = db.OrdersDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: AdminShop/OrderDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminShop/OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(OrdersDetail l)
        {
            var add = db.OrdersDetails.Add(l);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: AdminShop/OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersDetail orderDetail = db.OrdersDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: AdminShop/OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.\
        public JsonResult GetByID(int id)
        {
            var all = db.OrdersDetails.ToList();
            var kq = (from x in all
                      where x.OrdID == id
                      select new
                      {
                          OrdID = x.OrdID,
                          ProID = x.ProID,
                          Quantity = x.Quantity
                      }).FirstOrDefault();
            return Json(kq, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit(OrdersDetail loaimoi)
        {
            var loaicu = db.OrdersDetails.Where(x => x.OrdID == loaimoi.OrdID).FirstOrDefault();

            db.Entry(loaicu).CurrentValues.SetValues(loaimoi);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpDelete]
        public JsonResult DeleteObj(int id)
        {
            var loaicu = db.OrdersDetails.Where(x => x.OrdID == id).FirstOrDefault();
            db.OrdersDetails.Remove(loaicu);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        // GET: AdminShop/OrderDetails/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersDetail orderDetail = db.OrdersDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: AdminShop/OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrdersDetail orderDetail = db.OrdersDetails.Find(id);
            db.OrdersDetails.Remove(orderDetail);
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
