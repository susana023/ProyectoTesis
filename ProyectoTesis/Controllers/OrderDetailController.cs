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
    public class OrderDetailController : Controller
    {
        private StoreContext db = new StoreContext();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // GET: OrderDetail
        public ActionResult Index(int OrderID)
        {
            var orderDetails = db.OrderDetails.Include(p => p.Product).Include(p => p.Order).Where(
                                        p => p.OrderID == OrderID);
            ViewBag.Order = OrderID;
            return View(db.OrderDetails.ToList());
        }

        // GET: OrderDetail/Details/5
        public ActionResult Details(int? id, int OrderID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Order = OrderID;
            return View(orderDetail);
        }

        // GET: OrderDetail/Create
        public ActionResult Create(int OrderID)
        {
            List<int> products = new List<int>();

            foreach (OrderDetail p in db.Orders.Find(OrderID).OrderDetails)
            {
                products.Add(p.ProductID);
            }
            ViewBag.productID = new SelectList(db.Products.Where(p => p.ActiveFlag == true && !products.Contains(p.ID)), "ID", "Description");
            ViewBag.OrderID = new SelectList(db.Orders.Where(p => p.ID == OrderID), "ID", "BillSerialNumber");
            ViewBag.Order = OrderID;

            return View();
        }

        // POST: OrderDetail/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OrderID,ProductID,Subtotal,BoxUnits,FractionUnits")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                int boxes = 0, fractions = 0, fractionUnits = 1;
                if (orderDetail.BoxUnits != null) boxes = orderDetail.BoxUnits.Value;
                if (orderDetail.FractionUnits != null) fractions = orderDetail.FractionUnits.Value;
                Product product = db.Products.Find(orderDetail.ProductID);

                if (product != null)
                {
                    if (product.FractionUnits > 1) fractionUnits = product.FractionUnits;
                    product.LogicalStock -= ((double)boxes + (double)fractions / (double)fractionUnits);
                }

                db.SaveChanges();

                var controller = DependencyResolver.Current.GetService<OrderController>();
                controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                controller.ActualizarTotal(orderDetail.OrderID);

                return RedirectToAction("Index", new { OrderID = orderDetail.OrderID });
            }
            ViewBag.productID = new SelectList(db.Products, "ID", "Description", orderDetail.ProductID);
            ViewBag.Order = orderDetail.OrderID;
            ViewBag.OrderID = orderDetail.OrderID;
            return View(orderDetail);
        }

        // GET: OrderDetail/Edit/5
        public ActionResult Edit(int? id, int OrderID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.productID = orderDetail.ProductID;
            ViewBag.fractionPrice = orderDetail.Product.FractionPrice;
            ViewBag.boxPrice = orderDetail.Product.BoxPrice;
            ViewBag.OrderID = OrderID;
            return View(orderDetail);
        }

        // POST: OrderDetail/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OrderID,ProductID,Subtotal,BoxUnits,FractionUnits")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();

                int boxes = 0, fractions = 0;
                if (orderDetail.BoxUnits != null) boxes = orderDetail.BoxUnits.Value;
                if (orderDetail.FractionUnits != null) fractions = orderDetail.FractionUnits.Value;

                //Edición

                db.SaveChanges();

                var controller = DependencyResolver.Current.GetService<OrderController>();
                controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                controller.ActualizarTotal(orderDetail.OrderID);
               
                return RedirectToAction("Index", new { OrderID = orderDetail.OrderID });
            }
            ViewBag.productID = new SelectList(db.Products, "ID", "Description", orderDetail.ProductID);
            ViewBag.OrderID = orderDetail.OrderID;
            ViewBag.Order = orderDetail.OrderID;
            return View(orderDetail);
        }

        // GET: OrderDetail/Delete/5
        public ActionResult Delete(int? id, int OrderID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Order = OrderID;
            return View(orderDetail);
        }

        // POST: OrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int OrderID)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail != null)
            {
                int orderID = orderDetail.OrderID;
                var controller = DependencyResolver.Current.GetService<OrderController>();
                controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                controller.ActualizarTotal(orderID);
                deleteOrderDetail(orderDetail.ID);
                db.OrderDetails.Remove(orderDetail);
                db.SaveChanges();
                
            }

            return RedirectToAction("Index", new { PurchaseOrderID = orderDetail.OrderID });
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

        public void deleteOrderDetail(int orderDetailID)
        {
            int boxes = 0, fractions = 0, fractionUnits = 1;
            OrderDetail orderDetail = db.OrderDetails.Find(orderDetailID);
            if (orderDetail != null)
            {
                if (orderDetail.BoxUnits != null) boxes = orderDetail.BoxUnits.Value;
                if (orderDetail.FractionUnits != null) fractions = orderDetail.FractionUnits.Value;
                Product product = db.Products.Find(orderDetail.ProductID);

                if (product != null)
                {
                    if (product.FractionUnits > 1) fractionUnits = product.FractionUnits;
                    product.LogicalStock += ((double)boxes + (double)fractions / (double)fractionUnits);
                }
                db.SaveChanges();
            }
        }
    }
}
