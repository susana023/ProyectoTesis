using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoTesis.DAL;
using ProyectoTesis.Models;

namespace ProyectoTesis.Controllers
{
    public class SalesMarginsController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: SalesMargins
        public ActionResult Index()
        {
            var saleMargins = db.SaleMargins.Include(s => s.Product).Where(s => s.Product.ActiveFlag == true);
            return View(saleMargins.ToList());
        }

        // GET: SalesMargins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesMargin salesMargin = db.SaleMargins.Find(id);
            if (salesMargin == null)
            {
                return HttpNotFound();
            }
            return View(salesMargin);
        }

        // GET: SalesMargins/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Products, "ID", "Description");
            return View();
        }

        // POST: SalesMargins/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MarketMargin,StoreMargin,DistributionMargin,ActiveFlag")] SalesMargin salesMargin)
        {
            if (ModelState.IsValid)
            {
                db.SaleMargins.Add(salesMargin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Products.Where(s => s.ActiveFlag == true), "ID", "Description", salesMargin.ID);
            return View(salesMargin);
        }

        // GET: SalesMargins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesMargin salesMargin = db.SaleMargins.Find(id);
            if (salesMargin == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Products.Where(s => s.ActiveFlag == true), "ID", "Description", salesMargin.ID);
            return View(salesMargin);
        }

        // POST: SalesMargins/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MarketMargin,StoreMargin,DistributionMargin,ActiveFlag")] SalesMargin salesMargin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesMargin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Products.Where(s => s.ActiveFlag == true), "ID", "Description", salesMargin.ID);
            return View(salesMargin);
        }

        // GET: SalesMargins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesMargin salesMargin = db.SaleMargins.Find(id);
            if (salesMargin == null)
            {
                return HttpNotFound();
            }
            return View(salesMargin);
        }

        // POST: SalesMargins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesMargin salesMargin = db.SaleMargins.Find(id);
            db.SaleMargins.Remove(salesMargin);
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
