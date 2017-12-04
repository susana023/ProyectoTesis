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
    public class SaleDocumentController : Controller
    {
        private StoreContext db = new StoreContext();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string user = DAL.GlobalVariables.CurrentUser;

        public ActionResult Ticket()
        {
            var tickets = db.SaleDocuments.Where(s => s.DocumentType == DocumentType.Boleta);
            return View(tickets.ToList());
        }

        public ActionResult Bill()
        {
            var bills = db.SaleDocuments.Where(s => s.DocumentType == DocumentType.Factura);
            return View(bills.ToList());
        }

        public ActionResult CreditNote()
        {
            var creditNotes = db.SaleDocuments.Where(s => s.DocumentType == DocumentType.NotaCrédito);
            return View(creditNotes.ToList());
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
