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
    public class CorrelativeController : Controller
    {
        private StoreContext db = new StoreContext();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string user = DAL.GlobalVariables.CurrentUser;

        // GET: Correlative
        public ActionResult Index()
        {
            var correlatives = db.Correlatives.Include(c => c.Store);
            return View(correlatives.ToList());
        }

        // GET: Correlative/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correlative correlative = db.Correlatives.Find(id);
            if (correlative == null)
            {
                return HttpNotFound();
            }
            return View(correlative);
        }

        // GET: Correlative/Create
        public ActionResult Create()
        {
            ViewBag.StoreID = new SelectList(db.Stores, "ID", "Description");
            return View();
        }

        // POST: Correlative/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StoreID,CorrelativeNumber,SerialNumber,DocumentType")] Correlative correlative)
        {
            if (ModelState.IsValid)
            {
                correlative.CorrelativeNumber = 0;
                correlative.ActiveFlag = true;
                db.Correlatives.Add(correlative);
                log.Info("El usuario " + user + " creó el correlativo para el documento: " + correlative.DocumentType.ToString() + " de la tienda: " + db.Stores.Find(correlative.StoreID).Description);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StoreID = new SelectList(db.Stores, "ID", "Description", correlative.StoreID);
            return View(correlative);
        }

        // GET: Correlative/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correlative correlative = db.Correlatives.Find(id);
            if (correlative == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreID = new SelectList(db.Stores, "ID", "Description", correlative.StoreID);
            return View(correlative);
        }

        // POST: Correlative/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StoreID,CorrelativeNumber,SerialNumber,DocumentType")] Correlative correlative)
        {
            if (ModelState.IsValid)
            {
                db.Entry(correlative).State = EntityState.Modified;
                log.Info("El usuario " + user + " editó el correlativo para el documento: " + correlative.DocumentType.ToString() + " de la tienda: " + db.Stores.Find(correlative.StoreID).Description);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StoreID = new SelectList(db.Stores, "ID", "Description", correlative.StoreID);
            return View(correlative);
        }

        // GET: Correlative/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correlative correlative = db.Correlatives.Find(id);
            if (correlative == null)
            {
                return HttpNotFound();
            }
            return View(correlative);
        }

        // POST: Correlative/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Correlative correlative = db.Correlatives.Find(id);
            log.Info("El usuario " + user + " eliminó el correlativo para el documento: " + correlative.DocumentType.ToString() + " de la tienda: " + db.Stores.Find(correlative.StoreID).Description);
            correlative.ActiveFlag = false;
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
