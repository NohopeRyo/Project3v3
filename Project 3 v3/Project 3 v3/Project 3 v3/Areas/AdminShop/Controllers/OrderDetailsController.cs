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
            var list = db.OrderDetails.ToList();
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
            return View(db.OrderDetails.ToList());
        }

        // GET: AdminShop/OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
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
        public JsonResult Create(OrderDetail l)
        {
            var add = db.OrderDetails.Add(l);
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
            OrderDetail orderDetail = db.OrderDetails.Find(id);
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
            var all = db.OrderDetails.ToList();
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
        public JsonResult Edit(OrderDetail loaimoi)
        {
            var loaicu = db.OrderDetails.Where(x => x.OrdID == loaimoi.OrdID).FirstOrDefault();

            db.Entry(loaicu).CurrentValues.SetValues(loaimoi);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpDelete]
        public JsonResult DeleteObj(int id)
        {
            var loaicu = db.OrderDetails.Where(x => x.OrdID == id).FirstOrDefault();
            db.OrderDetails.Remove(loaicu);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        // GET: AdminShop/OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
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
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
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
