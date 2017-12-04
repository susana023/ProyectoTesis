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
    public class ZoneController : Controller
    {
        private StoreContext db = new StoreContext();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string user = DAL.GlobalVariables.CurrentUser;

        // GET: Zone
        public ActionResult Index(int StockroomID)
        {
            var zones = db.Zones.Where(z => z.ActiveFlag == false).Include(z => z.Stockroom);
            //var zones = db.Zones;
            ViewBag.Stockroom = StockroomID;
            return View(zones.ToList());
        }

        // GET: Zone/Details/5
        public ActionResult Details(int? id, int StockroomID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone zone = db.Zones.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            ViewBag.Stockroom = StockroomID;
            return View(zone);
        }

        // GET: Zone/Create
        public ActionResult Create(int StockroomID)
        {
            ViewBag.StockroomID = new SelectList(db.Stockrooms, "ID", "Name");
            ViewBag.Stockroom = StockroomID;
            return View();
        }

        // POST: Zone/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StockroomID,Description")] Zone zone)
        {
            if (ModelState.IsValid)
            {
                db.Zones.Add(zone);
                db.SaveChanges();
                log.Info("El usuario " + user + " creó la zona de almacén: " + zone.Description);
                return RedirectToAction("Index", new { StockroomID = zone.StockroomID });
            }

            ViewBag.StockroomID = new SelectList(db.Stockrooms, "ID", "Name", zone.StockroomID);
            return View(zone);
        }

        // GET: Zone/Edit/5
        public ActionResult Edit(int? id, int StockroomID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone zone = db.Zones.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            ViewBag.StockroomID = new SelectList(db.Stockrooms, "ID", "Name", zone.StockroomID);
            ViewBag.Stockroom = StockroomID;
            return View(zone);
        }

        // POST: Zone/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StockroomID,Description")] Zone zone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zone).State = EntityState.Modified;
                db.SaveChanges();
                log.Info("El usuario " + user + " editó la zona de almacén: " + zone.Description);
                return RedirectToAction("Index", new { StockroomID = zone.StockroomID });
            }
            ViewBag.StockroomID = new SelectList(db.Stockrooms, "ID", "Name", zone.StockroomID);
            return View(zone);
        }

        // GET: Zone/Delete/5
        public ActionResult Delete(int? id, int StockroomID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone zone = db.Zones.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            ViewBag.Stockroom = StockroomID;
            return View(zone);
        }

        // POST: Zone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zone zone = db.Zones.Find(id);
            zone.ActiveFlag = false;
            log.Info("El usuario " + user + " eliminó la zona de almacén: " + zone.Description);
            db.SaveChanges();
            return RedirectToAction("Index", new { StockroomID = zone.StockroomID });
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
