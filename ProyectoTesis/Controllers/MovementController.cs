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
    public class MovementController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Movement
        public ActionResult Index()
        {
            var movements = db.Movements.Include(m => m.Product).Include(m => m.Zone);
            return View(movements.ToList());
        }

        // GET: Movement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movement movement = db.Movements.Find(id);
            if (movement == null)
            {
                return HttpNotFound();
            }
            return View(movement);
        }

        // GET: Movement/Create
        public ActionResult Create()
        {
            ViewBag.DocumentID = new SelectList(db.Documents, "ID", "ID");
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Description");
            ViewBag.ZoneID = new SelectList(db.Zones, "ID", "Description");
            return View();
        }

        // POST: Movement/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductID,ExpirationDate,ZoneID,MovementType,BoxUnits,FractionUnits,DocumentID")] Movement movement)
        {
            if (ModelState.IsValid)
            {
                db.Movements.Add(movement);
                Product product = db.Products.Find(movement.ProductID);
                double quantity = movement.BoxUnits + (movement.FractionUnits / product.FractionUnits);
                if(movement.MovementType == MovementType.Compra || movement.MovementType == MovementType.Hallazgo)
                {
                    product.PhysicalStock += quantity;
                    product.LogicalStock += quantity;
                }else if(movement.MovementType == MovementType.Despacho || movement.MovementType == MovementType.Pérdida){
                    product.PhysicalStock -= quantity;
                    product.LogicalStock -= quantity;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DocumentID = new SelectList(db.Documents, "ID", "ID", movement.DocumentID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Description", movement.ProductID);
            ViewBag.ZoneID = new SelectList(db.Zones, "ID", "Description", movement.ZoneID);
            return View(movement);
        }

        // GET: Movement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movement movement = db.Movements.Find(id);
            if (movement == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentID = new SelectList(db.Documents, "ID", "ID", movement.DocumentID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Description", movement.ProductID);
            ViewBag.ZoneID = new SelectList(db.Zones, "ID", "Description", movement.ZoneID);
            return View(movement);
        }

        // POST: Movement/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductID,ExpirationDate,ZoneID,MovementType,BoxUnits,FractionUnits,DocumentID")] Movement movement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocumentID = new SelectList(db.Documents, "ID", "ID", movement.DocumentID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Description", movement.ProductID);
            ViewBag.ZoneID = new SelectList(db.Zones, "ID", "Description", movement.ZoneID);
            return View(movement);
        }

        // GET: Movement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movement movement = db.Movements.Find(id);
            if (movement == null)
            {
                return HttpNotFound();
            }
            return View(movement);
        }

        // POST: Movement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movement movement = db.Movements.Find(id);
            db.Movements.Remove(movement);
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

        public void CrearMovimiento(MovementType type, int purchaseOrderID, DateTime expirationDate, int boxes, int fractions, int zoneID, int productID)
        {
            db.Movements.Add(new Movement
            {
                MovementType = type,
                DocumentID = purchaseOrderID,
                ExpirationDate = expirationDate,
                BoxUnits = boxes,
                FractionUnits = fractions,
                ZoneID = zoneID,
                ProductID = productID
            });

            Product product = db.Products.Find(productID);
            double quantity = boxes + ((double)fractions / (double)product.FractionUnits);

            if (type == MovementType.Compra || type == MovementType.Hallazgo)
            {
                product.PhysicalStock += quantity;
                product.LogicalStock += quantity;
            }
            else
            {
                product.PhysicalStock -= quantity;
                product.LogicalStock -= quantity;
            }
            db.SaveChanges();
        }
        public void EditarMovimiento(MovementType type, int documentID, int productID, int boxes, int fractions, int zoneID, DateTime expirationDate)
        {
            Movement movement = db.Movements.Where(m => m.DocumentID == documentID && m.ProductID == productID).FirstOrDefault();
            if (movement != null)
            {
                int oldBoxes = 0, oldFractions = 0;
                oldBoxes = movement.BoxUnits;
                oldFractions = movement.FractionUnits;
                
                movement.ExpirationDate = expirationDate;
                movement.BoxUnits = boxes;
                movement.FractionUnits = fractions;
                movement.ZoneID = zoneID;

                Product product = db.Products.Find(productID);
                double quantity = (boxes - oldBoxes) + ((double)(fractions - oldFractions) / (double)product.FractionUnits);

                if (type == MovementType.Compra || type == MovementType.Hallazgo)
                {
                    product.PhysicalStock += quantity;
                    product.LogicalStock += quantity;
                }
                else
                {
                    product.PhysicalStock -= quantity;
                    product.LogicalStock -= quantity;
                }
                db.SaveChanges();
            }
        }
        public void EliminarMovimiento(int documentID, int productID)
        {
            Movement movement = db.Movements.Where(m => m.DocumentID == documentID && m.ProductID == productID).FirstOrDefault();

            if (movement != null)
            {
                Product product = db.Products.Find(productID);
                int boxes = movement.BoxUnits;
                int fractions = movement.FractionUnits;
                double quantity = boxes + ((double)fractions / (double)product.FractionUnits);

                if (movement.MovementType == MovementType.Compra || movement.MovementType == MovementType.Hallazgo)
                {
                    product.PhysicalStock -= quantity;
                    product.LogicalStock -= quantity;
                }
                else
                {
                    product.PhysicalStock += quantity;
                    product.LogicalStock += quantity;
                }
                db.SaveChanges();
            }
        }
    }
}
