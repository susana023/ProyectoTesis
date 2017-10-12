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
    public class OrderController : Controller
    {
        private double IGV = DAL.GlobalVariables.Igv;

        private StoreContext db = new StoreContext();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string user = DAL.GlobalVariables.CurrentUser;

        // GET: Order
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Client).Include(o => o.User);
            return View(orders.ToList());
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "FullName");
            ViewBag.UserID = new SelectList(db.Users, "ID", "FullName");
            return View();
        }

        // POST: Order/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClientID,UserID,Date,DeliveredFlag,ActiveFlag")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                log.Info("El usuario " + user + " creó un pedido para el cliente: " + db.Clients.Find(order.ClientID));
                return RedirectToAction("Index", "OrderDetail", new { OrderID = order.ID });
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ID", "FullName", order.ClientID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "FullName", order.UserID);
            return View(order);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "Name", order.ClientID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", order.UserID);
            return View(order);
        }

        // POST: Order/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClientID,UserID,Date,DeliveredFlag,ActiveFlag")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "Name", order.ClientID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", order.UserID);
            return View(order);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            OrderDetailController controller = new OrderDetailController();
            foreach (OrderDetail orderDetail in order.OrderDetails)
            {
                controller.deleteOrderDetail(orderDetail.ID);
            }
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void ActualizarTotal(int id)
        {
            Order order = db.Orders.Find(id);
            double subtotal = 0;
            List<OrderDetail> detalles = db.OrderDetails.Where(p => p.OrderID == id).ToList();
            if (detalles != null)
            {
                foreach (OrderDetail detalle in detalles)
                {
                    subtotal += detalle.Subtotal;
                }
                //order.Subtotal = subtotal;
                //order.Igv = subtotal * IGV;
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
