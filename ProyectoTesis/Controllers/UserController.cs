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
    public class UserController : Controller
    {
        private StoreContext db = new StoreContext();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string user = DAL.GlobalVariables.CurrentUser;

        // GET: User
        public ActionResult Index()
        {
            var users = db.Users.Where(u => u.ActiveFlag == false).Include(u => u.Store);
            return View(users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Stockrooms.Where(s => s.ActiveFlag == false), "ID", "Name");
            ViewBag.StoreID = new SelectList(db.Stores.Where(s => s.ActiveFlag == false), "ID", "Description");
            return View();
        }

        // POST: User/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Dni,Name,LastName,Phone,Username,Email,Password,StoreID,Type")] User user)
        {
            if (ModelState.IsValid)
            {
                user.ActiveFlag = false;
                db.Users.Add(user);
                db.SaveChanges();
                log.Info("El usuario " + user + " creó al usuario: " + user.FullName);
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Stockrooms.Where(s => s.ActiveFlag == false), "ID", "Name");
            ViewBag.StoreID = new SelectList(db.Stores.Where(s => s.ActiveFlag == false), "ID", "Description");
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Stockrooms.Where(s => s.ActiveFlag == false), "ID", "Name");
            ViewBag.StoreID = new SelectList(db.Stores.Where(s => s.ActiveFlag == false), "ID", "Description");
            return View(user);
        }

        // POST: User/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Dni,Name,LastName,Phone,Username,Email,Password,ActiveFlag,StoreID,Type")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                log.Info("El usuario " + user + " editó al usuario: " + user.FullName);
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Stockrooms.Where(s => s.ActiveFlag == false), "ID", "Name");
            ViewBag.StoreID = new SelectList(db.Stores.Where(s => s.ActiveFlag == false), "ID", "Description");
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            user.ActiveFlag = true;
            log.Info("El usuario " + user + " eliminó al usuario: " + user.FullName);
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
