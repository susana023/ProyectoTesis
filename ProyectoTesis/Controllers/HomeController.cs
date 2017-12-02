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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "";
            return View();
        }

        public void Loging(string userName, string password)
        {
            User user = db.Users.Where(u => u.Username == userName && u.Password == password).FirstOrDefault();
            if(user != null)
            {
                ViewBag.Message = "";
                DAL.GlobalVariables.CurrentUser = userName;
                DAL.GlobalVariables.CurrentUserID = user.ID.ToString();
                RedirectToAction("Index", "HomeController");
            }
            else
            {
                ViewBag.Message = "Usuario o contraseña incorrectos";
                RedirectToAction("Login", "HomeController");
            }
        }
    }
}