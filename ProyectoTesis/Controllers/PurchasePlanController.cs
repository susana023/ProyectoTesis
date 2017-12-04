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
using System.ComponentModel.DataAnnotations;

namespace ProyectoTesis.Controllers
{
    public class PurchasePlanController : Controller
    {
        private StoreContext db = new StoreContext();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string user = DAL.GlobalVariables.CurrentUser;

        private double A = 0.1;

        private int[] ProductsID;

        private List<double> CARAMELO = new List<double>(new double[] { 0.06, 0.05, 0.06, 0.12, 0.07, 0.1, 0.09, 0.1, 0.1, 0.13, 0.11, 0.15 });
        private List<double> GALLETA = new List<double>(new double[] {0.09, 0.07, 0.1, 0.12, 0.1, 0.08, 0.11, 0.11, 0.08, 0.1, 0.09, 0.09});
        private List<double> CHOCOLATE = new List<double>(new double[] {0.05, 0.09, 0.06, 0.16, 0.12, 0.1, 0.1, 0.12, 0.08, 0.11, 0.06, 0.1});
        private List<double> CHICLE = new List<double>(new double[] {0.08, 0.06, 0.13, 0.11, 0.1, 0.09, 0.08, 0.1, 0.12, 0.09, 0.1, 0.08});
        private List<double> SNACK = new List<double>(new double[] {0.12, 0.17, 0.11, 0.08, 0.13, 0.04, 0.09, 0.09, 0.09, 0.12, 0.07, 0.03});
        private List<double> BARRA_ENERGETICA = new List<double>(new double[] { 0.01, 0.03, 0.07, 0.17, 0.1, 0.15, 0.04, 0.28, 0.05, 0.1, 0.05, 0.09 });
        private List<double> GOMA = new List<double>(new double[] {0.03, 0.06, 0.05, 0.09, 0.08, 0.1, 0.08, 0.09, 0.14, 0.08, 0.07, 0.27});
        private List<double> GASEOSA = new List<double>(new double[] {0.17, 0.16, 0.02, 0.34, 0.11, 0.05, 0, 0.05, 0, 0, 0.08, 0.17});
        private List<double> MARSHMALLOW = new List<double>(new double[] {0.06, 0.04, 0.09, 0.08, 0.12, 0.11, 0.12, 0.13, 0.08, 0.11, 0.09, 0.09});
        private List<double> WAFER = new List<double>(new double[] {0.03, 0.04, 0.09, 0.09, 0.09, 0.13, 0.1, 0.12, 0.06, 0.11, 0.1, 0.18});
        private List<double> CHUPETE = new List<double>(new double[] {0.04, 0.04, 0.09, 0.09, 0.08, 0.13, 0.07, 0.11, 0.11, 0.13, 0.09, 0.16});
        private List<double> PANETON = new List<double>(new double[] {0.12, 0.02, 0.07, 0, 0, 0.03, 0, 0, 0.1, 0.2, 0.18, 0.44});
        private List<double> CEREAL = new List<double>(new double[] {0.05, 0.04, 0.15, 0.13, 0.12, 0.08, 0.05, 0.12, 0.14, 0.08, 0.1, 0.08});
        private List<double> YOGURT = new List<double>(new double[] {1.2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0});
        private List<double> FRUNA = new List<double>(new double[] {0.08, 0.09, 0.1, 0.11, 0.1, 0.08, 0.09, 0.11, 0.08, 0.13, 0.08, 0.08});
        private List<double> TOFFEE = new List<double>(new double[] {0.07, 0.05, 0.08, 0.13, 0.06, 0.06, 0.1, 0.16, 0.18, 0.07, 0.07, 0.12});
        private List<double> BIZCOCHO = new List<double>(new double[] {0.01, 0.05, 0.06, 0.11, 0.06, 0.12, 0.15, 0.09, 0.08, 0.13, 0.13, 0.15});
        private List<double> AGUA = new List<double>(new double[] {0.46, 0.39, 0.04, 0, 0, 0, 0, 0, 0, 0, 0.07, 0.21});
        private List<double> JUGO = new List<double>(new double[] {0.09, 0.14, 0.03, 0.17, 0.13, 0, 0, 0.16, 0.11, 0, 0.14, 0.19});
        private List<double> GELATINA = new List<double>(new double[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1.2});


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
            ViewBag.Details = new List<PurchasePlanDetail>(db.PurchasePlanDetails.Where(p => p.PurchasePlanID == id && (p.BoxUnits >= 0 && p.FractionUnits >= 0)));
            return View(purchasePlan);
        }

        // GET: PurchasePlan/Create
        public ActionResult Create()
        {
            ViewBag.Products = db.Products.ToList(); 
            return View();
        }

        // POST: PurchasePlan/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(string button1, string button2, [Bind(Include = "ID,BeginDate, EndDate,Investment")] PurchasePlan purchasePlan)
        {
            if (ModelState.IsValid)
            {
                db.PurchasePlans.Add(purchasePlan);
                db.SaveChanges();
                int ID = db.PurchasePlans.OrderByDescending(p => p.ID).FirstOrDefault().ID;

                if (button2 != null)
                {
                    return RedirectToAction("ChooseProducts", new { beginDate = purchasePlan.BeginDate, endDate = purchasePlan.EndDate, investment = purchasePlan.Investment, id = ID });
                }
                else
                {
                    List<Product> products = db.Products.Where(p => p.ActiveFlag).ToList();
                    Algorithm(products, purchasePlan.BeginDate, purchasePlan.EndDate, purchasePlan.Investment, ID);
                    return RedirectToAction("Details", new { id = ID });
                }
                log.Info("El usuario " + user + " ejecutó la simulación del plan de compra desde: " + purchasePlan.BeginDate + " hasta: " + purchasePlan.EndDate);

            }
            return View(purchasePlan);
        }



        public ActionResult Plan(int ID, int[] productID)
        {
            int i = 1;
            if (productID != null)
            {
                List<Product> products = new List<Product>();

                foreach (int product in productID)
                {
                    products.Add(db.Products.Where(p => p.ID == product).FirstOrDefault());
                }
                PurchasePlan purchasePlan = db.PurchasePlans.Where(p => p.ID == ID).FirstOrDefault();
                Algorithm(products, purchasePlan.BeginDate, purchasePlan.EndDate, purchasePlan.Investment, ID);

                return RedirectToAction("Details", new { id = ID });
            }
            return View();
        }

        public ActionResult ChooseProducts(DateTime beginDate, DateTime endDate, double investment, int ID, string currentFilter, string searchString)
        {
            List<double> MaxBox = MaxBoxes(db.Products.Where(p => p.ActiveFlag == true).ToList(), beginDate, endDate);
            List<double> LastMonth = LastPeriod(db.Products.Where(p => p.ActiveFlag == true).ToList(), beginDate, endDate);
            List<Product> Products = db.Products.Where(p => p.ActiveFlag == true).OrderBy(p => p.Description).ToList();

            List<ProductPlan> ProductPlan = new List<ProductPlan>();

            int i = 0;

            foreach (Product product in Products)
            {
                ProductPlan.Add(new ProductPlan
                {
                    LastPeriod = LastMonth[i],
                    MaxBoxes = MaxBox[i],
                    Product = product
                });
                i++;
            }

            ViewBag.BeginDate = beginDate;
            ViewBag.EndDate = endDate;
            ViewBag.Investment = investment;

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.ProductPlans = ProductPlan.Where(p => p.Product.Description.Contains(searchString.ToUpper())).ToList();
            }
            else
            {
                ViewBag.ProductPlans = ProductPlan;
            }

            ViewBag.ID = ID;

            return View();
        }

        public List<double> MaximunBoxesQuantity(List<Product> products, DateTime beginDate)
        {
            List<double> maximunQuantity = new List<double>();
            DateTime end;
            
            int i = 0;
            int beginMonth = beginDate.Month, beginDay = beginDate.Day, endMonth, endDay;
            int fractionUnits;
            int productID;
            int maxBoxes = 0, maxFractions = 0;
            ProductType type;
            int months = 0;

            foreach (Product product in products)
            {
                months = 0;
                fractionUnits = product.FractionUnits;
                productID = product.ID;
                type = product.ProductType;
                var month = db.ExpirationTimes.Where(e => e.ProductType == type).FirstOrDefault().Months;
                if (month != null) months = month;
                end = beginDate;
                endDay = end.Day;
                endMonth = end.Month;

                var boxes = db.Movements.Where(m => m.ProductID == productID && ((m.MovementDate.Value.Month == beginMonth && m.MovementDate.Value.Day >= beginDay) ||
                                                                    (m.MovementDate.Value.Month > beginMonth)) &&
                                                                    ((m.MovementDate.Value.Month < endMonth) || (m.MovementDate.Value.Day <= endDay && m.MovementDate.Value.Month == endMonth)) && m.MovementType == MovementType.Despacho).Select(m => m.BoxUnits).DefaultIfEmpty(0).Sum();

                var fractions = db.Movements.Where(m => m.ProductID == productID && ((m.MovementDate.Value.Month == beginMonth && m.MovementDate.Value.Day >= beginDay) ||
                                                                    (m.MovementDate.Value.Month > beginMonth)) &&
                                                                   ((m.MovementDate.Value.Month < endMonth) || (m.MovementDate.Value.Day <= endDay && m.MovementDate.Value.Month == endMonth)) && m.MovementType == MovementType.Despacho).Select(m => m.FractionUnits).DefaultIfEmpty(0).Sum();
                if (boxes != null) maxBoxes = boxes;
                if (fractions != null) maxFractions = fractions;

                if (fractionUnits == 0) fractionUnits = 1;
                maximunQuantity.Add(maxBoxes + (maxFractions / fractionUnits));
            }
            return maximunQuantity;
        }

        public List<double> LastPeriod(List<Product> products, DateTime beginDate, DateTime endDate)
        {
            List<double> LastPeriod = new List<double>();

            TimeSpan difference = endDate.Subtract(beginDate);
            DateTime begin = beginDate - difference;
            int i = 0;
            int beginMonth = begin.Month, beginDay = begin.Day, endMonth = endDate.Month, endDay = endDate.Day;

            foreach(Product product in products)
            {
                int fractionUnits = product.FractionUnits;
                int productID = product.ID;
                int maxBoxes = 0, maxFractions = 0;
                var boxes = db.Movements.Where(m => m.ProductID == productID && ((m.MovementDate.Value.Month == beginMonth && m.MovementDate.Value.Day >= beginDay) ||
                                                                    (m.MovementDate.Value.Month > beginMonth)) && m.MovementDate.Value.Year == DateTime.Today.Year &&
                                                                    ((m.MovementDate.Value.Month < endMonth) || (m.MovementDate.Value.Day <= endDay && m.MovementDate.Value.Month == endMonth)) && m.MovementType == MovementType.Despacho).Select(m => m.BoxUnits).DefaultIfEmpty(0).Sum();
                
                var fractions = db.Movements.Where(m => m.ProductID == productID && ((m.MovementDate.Value.Month == beginMonth && m.MovementDate.Value.Day >= beginDay) ||
                                                                    (m.MovementDate.Value.Month > beginMonth)) && m.MovementDate.Value.Year == DateTime.Today.Year &&
                                                                   ((m.MovementDate.Value.Month < endMonth) || (m.MovementDate.Value.Day <= endDay && m.MovementDate.Value.Month == endMonth)) && m.MovementType == MovementType.Despacho).Select(m => m.FractionUnits).DefaultIfEmpty(0).Sum();
                if (boxes != null) maxBoxes = boxes;
                if (fractions != null) maxFractions = fractions;

                if (fractionUnits == 0) fractionUnits = 1;
                LastPeriod.Add(maxBoxes + (maxFractions/fractionUnits));
                i++;
            }

            return LastPeriod;
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

        public void Algorithm(List<Product> products, DateTime beginDate, DateTime endDate, double invesment, int purchasePlanID)
        {
            //List<Product> products = db.Products.Where(p => p.ActiveFlag == true && (p.ProductType != ProductType.Otros)).ToList();
            List<Solution> bests = new List<Solution>();
            double bestValue = double.MinValue, repeatedBest = 0;
            Solution best = new Solution(products.Count);
            int j = 0, k = 0, repeatBest = 0;
            //falta calcular máxima cantidad por producto en ventas históricas
            List<double> maxBoxes = MaxBoxes(products, beginDate, endDate);

            List<Solution> Childs;
            double value;
            int month = beginDate.Month;

            foreach(double maxBox in maxBoxes)
            {
                best.MaxValue.Add(maxBox);
                best.MinValue.Add(0.0);
                j++;
            }

            while (Cost(best, products) > (2 * invesment))  best = ReduceQuantities(best, products);

            if(Cost(best,products) > (invesment))
            {
                List<double> MaxBoxes = MaximunBoxesQuantity(products, beginDate);

                for(int i = 0; i < best.MaxValue.Count; i++)
                {
                    best.MaxValue[i] = MaxBoxes[i];
                    i++;
                }
            }

            bests.Add(best);

            Solution least = new Solution(products.Count);

            double lessBad = double.MaxValue;
            double cost = 0.0;
            while (bests.Count >= (k + 1))
            {
                Childs = GenerateChilds(bests[k], products, maxBoxes, month, invesment);
                for(int i = 0; i < Childs.Count; i++)
                {
                    cost = Cost(Childs[i], products);
                    value = WholeValue(Childs[i], products, maxBoxes, month);
                    if (Math.Abs(value - bestValue) > 0.01)
                    {
                        if (value > bestValue)
                        {
                            if (cost <= invesment)
                            {
                                bests.Add(Childs[i]);
                                best = Childs[i].DeepCopy();
                                bestValue = value;
                                repeatBest = 0;
                                repeatedBest = bestValue;
                            }
                            else if (lessBad > cost)
                            {
                                least = Childs[i].DeepCopy();
                                lessBad = cost;
                            }
                        }
                    }
                    
                }
                if (bestValue == repeatedBest) repeatBest++;
                k++;
                if (k == bests.Count && lessBad > invesment && bestValue <= 0 && repeatBest < 1000)
                {
                    bests.Add(least);
                }
            }
            SaveResults(best, products, purchasePlanID);            
        }

        public Solution ReduceQuantities(Solution solution, List<Product> products)
        {
            
            for(int i = 0; i < solution.MaxValue.Count; i++)
            {
                if (solution.MaxValue[i] > (1 / products[i].FractionUnits)) solution.MaxValue[i] /= 2;
                else solution.MaxValue[i] = 0;
            }
            return solution;
        }

        public void SaveResults(Solution best, List<Product> products, int purchasePlanID)
        {
            double wholeBenefit = 0;

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

                quantity = best.MaxValue[i];

                if (quantity > 0)
                {
                    units = products[i].FractionUnits;

                    boxUnits = (int)(quantity / 1);
                    fractionUnits = (int)((quantity % 1) * units);

                    benefit = margin * quantity;

                    wholeBenefit += benefit;

                    db.PurchasePlanDetails.Add(new PurchasePlanDetail
                    {
                        ProductID = products[i].ID,
                        PurchasePlanID = purchasePlanID,
                        FractionUnits = fractionUnits,
                        BoxUnits = boxUnits,
                        Benefit = benefit
                    });
                }
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

                int units, i = 0, initialYear, lastYear;
                double maxBoxesPerYear = 0.0, boxes = 0.0, boxesPerYear = 0.0, fractionsPerYear = 0.0;

                if (movements.Count > 0)
                {         
                    units = product.FractionUnits;

                    List<double> aux = new List<double>(products.Count);

                    initialYear = movements[0].MovementDate.Value.Year;
                    lastYear = movements.Last().MovementDate.Value.Year;

                    i = initialYear;

                    while (i <= lastYear)
                    {
                        boxesPerYear = movements.Where(m => m.MovementDate.Value.Year == i).Sum(m => m.BoxUnits);
                        fractionsPerYear = movements.Where(m => m.MovementDate.Value.Year == i).Sum(m => m.FractionUnits);

                        if (boxesPerYear > 0)
                        {
                            maxBoxesPerYear = boxesPerYear + (fractionsPerYear / units);

                            if (boxes > 0.0) boxes = (boxes + maxBoxesPerYear) / 2;
                            else boxes = maxBoxesPerYear;
                        }
                        i++;
                    }                    
                }
                maxBoxes.Add(boxes);
            }
            return maxBoxes;
        }

        public double WholeValue(Solution solution, List<Product> products, List<double> maxBoxes, int month)
        {
            double wholeValue = 0.0;
            double quantity;
            for(int i = 0; i < solution.MaxValue.Count; i++)
            {
                quantity = (solution.MinValue[i] + solution.MaxValue[i]) / 2;
                wholeValue += Value(quantity, products[i], maxBoxes[i], month);
            }
            return wholeValue;
        }

        public double Cost(Solution solution, List<Product> products)
        {
            double cost = 0.0;
            double quantity;
            for (int i = 0; i < solution.MaxValue.Count; i++)
            {
                quantity = solution.MaxValue[i];
                cost += quantity * products[i].BoxPrice;
            }
            return cost;
        }
        public List<Solution> GenerateChilds(Solution solution, List<Product> products, List<double> maxBoxes, int month, double investment)
        {
            List<Solution> Childs = new List<Solution>();
            Solution child = new Solution(products.Count);
            for (int i = 0; i < solution.MaxValue.Count; i++)
            {
                if (solution.MaxValue[i] == 0) child = null;
                else child = SearchOptimum(solution, products, maxBoxes, i, month, investment);
                if(child != null) Childs.Add(child);
            }
            return Childs;
        }

        public Solution SearchOptimum(Solution solution, List<Product> products, List<double> maxBoxes, int i, int month, double investment)
        {
            Solution child = solution.DeepCopy();
            double minValue, maxValue, midValue, lowerMid, upperMid, maxBoxUnits;

            maxBoxUnits = products[i].FractionUnits;

            minValue = child.MinValue[i];
            maxValue = child.MaxValue[i];
            midValue = Round((minValue + maxValue) / 2, maxBoxUnits);

            lowerMid = (midValue + minValue) / 2;
            upperMid = (midValue + maxValue) / 2;

            if(maxValue < maxBoxUnits)
            {
                midValue = 0;
            }

            double lowerMidValue = Value(lowerMid, products[i], maxBoxes[i], month);
            double upperMidValue = Value(upperMid, products[i], maxBoxes[i], month);

            if (Cost(child, products) > investment) child.MaxValue[i] = upperMid;
            else if (Math.Abs(lowerMidValue - upperMidValue) < 0.001) return null;
            else if (lowerMidValue > upperMidValue) child.MaxValue[i] = midValue;
            else child.MinValue[i] = midValue;
            //if (Cost(child, products) < (investment - 100)) return null;
            return child;
        }

        public double Round(double quantity, double fractions)
        {
            double units = (quantity * fractions) / 1;
            return (units / fractions);
        }

        public double Value(double quantity, Product product, double maxBoxes, int month)
        {
            double value = 0.0;
            double A = 0.0;
            //Función objetivo
            SalesMargin saleMargin = db.SaleMargins.Where(s => s.Product.ID == product.ID).FirstOrDefault();
            double margin;
            if (saleMargin != null)
            {
                switch (product.ProductType)
                {
                    case ProductType.Agua:
                        A = AGUA[month - 1];
                        break;
                    case ProductType.BarraEnergética:
                        A = BARRA_ENERGETICA[month - 1];
                        break;
                    case ProductType.Bizcocho:
                        A = BIZCOCHO[month - 1];
                        break;
                    case ProductType.Caramelo:
                        A = CARAMELO[month - 1];
                        break;
                    case ProductType.Cereal:
                        A = CEREAL[month - 1];
                        break;
                    case ProductType.Chicle:
                        A = CHICLE[month - 1];
                        break;
                    case ProductType.Chocolate:
                        A = CHOCOLATE[month - 1];
                        break;
                    case ProductType.Chupete:
                        A = CHUPETE[month - 1];
                        break;
                    case ProductType.Fruna:
                        A = FRUNA[month - 1];
                        break;
                    case ProductType.Galleta:
                        A = GALLETA[month - 1];
                        break;
                    case ProductType.Gaseosa:
                        A = GASEOSA[month - 1];
                        break;
                    case ProductType.Gelatina:
                        A = GELATINA[month - 1];
                        break;
                    case ProductType.Goma:
                        A = GOMA[month - 1];
                        break;
                    case ProductType.Jugo:
                        A = JUGO[month - 1];
                        break;
                    case ProductType.Marshmallow:
                        A = MARSHMALLOW[month - 1];
                        break;
                    case ProductType.Panetón:
                        A = PANETON[month - 1];
                        break;
                    case ProductType.Snack:
                        A = SNACK[month - 1];
                        break;
                    case ProductType.Toffee:
                        A = TOFFEE[month - 1];
                        break;
                    case ProductType.Wafer:
                        A = WAFER[month - 1];
                        break;
                    case ProductType.Yogurt:
                        A = YOGURT[month - 1];
                        break;

                }

                margin = (saleMargin.MarketMargin + saleMargin.StoreMargin + saleMargin.DistributionMargin)/ 3;
                value = (quantity * margin) - A * Math.Abs((maxBoxes - product.LogicalStock - quantity)/(quantity + 1)); //penalidad
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

        public class ProductPlan
        {
            public Product Product { get; set; }

            [DisplayFormat(DataFormatString = "{0:n2}")]
            public double MaxBoxes { get; set; }
            [DisplayFormat(DataFormatString = "{0:n2}")]
            public double LastPeriod { get; set; }
        }

        public class Solution
        {
            public Solution(int n)
            {
                StoreContext db = new StoreContext();
                MinValue = new List<double>(n);
                MaxValue = new List<double>(n);
            }
            public Solution ShallowCopy()
            {
                return (Solution)this.MemberwiseClone();
            }

            public Solution DeepCopy()
            {
                Solution other = new Solution(this.MaxValue.Count);
                for (int i = 0; i < MinValue.Count; i++)
                {
                    other.MinValue.Add(MinValue[i]);
                    other.MaxValue.Add(MaxValue[i]);
                }
                return other;
            }

            public List<double> MinValue { get; set; }
            public List<double> MaxValue { get; set; }
        }
    }
}
