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
    public class PurchasePlanController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: PurchasePlan
        public ActionResult Index()
        {
            return View(db.PurchasePlans.ToList());
        }

        // GET: PurchasePlan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasePlan purchasePlan = db.PurchasePlans.Find(id);
            if (purchasePlan == null)
            {
                return HttpNotFound();
            }
            return View(purchasePlan);
        }

        // GET: PurchasePlan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurchasePlan/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,Investment")] PurchasePlan purchasePlan)
        {
            if (ModelState.IsValid)
            {
                db.PurchasePlans.Add(purchasePlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(purchasePlan);
        }

        // GET: PurchasePlan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasePlan purchasePlan = db.PurchasePlans.Find(id);
            if (purchasePlan == null)
            {
                return HttpNotFound();
            }
            return View(purchasePlan);
        }

        // POST: PurchasePlan/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,Investment")] PurchasePlan purchasePlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchasePlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchasePlan);
        }

        // GET: PurchasePlan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasePlan purchasePlan = db.PurchasePlans.Find(id);
            if (purchasePlan == null)
            {
                return HttpNotFound();
            }
            return View(purchasePlan);
        }

        // POST: PurchasePlan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchasePlan purchasePlan = db.PurchasePlans.Find(id);
            db.PurchasePlans.Remove(purchasePlan);
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
