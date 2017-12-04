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
    public class ExpirationTimeController : Controller
    {
        private StoreContext db = new StoreContext();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string user = DAL.GlobalVariables.CurrentUser;

        // GET: ExpirationTime
        public ActionResult Index()
        {
            return View(db.ExpirationTimes.ToList());
        }

        // GET: ExpirationTime/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpirationTime expirationTime = db.ExpirationTimes.Find(id);
            if (expirationTime == null)
            {
                return HttpNotFound();
            }
            return View(expirationTime);
        }

        // GET: ExpirationTime/Create
        public ActionResult Create()
        {
            List<ProductType> productType = new List<ProductType>();
            for(int i = 0; i <= 20; i++)
            {
                if(db.ExpirationTimes.Where(e => e.ProductType == (ProductType)i).Count() <= 0)
                {
                    productType.Add((ProductType)i);
                }
            }
            var itemTypes = (from ProductType i in productType
                             select new SelectListItem { Text = i.ToString(), Value = i.ToString() }).ToList();
            ViewBag.ProductTypes = itemTypes;
            return View();
        }

        // POST: ExpirationTime/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductType,Months")] ExpirationTime expirationTime)
        {
            if (ModelState.IsValid)
            {
                db.ExpirationTimes.Add(expirationTime);
                log.Info("El usuario " + user + " creó en tiempo de expiración para los productos de tipo: " + expirationTime.ProductType.ToString());
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expirationTime);
        }

        // GET: ExpirationTime/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpirationTime expirationTime = db.ExpirationTimes.Find(id);
            if (expirationTime == null)
            {
                return HttpNotFound();
            }
            return View(expirationTime);
        }

        // POST: ExpirationTime/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductType,Months")] ExpirationTime expirationTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expirationTime).State = EntityState.Modified;
                log.Info("El usuario " + user + " editó el tiempo de expiración para el tipo de producto: " + expirationTime.ProductType.ToString());
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expirationTime);
        }

        // GET: ExpirationTime/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpirationTime expirationTime = db.ExpirationTimes.Find(id);
            if (expirationTime == null)
            {
                return HttpNotFound();
            }
            return View(expirationTime);
        }

        // POST: ExpirationTime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpirationTime expirationTime = db.ExpirationTimes.Find(id);
            db.ExpirationTimes.Remove(expirationTime);
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
