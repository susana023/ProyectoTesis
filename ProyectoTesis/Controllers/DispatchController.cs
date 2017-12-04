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

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string user = DAL.GlobalVariables.CurrentUser;

        // GET: Dispatch
        public ActionResult Index()
        {
            var saleDocument = db.SaleDocuments.Where(s => s.DeliveredFlag == false).ToList();
            return View(saleDocument);
        }
    }
}