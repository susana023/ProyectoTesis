using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using ProyectoTesis.Models;
using ProyectoTesis.DAL;

namespace ProyectoTesis.Controllers
{
    public class DataHistoricaController : Controller
    {
        private StoreContext db = new StoreContext();
        // GET: DataHistorica
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult ChargeProducts()
        {
            SqlDataReader myReader = null
                ;
            SqlConnection conn = new SqlConnection("server=RITA;" +
                                       "Trusted_Connection=yes;" +
                                       "database=FACTU05");
            conn.Open();
            SqlCommand myCommand = new SqlCommand("select * from [inven05].[dbo].[arti12017]", conn);
            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                double boxPrice, fractionPrice;
                int fractionUnits;
                if (double.TryParse(myReader["CostoAsumido"].ToString(), out boxPrice)) {
                    if (int.TryParse(myReader["unifracc"].ToString(), out fractionUnits))
                    {
                        fractionPrice = boxPrice / fractionUnits;
                        string codarti = myReader["codarti"].ToString();
                        Product product = new Product
                        {
                            Description = myReader["Artdeslar"].ToString(),
                            BoxDescription = "CJA",
                            FractionDescription = "BLS",
                            BoxPrice = boxPrice,
                            FractionPrice = fractionPrice,
                            MaxStock = 1000,
                            MinStock = 0,
                            ActiveFlag = true,
                            LogicalStock = 0,
                            PhysicalStock = 0,
                            FractionUnits = fractionUnits,
                            Codarti = codarti
                        };
                        db.Products.Add(product);
                        db.SaveChanges();                        
                        product = db.Products.Where(p => p.Codarti == codarti).FirstOrDefault();
                        double distribution, market, store;
                        if(product != null && double.TryParse(myReader["Margen00"].ToString(), out distribution) && double.TryParse(myReader["Margen00E"].ToString(), out store) && double.TryParse(myReader["Margen00I"].ToString(), out market))
                        {
                            SalesMargin saleMargin = new SalesMargin
                            {
                                Product = product,
                                DistributionMargin = distribution,
                                MarketMargin = market,
                                StoreMargin = store
                            };
                            db.SaleMargins.Add(saleMargin);
                            db.SaveChanges();
                        }
                    }
                }
            }
            myReader.Close();
            conn.Close();
            return View();
        }


        public ActionResult ChargeMovements()
        {
            SqlDataReader myReader = null
                ;
            SqlConnection conn = new SqlConnection("server=RITA;" +
                                       "Trusted_Connection=yes;" +
                                       "database=FACTU05");
            conn.Open();
            for (int y = 2009; y <= 2017; y++)
            {
                for (int m = 1; m <= 12; m++)
                {
                    string month = m.ToString("D2");
                    Zone zone = db.Zones.ToList().FirstOrDefault();
                    SqlCommand myCommand = new SqlCommand("select * FROM [inven05].[dbo].[im" + y + month + "]", conn);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        int type, numBox, numFraction;
                        double boxes;
                        MovementType movType;
                        if (int.TryParse(myReader["DetTipDoc"].ToString(), out type)){
                            if (type == 1) movType = MovementType.Compra;
                            else movType = MovementType.Despacho;
                            string codart = myReader["Codart"].ToString();
                            Product product = db.Products.Where(p => p.Codarti == codart).FirstOrDefault();
                            DateTime movDate;
                            if (product != null && zone != null)
                            {
                                if(double.TryParse(myReader["DetCanArt"].ToString(), out boxes) && DateTime.TryParse(myReader["DetFecDoc"].ToString(), out movDate))
                                {
                                    numBox = (int)(boxes / 1);
                                    numFraction = (int)((boxes % 1) * product.FractionUnits);
                                    Movement movement = new Movement
                                    {
                                        ProductID = product.ID,
                                        ExpirationDate = DateTime.Today,
                                        ZoneID = zone.ID,
                                        MovementType = movType,
                                        BoxUnits = numBox,
                                        FractionUnits = numFraction,
                                        MovementDate = movDate
                                    };
                                    db.Movements.Add(movement);
                                    db.SaveChanges();
                                }
                            }
                        }                        
                    }
                    myReader.Close();
                }
            }
            conn.Close();
            int Chocolate = 0;
            return View();
        }
    }
}