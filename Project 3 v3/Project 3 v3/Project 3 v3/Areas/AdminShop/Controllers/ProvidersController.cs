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
    public class ProvidersController : Controller
    {
        private dataver2Entities db = new dataver2Entities();
        public JsonResult Get()
        {
            var list = db.Providers.ToList();
            var kq = (from x in list
                      select new
                      {
                          PrvID=x.PrvID,
                          PrvName=x.PrvName,
                          Address=x.Address,
                          Phone=x.Phone,
                          Email=x.Email
                      });
            return Json(kq, JsonRequestBehavior.AllowGet);
        }
        // GET: AdminShop/Providers
        public ActionResult Index()
        {
            return View(db.Providers.ToList());
        }

        // GET: AdminShop/Providers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provider provider = db.Providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        // GET: AdminShop/Providers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminShop/Providers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(Provider l)
        {
            var add = db.Providers.Add(l);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: AdminShop/Providers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provider provider = db.Providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        // POST: AdminShop/Providers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public JsonResult GetByID(int id)
        {
            var all = db.Providers.ToList();
            var kq = (from x in all
                      where x.PrvID == id
                      select new
                      {
                          PrvID = x.PrvID,
                          PrvName = x.PrvName,
                          Address = x.Address,
                          Phone = x.Phone,
                          Email = x.Email
                      }).FirstOrDefault();
            return Json(kq, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit(Provider loaimoi)
        {
            var loaicu = db.Providers.Where(x => x.PrvID == loaimoi.PrvID).FirstOrDefault();

            db.Entry(loaicu).CurrentValues.SetValues(loaimoi);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpDelete]
        public JsonResult DeleteObj(int id)
        {
            var loaicu = db.Providers.Where(x => x.PrvID == id).FirstOrDefault();
            db.Providers.Remove(loaicu);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: AdminShop/Providers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provider provider = db.Providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        // POST: AdminShop/Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Provider provider = db.Providers.Find(id);
            db.Providers.Remove(provider);
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
