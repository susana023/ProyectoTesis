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

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string user = DAL.GlobalVariables.CurrentUser;

        // GET: PurchaseOrderDetail
        public ActionResult Index(int PurchaseOrderID)
        {
            var purchaseOrderDetails = db.PurchaseOrderDetails.Include(p => p.Product).Include(p => p.PurchaseOrder).Where(
                                        p => p.PurchaseOrderID == PurchaseOrderID && p.ActiveFlag == false);
            ViewBag.PurchaseOrder = PurchaseOrderID;

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
            ViewBag.ZoneID = new SelectList(db.Zones, "ID", "Description");
            ViewBag.PurchaseOrder = PurchaseOrderID;
            
            return View();
        }

        // POST: PurchaseOrderDetail/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PurchaseOrderID,productID,BoxUnits,FractionUnits,ZoneID,Subtotal,BatchExpirationDay")] PurchaseOrderDetail purchaseOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseOrderDetails.Add(purchaseOrderDetail);
                int boxes = 0, fractions = 0;
                if (purchaseOrderDetail.BatchExpirationDay == null) purchaseOrderDetail.BatchExpirationDay = DateTime.Today;
                if (purchaseOrderDetail.BoxUnits != null) boxes = purchaseOrderDetail.BoxUnits.Value;
                if (purchaseOrderDetail.FractionUnits != null) fractions = purchaseOrderDetail.FractionUnits.Value;

                MovementController movementController = new MovementController();
                movementController.CrearMovimiento(MovementType.Compra, purchaseOrderDetail.PurchaseOrderID, purchaseOrderDetail.BatchExpirationDay.Value, boxes, fractions, purchaseOrderDetail.ZoneID.Value, purchaseOrderDetail.productID);
                int zoneID = db.Zones.FirstOrDefault().ID, boxUnits = 0, fractionUnits = 0;
                DateTime ExpirationDate = DateTime.Today;
                if (purchaseOrderDetail.ZoneID.HasValue) zoneID = purchaseOrderDetail.ZoneID.Value;
                if (purchaseOrderDetail.BoxUnits.HasValue) boxUnits = purchaseOrderDetail.BoxUnits.Value;
                if (purchaseOrderDetail.FractionUnits.HasValue) fractionUnits = purchaseOrderDetail.FractionUnits.Value;
                if (purchaseOrderDetail.BatchExpirationDay.HasValue) ExpirationDate = purchaseOrderDetail.BatchExpirationDay.Value;
                ProductInZone productBatch = new ProductInZone()
                {
                    ProductID = purchaseOrderDetail.productID,
                    ZoneID = zoneID,
                    ExpirationDate = ExpirationDate,
                    BoxUnits = boxUnits,
                    FractionUnits = fractionUnits
                };
                db.ProductsInZone.Add(productBatch);
                db.SaveChanges();

                var controller = DependencyResolver.Current.GetService<PurchaseOrderController>();
                controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                controller.ActualizarTotal(purchaseOrderDetail.PurchaseOrderID);
                log.Info("El usuario " + user + " agregó el producto: " + db.Products.Find(purchaseOrderDetail.productID).Description + " a la orden de compra con ID: " + purchaseOrderDetail.PurchaseOrderID);

                return RedirectToAction("Index", new { PurchaseOrderID = purchaseOrderDetail.PurchaseOrderID });
            }
            ViewBag.ZoneID = new SelectList(db.Zones, "ID", "Description");
            ViewBag.productID = new SelectList(db.Products, "ID", "Description", purchaseOrderDetail.productID);
            ViewBag.PurchaseOrderID = purchaseOrderDetail.PurchaseOrderID;
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
            ViewBag.productID = purchaseOrderDetail.productID;
            ViewBag.ZoneID = purchaseOrderDetail.ZoneID;
            ViewBag.Zone = new SelectList(db.Zones, "ID", "Description");
            ViewBag.fractionPrice = purchaseOrderDetail.Product.FractionPrice;
            ViewBag.boxPrice = purchaseOrderDetail.Product.BoxPrice;
            ViewBag.PurchaseOrderID = PurchaseOrderID;
            return View(purchaseOrderDetail);
        }

        // POST: PurchaseOrderDetail/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PurchaseOrderID,productID,BoxUnits,FractionUnits,ZoneID,Subtotal,BatchExpirationDay")] PurchaseOrderDetail purchaseOrderDetail)
        {
            if (ModelState.IsValid)
            {                
                db.Entry(purchaseOrderDetail).State = EntityState.Modified;
                db.SaveChanges();

                int boxes = 0, fractions = 0;
                if (purchaseOrderDetail.BoxUnits != null) boxes = purchaseOrderDetail.BoxUnits.Value;
                if (purchaseOrderDetail.FractionUnits != null) fractions = purchaseOrderDetail.FractionUnits.Value;

                MovementController movementController = new MovementController();
                purchaseOrderDetail = db.PurchaseOrderDetails.Find(purchaseOrderDetail.ID);
                movementController.EditarMovimiento(MovementType.Compra, purchaseOrderDetail.PurchaseOrderID, purchaseOrderDetail.productID, boxes, fractions, purchaseOrderDetail.ZoneID.Value, purchaseOrderDetail.BatchExpirationDay.Value);

                int zoneID = db.Zones.FirstOrDefault().ID, boxUnits = 0, fractionUnits = 0;
                DateTime ExpirationDate = DateTime.Today;
                if (purchaseOrderDetail.ZoneID.HasValue) zoneID = purchaseOrderDetail.ZoneID.Value;
                if (purchaseOrderDetail.BoxUnits.HasValue) boxUnits = purchaseOrderDetail.BoxUnits.Value;
                if (purchaseOrderDetail.FractionUnits.HasValue) fractionUnits = purchaseOrderDetail.FractionUnits.Value;
                if (purchaseOrderDetail.BatchExpirationDay.HasValue) ExpirationDate = purchaseOrderDetail.BatchExpirationDay.Value;
                ProductInZone productBatch = db.ProductsInZone.Where(p => p.ExpirationDate == ExpirationDate && p.ZoneID == zoneID && p.ProductID == purchaseOrderDetail.productID).FirstOrDefault();
                if (productBatch != null)
                {
                    productBatch.ZoneID = zoneID;
                    productBatch.ExpirationDate = ExpirationDate;
                    productBatch.BoxUnits = boxUnits;
                    productBatch.FractionUnits = fractionUnits;
                }
                log.Info("El usuario " + user + " editó el producto: " + db.Products.Find(purchaseOrderDetail.productID).Description + " de la orden de compra con ID: " + purchaseOrderDetail.PurchaseOrderID);

                db.SaveChanges();

                var controller = DependencyResolver.Current.GetService<PurchaseOrderController>();
                controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                controller.ActualizarTotal(purchaseOrderDetail.PurchaseOrderID);

                return RedirectToAction("Index", new { PurchaseOrderID = purchaseOrderDetail.PurchaseOrderID });
            }
            ViewBag.ZoneID = new SelectList(db.Zones, "ID", "Description");
            ViewBag.productID = new SelectList(db.Products, "ID", "Description", purchaseOrderDetail.productID);
            ViewBag.PurchaseOrderID = purchaseOrderDetail.PurchaseOrderID;
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

                deletePurchaseOrderDetail(id);

                purchaseOrderDetail.ActiveFlag = true;
                
                db.SaveChanges();
                log.Info("El usuario " + user + " eliminó el producto: " + db.Products.Find(purchaseOrderDetail.productID).Description + " de la orden de compra con ID: " + purchaseOrderDetail.PurchaseOrderID);


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

        public void deletePurchaseOrderDetail(int purchaseOrderDetailID)
        {
            PurchaseOrderDetail purchaseOrderDetail = db.PurchaseOrderDetails.Find(purchaseOrderDetailID);
            if (purchaseOrderDetail != null)
            {
                MovementController movementController = new MovementController();
                movementController.EliminarMovimiento(purchaseOrderDetail.PurchaseOrderID, purchaseOrderDetail.productID);

                int zoneID = db.Zones.FirstOrDefault().ID, boxUnits = 0, fractionUnits = 0;
                DateTime ExpirationDate = DateTime.Today;
                if (purchaseOrderDetail.ZoneID.HasValue) zoneID = purchaseOrderDetail.ZoneID.Value;
                if (purchaseOrderDetail.BoxUnits.HasValue) boxUnits = purchaseOrderDetail.BoxUnits.Value;
                if (purchaseOrderDetail.FractionUnits.HasValue) fractionUnits = purchaseOrderDetail.FractionUnits.Value;
                if (purchaseOrderDetail.BatchExpirationDay.HasValue) ExpirationDate = purchaseOrderDetail.BatchExpirationDay.Value;
                ProductInZone productBatch = db.ProductsInZone.Where(p => p.ExpirationDate == ExpirationDate && p.ZoneID == zoneID && p.ProductID == purchaseOrderDetail.productID).FirstOrDefault();
                if (productBatch != null) db.ProductsInZone.Remove(productBatch);
            }
            db.SaveChanges();
        }
    }
}
