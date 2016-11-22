using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEP.BL;
using BEP.BL.Models.Gemeentes;
using Microsoft.AspNet.Identity.Owin;
using BEP.BL.Models.Begrotingen;

namespace BEP.UI.MVC.Controllers
{
    public class TransparanceController : Controller
    {
        UnitOfWorkManager uowMgr = new UnitOfWorkManager();
        //private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public TransparanceController()
        {

        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Transparance
        public ActionResult Compare(short zip1 = 0, short zip2 = 0, int year1 = 0, int year2 = 0)
        {
            BegrotingManager begrotingmgr = new BegrotingManager(uowMgr);
            GemeenteManager gemeentemgr = new GemeenteManager(uowMgr);
            try
            {
                if (zip1 != 0 & year1 != 0)
                {
                    ViewBag.BudgetLeft = begrotingmgr.GetBegrotingsTypeFromGemeente(year1, zip1);
                    ViewBag.CityLeftZip = gemeentemgr.GetGemeente(zip1).Postcode;
                    ViewBag.CityLeft = gemeentemgr.GetGemeente(zip1).Naam;
                    ViewBag.YearLeft = year1;
                }
                if (zip2 != 0 & year2 != 0)
                {
                    if (zip2 == 9999 && zip1 != 0 && year1 != 0)
                    {
                        //Bereken gemiddelde van cluster van linkse gemeente
                        Begroting avgCluster = new Begroting();
                        Gemeente g1 = gemeentemgr.GetGemeente(zip1);
                        if (g1.Cluster != null)
                        {
                            var categorieInformatieId = begrotingmgr.GetAllInformaties().Last().CategorieInformatieId + 1;
                            List<BegrotingsType> begrotingenCluster = begrotingmgr.GetAllBegrotingsTypes().Where(b => b.Gemeente.Cluster.Equals(g1.Cluster)).ToList();
                            List<CategorieInformatie> informaties = new List<CategorieInformatie>();
                            foreach (BegrotingsType bt in begrotingenCluster)
                            {
                                informaties.AddRange(bt.CategorieInformaties);
                            }
                            foreach (Categorie c in begrotingmgr.GetAllCategorieën())
                            {
                                CategorieInformatie categorieInfoAvg = new CategorieInformatie { CategorieInformatieId = categorieInformatieId, Categorie = c };
                                List<CategorieInformatie> _info = informaties.Where(cinfo => cinfo.Categorie.CategorieId == c.CategorieId).ToList();
                                if (_info != null && _info.Count > 0)
                                {
                                    categorieInfoAvg.Bedrag = _info.Select(cinfo2 => cinfo2.Bedrag).Average();
                                }
                                avgCluster.CategorieInformaties.Add(categorieInfoAvg);
                                categorieInformatieId++;
                            }
                            avgCluster.Totaal = avgCluster.CategorieInformaties.Where(ccccc => ccccc.Categorie.CategorieNiveau == CategorieNiveau.A).Select(cccc => cccc.Bedrag).Sum();
                            ViewBag.BudgetRight = avgCluster;
                            ViewBag.CityRightZip = 9999;
                            ViewBag.CityRight = "Gemiddelde " + g1.Cluster.ToString();
                            ViewBag.YearRight = year1;
                        }
                        else
                        {
                            ViewBag.BudgetRight = null;
                        }
                    }
                    else
                    {
                        ViewBag.BudgetRight = begrotingmgr.GetBegrotingsTypeFromGemeente(year2, zip2);
                        ViewBag.CityRightZip = gemeentemgr.GetGemeente(zip2).Postcode;
                        ViewBag.CityRight = gemeentemgr.GetGemeente(zip2).Naam;
                        ViewBag.YearRight = year2;
                    }
                }
            }
            catch
            {
                ViewBag.BudgetRight = null;
                ViewBag.CityRightZip = 0;
                ViewBag.CityRight = null;
                ViewBag.YearRight = year2;
                ViewBag.BudgetLeft = null;
                ViewBag.CityLeftZip = 0;
                ViewBag.CityLeft = null;
                ViewBag.YearLeft = year1;
            }

            //Jaar kiezen
            ViewBag.Years = begrotingmgr.GetAllBegrotingsTypes().OrderBy(bb => bb.Jaartal).Select(b => b.Jaartal).Distinct();
            return View();
        }

        public ActionResult MySalary(short zip, string type = "Arbeider", int mySalary = 3243)
        {
            GemeenteManager gemeentemgr = new GemeenteManager(uowMgr);
            BegrotingManager begrotingmgr = new BegrotingManager(uowMgr);
            Gemeente city;
            city = gemeentemgr.GetGemeente(zip);

            //Berkenen gemeentebelasting & pas aan in de begroting die wordt doorgegeven
            BegrotingsType bt = begrotingmgr.GetBegrotingsTypeFromGemeente(DateTime.Now.Year, zip);
            double citytax = gemeentemgr.GetGemeente(zip).GemeenteBelasting;
            double tax = 0;
            double verschil1 = 8709.99;
            double verschil2 = 3689;
            double verschil3 = 8259;
            double verschil4 = 17209;
            double max1 = 8710;
            double max2 = 12400;
            double max3 = 20660;
            double berekeningschijf1 = 0;
            double berekeningschijf2 = 0;
            double berekeningschijf3 = 0;
            double berekeningschijf4 = 0;
            double brutoMinRSZ = 0;
            double inkomenschijfsom = 0;
            double belastingsvrij = 0;
            double personenbelasting = 0;
            double gemeentebelasting = 0;
            double jaarsal = 0;
            jaarsal = mySalary * 12;
            if (type.Equals("Arbeider"))
            {
                tax = (jaarsal * 1.08) * 0.1307;
            }
            else if (type.Equals("Bediende"))
            {
                tax = jaarsal * 0.1307;
            }
            brutoMinRSZ = jaarsal - tax;
            berekeningschijf1 = 0.25 * Math.Min(Math.Max(brutoMinRSZ, 0), verschil1);
            berekeningschijf2 = 0.3 * Math.Min(Math.Max(brutoMinRSZ - max1, 0), verschil2);
            berekeningschijf3 = 0.4 * Math.Min(Math.Max(brutoMinRSZ - max2, 0), verschil3);
            berekeningschijf4 = 0.45 * Math.Min(Math.Max(brutoMinRSZ - max3, 0), verschil4);
            inkomenschijfsom = berekeningschijf1 + berekeningschijf2 + berekeningschijf3 + berekeningschijf4;
            belastingsvrij = 0.25 * Math.Min(brutoMinRSZ, 7090);
            personenbelasting = inkomenschijfsom - belastingsvrij;
            gemeentebelasting = personenbelasting * citytax;
            for (var i = 0; i < bt.CategorieInformaties.Count; i++)
            {
                CategorieInformatie info = bt.CategorieInformaties.ToList()[i];

                info.Bedrag = Math.Round((((info.Bedrag / bt.Totaal) * gemeentebelasting) / 12), 2);
            }
            //Viewbags
            ViewBag.Salary = mySalary;
            ViewBag.CurrentBudget = bt;
            ViewBag.Type = type;
            return View(city);
        }
    }
}