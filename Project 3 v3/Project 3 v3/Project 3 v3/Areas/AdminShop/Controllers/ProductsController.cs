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
    public class ProductsController : Controller
    {
        private dataver2Entities db = new dataver2Entities();
        public JsonResult Get()
        {
            var list = db.Products.ToList();
            var kq = (from x in list
                      select new
                      {
                          ProID=x.ProID,
                          CateID=x.CateID,
                          Title=x.Title,
                          Image=x.Image,
                          Description=x.Description,
                          Quantity=x.Quantity,
                          Price=x.Price,
                          PromotionPrice=x.PromotionPrice
                      });
            return Json(kq, JsonRequestBehavior.AllowGet);
        }
        // GET: AdminShop/Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: AdminShop/Products/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: AdminShop/Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminShop/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public JsonResult Create(Product p)
        {
            var add = db.Products.Add(p);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: AdminShop/Products/Edit/5
        public ActionResult Edit(int id)
        {
            if (id.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: AdminShop/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public JsonResult GetByID(int id)
        {
            var all = db.Products.ToList();
            var kq = (from x in all
                      where x.ProID == id
                      select new
                      {
                          ProID = x.ProID,
                          CateID = x.CateID,
                          Title = x.Title,
                          Image = x.Image,
                          Description = x.Description,
                          Quantity = x.Quantity,
                          Price = x.Price,
                          PromotionPrice = x.PromotionPrice
                      }).FirstOrDefault();
            return Json(kq, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit(Product loaimoi)
        {
            var loaicu = db.Products.Where(x => x.ProID == loaimoi.ProID).FirstOrDefault();

            db.Entry(loaicu).CurrentValues.SetValues(loaimoi);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpDelete]
        public JsonResult DeleteObj(int id)
        {
            var loaicu = db.Products.Where(x => x.ProID == id).FirstOrDefault();
            db.Products.Remove(loaicu);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        // GET: AdminShop/Products/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: AdminShop/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
