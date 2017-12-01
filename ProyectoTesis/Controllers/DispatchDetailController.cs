using ProyectoTesis.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoTesis.Controllers
{
    public class DispatchDetailController : Controller
    {
        private StoreContext db = new StoreContext();
        // GET: DispatchDetail
        public ActionResult Index(int dispatchID)
        {
            var movements = db.Movements.Where(m => m.DocumentID == dispatchID).ToList();
            return View(movements);
        }
    }
}