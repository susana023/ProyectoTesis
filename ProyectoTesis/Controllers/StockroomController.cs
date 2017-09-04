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
            var stockrooms = db.Stockrooms.Include(s => s.Manager);
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
            ViewBag.ID = new SelectList(db.Users, "ID", "Name");
            return View();
        }

        // POST: Stockroom/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Phone,ActiveFlag")] Stockroom stockroom)
        {
            if (ModelState.IsValid)
            {
                db.Stockrooms.Add(stockroom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Users, "ID", "Name", stockroom.ID);
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
            ViewBag.ID = new SelectList(db.Users, "ID", "Name", stockroom.ID);
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
            ViewBag.ID = new SelectList(db.Users, "ID", "Name", stockroom.ID);
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
