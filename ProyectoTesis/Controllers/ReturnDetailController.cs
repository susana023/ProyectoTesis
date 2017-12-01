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
    public class ReturnDetailController : Controller
    {
        private StoreContext db = new StoreContext();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string user = DAL.GlobalVariables.CurrentUser;

        // GET: ReturnDetail
        public ActionResult Index(int returnID)
        {
            var returnDetails = db.ReturnDetails.Include(r => r.Product).Include(r => r.Return).Where(r => r.ReturnID == returnID && r.ActiveFlag == true);
            ViewBag.ReturnID = returnID;
            return View(returnDetails.ToList());
        }

        // GET: ReturnDetail/Details/5
        public ActionResult Details(int? id, int returnID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnDetail returnDetail = db.ReturnDetails.Find(id);
            if (returnDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReturnID = returnID;
            return View(returnDetail);
        }

        // GET: ReturnDetail/Create
        public ActionResult Create(int returnID)
        {
            Return ret = db.Returns.Where(r => r.ID == returnID).FirstOrDefault();
            List<int> products = new List<int>();
            List<int> dets = new List<int>();

            List<ReturnDetail> details = ret.ReturnDetails.ToList();
            if (details != null)
            {
                foreach (ReturnDetail d in details)
                {
                    if(d.ActiveFlag == true) dets.Add(d.ProductID);
                }
            }
            foreach (SaleDocumentDetail s in ret.SaleDocument.SaleDocumentDetails)
            {
                if(dets.Contains(s.ProductID)) products.Add(s.ProductID);
            }
            if (ret != null)
            {
                ViewBag.ProductID = new SelectList(db.Products.Where(p => p.ActiveFlag == true && products.Contains(p.ID)), "ID", "Description");
                ViewBag.ReturnID = returnID;
            }
            return View();
        }

        // POST: ReturnDetail/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ReturnID,ProductID,BoxUnits,FractionUnits,Subtotal,Reason")] ReturnDetail returnDetail)
        {
            if (ModelState.IsValid)
            {
                db.ReturnDetails.Add(returnDetail);
                db.SaveChanges();
                return RedirectToAction("Index", new { ReturnID = returnDetail.ReturnID});
            }
            Return ret = db.Returns.Where(r => r.ID == returnDetail.ReturnID).FirstOrDefault();
            List<int> products = new List<int>();
            List<int> dets = new List<int>();

            List<ReturnDetail> details = ret.ReturnDetails.ToList();
            if (details != null)
            {
                foreach (ReturnDetail d in details)
                {
                    if (d.ActiveFlag == true) dets.Add(d.ProductID);
                }
            }
            foreach (SaleDocumentDetail s in ret.SaleDocument.SaleDocumentDetails)
            {
                if (dets.Contains(s.ProductID)) products.Add(s.ProductID);
            }
            if (ret != null)
            {
                ViewBag.ProductID = new SelectList(db.Products.Where(p => p.ActiveFlag == true && products.Contains(p.ID)), "ID", "Description");
                ViewBag.ReturnID = returnDetail.ReturnID;
            }
            return View(returnDetail);
        }

        // GET: ReturnDetail/Edit/5
        public ActionResult Edit(int? id, int returnID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnDetail returnDetail = db.ReturnDetails.Find(id);
            if (returnDetail == null)
            {
                return HttpNotFound();
            }
            Return ret = db.Returns.Where(r => r.ID == returnID).FirstOrDefault();
            List<int> products = new List<int>();
            List<int> dets = new List<int>();

            List<ReturnDetail> details = ret.ReturnDetails.ToList();
            if (details != null)
            {
                foreach (ReturnDetail d in details)
                {
                    if (d.ActiveFlag == true) dets.Add(d.ProductID);
                }
            }
            foreach (SaleDocumentDetail s in ret.SaleDocument.SaleDocumentDetails)
            {
                if (dets.Contains(s.ProductID)) products.Add(s.ProductID);
            }
            if (ret != null)
            {
                ViewBag.ProductID = new SelectList(db.Products.Where(p => p.ActiveFlag == true && products.Contains(p.ID)), "ID", "Description");
                ViewBag.ReturnID = returnID;
            }
            return View(returnDetail);
        }

        // POST: ReturnDetail/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ReturnID,ProductID,BoxUnits,FractionUnits,Subtotal,Reason")] ReturnDetail returnDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(returnDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { ReturnID = returnDetail.ReturnID });
            }
            Return ret = db.Returns.Where(r => r.ID == returnDetail.ReturnID).FirstOrDefault();
            List<int> products = new List<int>();
            List<int> dets = new List<int>();

            List<ReturnDetail> details = ret.ReturnDetails.ToList();
            if (details != null)
            {
                foreach (ReturnDetail d in details)
                {
                    if (d.ActiveFlag == true) dets.Add(d.ProductID);
                }
            }
            foreach (SaleDocumentDetail s in ret.SaleDocument.SaleDocumentDetails)
            {
                if (dets.Contains(s.ProductID)) products.Add(s.ProductID);
            }
            if (ret != null)
            {
                ViewBag.ProductID = new SelectList(db.Products.Where(p => p.ActiveFlag == true && products.Contains(p.ID)), "ID", "Description");
                ViewBag.ReturnID = returnDetail.ReturnID;
            }
            return View(returnDetail);
        }

        // GET: ReturnDetail/Delete/5
        public ActionResult Delete(int? id, int returnID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnDetail returnDetail = db.ReturnDetails.Find(id);
            if (returnDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReturnID = returnID;
            return View(returnDetail);
        }

        // POST: ReturnDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int returnID)
        {
            ReturnDetail returnDetail = db.ReturnDetails.Find(id);
            returnDetail.ActiveFlag = false;
            db.SaveChanges();
            return RedirectToAction("Index", new { ReturnID = returnDetail.ReturnID });
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
