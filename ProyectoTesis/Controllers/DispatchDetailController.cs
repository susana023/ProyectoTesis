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

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string user = DAL.GlobalVariables.CurrentUser;

        // GET: DispatchDetail
        public ActionResult Index(int dispatchID)
        {
            var movements = db.Movements.Where(m => m.DocumentID == dispatchID && m.ActiveFlag == false).ToList();
            return View(movements);
        }
    }
}