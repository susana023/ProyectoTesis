using ProyectoTesis.DAL;
using ProyectoTesis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoTesis.Controllers
{
    public class HomeController : Controller
    {
        private StoreContext db = new StoreContext();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string user = DAL.GlobalVariables.CurrentUser;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //[HttpGet]
        //public ActionResult Login()
        //{
        //    ViewBag.Message = "";
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("Login")]
        public ActionResult Login([Bind(Include = "userName")] string userName, [Bind(Include = "password")] string password)
        {
            if (userName == null || password == null)
            {
                ViewBag.Message = "";
                return View();
            }
            else
            {
                User user = db.Users.Where(u => u.Username == userName && u.Password == password).FirstOrDefault();
                if (user != null)
                {
                    ViewBag.Message = "";
                    DAL.GlobalVariables.CurrentUser = userName;
                    DAL.GlobalVariables.CurrentUserID = user.ID.ToString();
                    log.Info("El usuario " + userName + " se logueó");
                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.Message = "Usuario o contraseña incorrectos";
                    log.Info("Hubo un intento fallido de logueo con el usuario: " + userName);
                    return RedirectToAction("Login");
                }
            }
        }
    }
}