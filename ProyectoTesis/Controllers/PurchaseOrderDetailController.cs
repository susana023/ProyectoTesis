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
    public class PurchaseOrderDetailController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: PurchaseOrderDetail
        public ActionResult Index(int PurchaseOrderID)
        {
            var purchaseOrderDetails = db.PurchaseOrderDetails.Include(p => p.Product).Include(p => p.PurchaseOrder);

            if (PurchaseOrderID != null)
            {
                purchaseOrderDetails = db.PurchaseOrderDetails.Include(p => p.Product).Include(p => p.PurchaseOrder).Where(
                                           p => p.PurchaseOrderID == PurchaseOrderID);
                ViewBag.PurchaseOrder = PurchaseOrderID;
            } 
            return View(purchaseOrderDetails.ToList());
        }

        // GET: PurchaseOrderDetail/Details/5
        public ActionResult Details(int? id, int PurchaseOrderID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderDetail purchaseOrderDetail = db.PurchaseOrderDetails.Find(id);
            if (purchaseOrderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.PurchaseOrder = PurchaseOrderID;
            return View(purchaseOrderDetail);
        }

        // GET: PurchaseOrderDetail/Create
        public ActionResult Create(int PurchaseOrderID)
        {
            
            List<int> products = new List<int>();
            
            foreach (PurchaseOrderDetail p in db.PurchaseOrders.Find(PurchaseOrderID).PurchaseOrderDetails)
            {
                products.Add(p.productID);
            }
            ViewBag.productID = new SelectList(db.Products.Where(p => p.ActiveFlag == true && !products.Contains(p.ID)), "ID", "Description");
            ViewBag.PurchaseOrderID = new SelectList(db.PurchaseOrders.Where(p => p.ID == PurchaseOrderID), "ID", "BillSerialNumber");
            ViewBag.PurchaseOrder = PurchaseOrderID;
            
            return View();
        }

        // POST: PurchaseOrderDetail/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PurchaseOrderID,productID,BoxUnits,FractionUnits,Subtotal")] PurchaseOrderDetail purchaseOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseOrderDetails.Add(purchaseOrderDetail);
                int boxes = 0, fractions = 0;

                if (purchaseOrderDetail.BoxUnits != null) boxes = purchaseOrderDetail.BoxUnits.Value;
                if (purchaseOrderDetail.FractionUnits != null) fractions = purchaseOrderDetail.FractionUnits.Value;

                db.Movements.Add(new Movement
                {
                    MovementType = MovementType.Compra,
                    DocumentID = purchaseOrderDetail.PurchaseOrderID,
                    ExpirationDate = purchaseOrderDetail.BatchExpirationDay,
                    BoxUnits = boxes,
                    FractionUnits = fractions,
                    ZoneID = purchaseOrderDetail.ZoneID.Value,
                    ProductID = purchaseOrderDetail.productID
                });

                Product product = db.Products.Find(purchaseOrderDetail.productID);
                double quantity = boxes + (fractions / product.FractionUnits);
                product.PhysicalStock += quantity;
                product.LogicalStock += quantity;

                db.SaveChanges();

                var controller = DependencyResolver.Current.GetService<PurchaseOrderController>();
                controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                controller.ActualizarTotal(purchaseOrderDetail.PurchaseOrderID);

                return RedirectToAction("Index", new { PurchaseOrderID = purchaseOrderDetail.PurchaseOrderID });
            }

            ViewBag.productID = new SelectList(db.Products, "ID", "Description", purchaseOrderDetail.productID);
            ViewBag.PurchaseOrderID = new SelectList(db.PurchaseOrders, "ID", "BillSerialNumber", purchaseOrderDetail.PurchaseOrderID);
            return View(purchaseOrderDetail);
        }

        // GET: PurchaseOrderDetail/Edit/5
        public ActionResult Edit(int? id, int PurchaseOrderID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderDetail purchaseOrderDetail = db.PurchaseOrderDetails.Find(id);
            if (purchaseOrderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.productID = new SelectList(db.Products, "ID", "Description", purchaseOrderDetail.productID);

            ViewBag.PurchaseOrderID = new SelectList(db.PurchaseOrders.Where(p => p.ID == PurchaseOrderID), "ID", "BillSerialNumber");
            ViewBag.PurchaseOrder = PurchaseOrderID;
            return View(purchaseOrderDetail);
        }

        // POST: PurchaseOrderDetail/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PurchaseOrderID,productID,BoxUnits,FractionUnits,Subtotal")] PurchaseOrderDetail purchaseOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseOrderDetail).State = EntityState.Modified;
                db.SaveChanges();

                var controller = DependencyResolver.Current.GetService<PurchaseOrderController>();
                controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                controller.ActualizarTotal(purchaseOrderDetail.PurchaseOrderID);

                return RedirectToAction("Index", new { PurchaseOrderID = purchaseOrderDetail.PurchaseOrderID });
            }
            ViewBag.productID = new SelectList(db.Products, "ID", "Description", purchaseOrderDetail.productID);
            ViewBag.PurchaseOrderID = new SelectList(db.PurchaseOrders, "ID", "BillSerialNumber", purchaseOrderDetail.PurchaseOrderID);
            return View(purchaseOrderDetail);
        }

        // GET: PurchaseOrderDetail/Delete/5
        public ActionResult Delete(int? id, int PurchaseOrderID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderDetail purchaseOrderDetail = db.PurchaseOrderDetails.Find(id);
            if (purchaseOrderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.PurchaseOrder = PurchaseOrderID;
            return View(purchaseOrderDetail);
        }

        // POST: PurchaseOrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOrderDetail purchaseOrderDetail = db.PurchaseOrderDetails.Find(id);
            if (purchaseOrderDetail != null)
            {
                int purchaseOrderID = purchaseOrderDetail.PurchaseOrderID;
                db.PurchaseOrderDetails.Remove(purchaseOrderDetail);
                db.SaveChanges();
            

            var controller = DependencyResolver.Current.GetService<PurchaseOrderController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            controller.ActualizarTotal(purchaseOrderID);
            }

            return RedirectToAction("Index", new { PurchaseOrderID = purchaseOrderDetail.PurchaseOrderID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public double getBoxPrice(int productID)
        {
            return db.Products.Where(p => p.ID == productID).ToList().FirstOrDefault().BoxPrice;
        }

        public double getFractionPrice(int productID)
        {
            return db.Products.Where(p => p.ID == productID).ToList().FirstOrDefault().FractionPrice;
        }
    }
}
