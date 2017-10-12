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
    public class PurchaseOrderController : Controller
    {
        private double IGV = DAL.GlobalVariables.Igv;
        private StoreContext db = new StoreContext();

        // GET: PurchaseOrder
        public ActionResult Index()
        {
            var purchaseOrders = db.PurchaseOrders.Include(p => p.Supplier);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "BusinessName");
            return View(purchaseOrders.ToList());
        }

        // GET: PurchaseOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // GET: PurchaseOrder/Create
        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "BusinessName");
            return View();
        }

        // POST: PurchaseOrder/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierID,BillCorrelative,BillSerialNumber,ActiveFlag,Date,Igv,Subtotal")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseOrders.Add(purchaseOrder);
                db.SaveChanges();
                return RedirectToAction("Index", "PurchaseOrderDetail", new { PurchaseOrderID = purchaseOrder.ID });
            }

            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "BusinessName", purchaseOrder.SupplierID);
            return View(purchaseOrder);
        }

        // GET: PurchaseOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "BusinessName", purchaseOrder.SupplierID);
            return View(purchaseOrder);
        }

        // POST: PurchaseOrder/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SupplierID,BillCorrelative,BillSerialNumber,ActiveFlag,Date,Igv,Subtotal")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "BusinessName", purchaseOrder.SupplierID);
            return View(purchaseOrder);
        }

        // GET: PurchaseOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        public ActionResult NewPurchaseOrder()
        {
            return View();
        }

        // POST: PurchaseOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            PurchaseOrderDetailController controller = new PurchaseOrderDetailController();
            foreach (PurchaseOrderDetail purchaseOrderDetail in purchaseOrder.PurchaseOrderDetails)
            {                
                controller.deletePurchaseOrderDetail(purchaseOrderDetail.ID);
            }
            db.PurchaseOrders.Remove(purchaseOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void ActualizarTotal(int id)
        {
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            double subtotal = 0;
            List<PurchaseOrderDetail> detalles = db.PurchaseOrderDetails.Where(p => p.PurchaseOrderID == id).ToList();
            if(detalles != null)
            {
                foreach (PurchaseOrderDetail detalle in detalles)
                {
                    subtotal += detalle.Subtotal;
                }
                purchaseOrder.Subtotal = subtotal;
                purchaseOrder.Igv = subtotal * IGV;
                db.SaveChanges();
            }            
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
