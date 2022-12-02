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
    public class CategoriesController : Controller
    {
        private dataver2Entities db = new dataver2Entities();
        public JsonResult Get()
        {
            var list =db.Categories.ToList();
            var kq = (from x in list
                      select new
                      {
                          CateID = x.CateID,
                          CateName = x.CateName
                      });
            return Json(kq,JsonRequestBehavior.AllowGet);
        }
        // GET: AdminShop/Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: AdminShop/Categories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: AdminShop/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminShop/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(Category l)
        {
            var add = db.Categories.Add(l);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult Create([Bind(Include = "CateID,CateName")] Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Categories.Add(category);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(category);
        //}

        // GET: AdminShop/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: AdminShop/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public JsonResult GetByID(int id)
        {
            var all = db.Categories.ToList();
            var kq = (from x in all
                      where x.CateID == id
                      select new
                      {
                          CateID = x.CateID,
                          CateName = x.CateName
                      }).FirstOrDefault();
            return Json(kq, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit(Category loaimoi)
        {
            var loaicu = db.Categories.Where(x => x.CateID == loaimoi.CateID).FirstOrDefault();

            db.Entry(loaicu).CurrentValues.SetValues(loaimoi);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteObj(int id)
        {
            var loaicu = db.Categories.Where(x => x.CateID == id).FirstOrDefault();
            db.Categories.Remove(loaicu);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: AdminShop/Categories/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: AdminShop/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
