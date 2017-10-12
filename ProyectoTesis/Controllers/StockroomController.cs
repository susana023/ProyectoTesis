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
    public class StockroomController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Stockroom
        public ActionResult Index()
        {
            var stockrooms = db.Stockrooms;
            return View(stockrooms.ToList());
        }

        // GET: Stockroom/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stockroom stockroom = db.Stockrooms.Find(id);
            if (stockroom == null)
            {
                return HttpNotFound();
            }
            return View(stockroom);
        }

        // GET: Stockroom/Create
        public ActionResult Create()
        {
            ViewBag.ManagerID = new SelectList(db.Users, "ID", "FullName");
            ViewBag.StoreID = new SelectList(db.Stores, "ID", "Description");
            return View();
        }

        // POST: Stockroom/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, Name,Phone,ActiveFlag,StoreID, ManagerID")] Stockroom stockroom)
        {
            if (ModelState.IsValid)
            {
                db.Stockrooms.Add(stockroom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManagerID = new SelectList(db.Users, "ID", "FullName");
            ViewBag.StoreID = new SelectList(db.Stores, "ID", "Description");
            return View(stockroom);
        }

        // GET: Stockroom/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stockroom stockroom = db.Stockrooms.Find(id);
            if (stockroom == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagerID = new SelectList(db.Users, "ID", "FullName");
            ViewBag.StoreID = new SelectList(db.Stores, "ID", "Description");
            return View(stockroom);
        }

        // POST: Stockroom/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Phone,ActiveFlag")] Stockroom stockroom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockroom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManagerID = new SelectList(db.Users, "ID", "FullName");
            ViewBag.StoreID = new SelectList(db.Stores, "ID", "Description");
            return View(stockroom);
        }

        // GET: Stockroom/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stockroom stockroom = db.Stockrooms.Find(id);
            if (stockroom == null)
            {
                return HttpNotFound();
            }
            return View(stockroom);
        }

        // POST: Stockroom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stockroom stockroom = db.Stockrooms.Find(id);
            db.Stockrooms.Remove(stockroom);
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
