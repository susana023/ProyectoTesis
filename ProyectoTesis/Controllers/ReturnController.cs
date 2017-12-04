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
    public class ReturnController : Controller
    {
        private StoreContext db = new StoreContext();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string user = DAL.GlobalVariables.CurrentUser;

        // GET: Return
        public ActionResult Index()
        {
            var returns = db.Returns.Include(r => r.SaleDocument);
            return View(returns.ToList());
        }

        // GET: Return/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Return @return = db.Returns.Find(id);
            if (@return == null)
            {
                return HttpNotFound();
            }
            return View(@return);
        }

        // GET: Return/Create
        public ActionResult Create()
        {
            ViewBag.SaleDocumentID = new SelectList(db.Documents.Where(s => s.ActiveFlag == false), "ID", "SerialNumber");
            return View();
        }

        // POST: Return/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,ActiveFlag,SaleDocumentID,Reason,Subtotal,Igv")] Return @return)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(@return);
                Return ret = db.Returns.Where(r => r.Date == @return.Date && r.SaleDocumentID == @return.SaleDocumentID && r.Reason == @return.Reason).FirstOrDefault();
                db.SaveChanges();
                log.Info("El usuario " + user + " creó la devolución para el pedido con ID: " + @return.SaleDocumentID);

                int returnID = 0;
                if(ret != null) returnID = ret.ID;
                return RedirectToAction("Index", "OrderDetail", new { ReturnID = returnID});
            }
            ViewBag.SaleDocumentID = new SelectList(db.Documents.Where(s => s.ActiveFlag == false), "ID", "SerialNumber", @return.SaleDocumentID);
            return View(@return);
        }

        // GET: Return/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Return @return = db.Returns.Find(id);
            if (@return == null)
            {
                return HttpNotFound();
            }
            ViewBag.SaleDocumentID = new SelectList(db.Documents.Where(s => s.ActiveFlag == false), "ID", "SerialNumber", @return.SaleDocumentID);
            return View(@return);
        }

        // POST: Return/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,ActiveFlag,SaleDocumentID,Reason,Subtotal,Igv")] Return @return)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@return).State = EntityState.Modified;
                db.SaveChanges();
                log.Info("El usuario " + user + " editó la devolución con ID: " + @return.ID + " para el pedido con ID: " + @return.SaleDocumentID);
                return RedirectToAction("Index");
            }
            ViewBag.SaleDocumentID = new SelectList(db.Documents.Where(s => s.ActiveFlag == false), "ID", "SerialNumber", @return.SaleDocumentID);
            return View(@return);
        }

        // GET: Return/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Return @return = db.Returns.Find(id);
            if (@return == null)
            {
                return HttpNotFound();
            }
            return View(@return);
        }

        // POST: Return/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Return @return = db.Returns.Find(id);
            db.Documents.Remove(@return);
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
