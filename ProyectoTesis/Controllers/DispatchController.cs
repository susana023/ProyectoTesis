using ProyectoTesis.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoTesis.Controllers
{
    public class DispatchController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Dispatch
        public ActionResult Index()
        {
            var saleDocument = db.SaleDocuments.Where(s => s.DeliveredFlag == false).ToList();
            return View(saleDocument);
        }
    }
}