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
    public class PurchasePlanController : Controller
    {
        private StoreContext db = new StoreContext();
        private double A = 0.1;

        // GET: PurchasePlan
        public ActionResult Index()
        {
            return View(db.PurchasePlans.ToList());
        }

        // GET: PurchasePlan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasePlan purchasePlan = db.PurchasePlans.Find(id);
            if (purchasePlan == null)
            {
                return HttpNotFound();
            }
            ViewBag.Details = new List<PurchasePlanDetail>(db.PurchasePlanDetails.Where(p => p.PurchasePlanID == id));
            return View(purchasePlan);
        }

        // GET: PurchasePlan/Create
        public ActionResult Create()
        {
            ViewBag.Products = new SelectList(db.Products, "ID", "Description"); 
            return View();
        }

        // POST: PurchasePlan/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BeginDate, EndDate,Investment")] PurchasePlan purchasePlan)
        {
            if (ModelState.IsValid)
            {
                db.PurchasePlans.Add(purchasePlan);
                db.SaveChanges();
                int ID = db.PurchasePlans.OrderByDescending(p => p.ID).FirstOrDefault().ID;
                
                Algorithm(purchasePlan.BeginDate, purchasePlan.EndDate, purchasePlan.Investment, ID);
                return RedirectToAction("Details", new { id = ID });
            }
            return View(purchasePlan);
        }

        // GET: PurchasePlan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasePlan purchasePlan = db.PurchasePlans.Find(id);
            if (purchasePlan == null)
            {
                return HttpNotFound();
            }
            return View(purchasePlan);
        }

        // POST: PurchasePlan/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,Investment")] PurchasePlan purchasePlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchasePlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchasePlan);
        }

        // GET: PurchasePlan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasePlan purchasePlan = db.PurchasePlans.Find(id);
            if (purchasePlan == null)
            {
                return HttpNotFound();
            }
            return View(purchasePlan);
        }

        // POST: PurchasePlan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchasePlan purchasePlan = db.PurchasePlans.Find(id);
            db.PurchasePlans.Remove(purchasePlan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void Algorithm(DateTime beginDate, DateTime endDate, double invesment, int purchasePlanID)
        {
            List<Product> products = db.Products.ToList();
            List<Solution> bests = new List<Solution>();
            double bestValue = 0.0;
            Solution best = new Solution();
            int j = 0, k = 0;
            //falta calcular máxima cantidad por producto en ventas históricas
            List<double> maxBoxes = MaxBoxes(products, beginDate, endDate);

            List<Solution> Childs;
            double value;

            foreach(double maxBox in maxBoxes)
            {
                best.MaxValue.Add(maxBox);
                best.MinValue.Add(0.0);
                j++;
            }

            bests.Add(best);

            while (bests.Count >= (k + 1))
            {
                Childs = GenerateChilds(bests[k], products, maxBoxes);
                for(int i = 0; i < Childs.Count; i++)
                {
                    value = WholeValue(Childs[i], products, maxBoxes);
                    if (Math.Abs(value - bestValue) > 0.0001)
                    {
                        if (value > bestValue)
                        {
                            if (Cost(Childs[i], products) <= invesment)
                            {
                                bests.Add(Childs[i]);
                                bestValue = value;
                            }
                        }
                    }
                }
                k++;
            }
            SaveResults(bests[k - 1], products, purchasePlanID);            
        }

        public void SaveResults(Solution best, List<Product> products, int purchasePlanID)
        {
            for (int i = 0; i < products.Count; i++)
            {
                double margin, mMargin = 0.0, sMargin = 0.0, dMargin = 0.0, units, benefit, quantity;
                int fractionUnits, boxUnits;

                List < SalesMargin > margins = db.SaleMargins.ToList();
                SalesMargin productMargins = margins.Where(m => m.Product.ID == products[i].ID).FirstOrDefault();

                if (productMargins != null)
                {
                    mMargin = productMargins.MarketMargin;
                    sMargin = productMargins.StoreMargin;
                    dMargin = productMargins.DistributionMargin;
                }

                margin = (mMargin + sMargin + dMargin) / 3;

                quantity = (best.MinValue[i] + best.MaxValue[i]) / 2;

                units = products[i].FractionUnits;

                boxUnits = (int)(quantity / 1);
                fractionUnits = (int)((quantity % 1) * units);

                benefit = margin * quantity;

                db.PurchasePlanDetails.Add(new PurchasePlanDetail
                {
                    ProductID = products[i].ID,
                    PurchasePlanID = purchasePlanID,
                    FractionUnits = fractionUnits,
                    BoxUnits = boxUnits,
                    Benefit = benefit
                });
            }
            db.SaveChanges();
        }

        public List<double> MaxBoxes(List<Product> products, DateTime beginDate, DateTime endDate)
        {
            int beginDay, beginMonth, endDay, endMonth, k = 0;
            beginDay = beginDate.Day;
            beginMonth = beginDate.Month;
            endDay = endDate.Day;
            endMonth = endDate.Month;
            List<double> maxBoxes = new List<double>(products.Count);

            foreach (Product product in products)
            {
                List<Movement> movements = db.Movements.Where(m => m.ProductID == product.ID && ((m.MovementDate.Value.Month == beginMonth && m.MovementDate.Value.Day >= beginDay) ||
                                                                    (m.MovementDate.Value.Month > beginMonth)) &&
                                                                   ((m.MovementDate.Value.Month < endMonth) || (m.MovementDate.Value.Day <= endDay && m.MovementDate.Value.Month == endMonth))).OrderBy(m => m.MovementDate).ToList();
                int year, units, i = 0;
                double maxBoxesPerYear = 0.0, boxes = 0.0;

                units = product.FractionUnits;

                List<double> aux = new List<double>(products.Count);

                while (i < movements.Count)
                {
                    year = movements[i].MovementDate.Value.Year;
                    while (i < movements.Count && movements[i].MovementDate.Value.Year == year)
                    {
                        maxBoxesPerYear += movements[i].BoxUnits + (movements[i].FractionUnits/units);
                        i++;
                    }
                    if (boxes > 0.0) boxes = (boxes + maxBoxesPerYear) / 2;
                    else boxes = maxBoxesPerYear;
                }
                maxBoxes.Add(boxes);
            }
            return maxBoxes;
        }

        public double WholeValue(Solution solution, List<Product> products, List<double> maxBoxes)
        {
            double wholeValue = 0.0;
            double quantity;
            for(int i = 0; i < solution.MaxValue.Count; i++)
            {
                quantity = (solution.MinValue[i] + solution.MaxValue[i]) / 2;
                wholeValue += Value(quantity, products[i], maxBoxes[i]);
            }
            return wholeValue;
        }

        public double Cost(Solution solution, List<Product> products)
        {
            double cost = 0.0;
            double quantity;
            for (int i = 0; i < solution.MaxValue.Count; i++)
            {
                quantity = (solution.MinValue[i] + solution.MaxValue[i]) / 2;
                cost += quantity * products[i].BoxPrice;
            }
            return cost;
        }
        public List<Solution> GenerateChilds(Solution solution, List<Product> products, List<double> maxBoxes)
        {
            List<Solution> Childs = new List<Solution>();
            Solution child;
            for(int i = 0; i < solution.MaxValue.Count; i++)
            {
                child = SearchOptimum(solution, products, maxBoxes, i);
                if(child != null) Childs.Add(child);
            }
            return Childs;
        }

        public Solution SearchOptimum(Solution solution, List<Product> products, List<double> maxBoxes, int i)
        {
            Solution child = solution;
            double minValue, maxValue, midValue, lowerMid, upperMid;

            minValue = child.MinValue[i];
            maxValue = child.MaxValue[i];
            midValue = (minValue + maxValue) / 2;

            lowerMid = (midValue + minValue) / 2;
            upperMid = (midValue + maxValue) / 2;

            if(Math.Abs(Value(lowerMid, products[i], maxBoxes[i]) - Value(upperMid, products[i], maxBoxes[i])) > 0.0001)
            {
                if (Value(lowerMid, products[i], maxBoxes[i]) > Value(upperMid, products[i], maxBoxes[i])) child.MaxValue[i] = midValue;
                else child.MinValue[i] = midValue;
                return child;
            }
            return null;
        }

        public double Value(double quantity, Product product, double maxBoxes)
        {
            double value = 0.0;
            //Función objetivo
            SalesMargin saleMargin = db.SaleMargins.Where(s => s.Product.ID == product.ID).FirstOrDefault();
            double margin;
            if (saleMargin != null)
            {
                margin = (saleMargin.MarketMargin + saleMargin.StoreMargin + saleMargin.DistributionMargin)/ 3;
                value = (quantity * margin) - A * Math.Abs((maxBoxes - product.LogicalStock - quantity)/quantity); //penalidad
            }
            return value;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public class Solution
        {
            public Solution()
            {
                StoreContext db = new StoreContext();
                int quantity = db.Products.Count();
                MinValue = new List<double>(quantity);
                MaxValue = new List<double>(quantity);
            } 
       
            public List<double> MinValue { get; set; }
            public List<double> MaxValue { get; set; }
        }
    }
}
