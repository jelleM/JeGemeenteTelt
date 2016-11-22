using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BEP.BL;
using BEP.BL.Models.Begrotingen;
using BEP.BL.Models.Gemeentes;

namespace BEP.UI.MVC.Controllers
{
    public class BudgetApiController : ApiController
    {
        UnitOfWorkManager uowMgr = new UnitOfWorkManager();
        public BudgetApiController()
        {

        }

        // GET: api/BudgetApi
        [Route("api/Budget/{zip}")]
        public BegrotingsType Get(short zip)
        {
            try
            {
                BegrotingManager mngr = new BegrotingManager(uowMgr);
                BegrotingsType begrotingsType = mngr.GetBegrotingsTypeFromGemeente(DateTime.Now.Year, zip);
                begrotingsType = new Begroting(begrotingsType.Jaartal)
                {
                    Totaal = begrotingsType.Totaal,
                    CategorieInformaties = begrotingsType.CategorieInformaties,
                    Acties = begrotingsType.Acties
                };
                return begrotingsType;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET: api/BudgetApiYear
        [Route("api/Budget/{zip}/{year}")]
        public BegrotingsType Get(short zip, int year)
        {
            try
            {
                BegrotingManager mngr = new BegrotingManager(uowMgr);
                BegrotingsType begrotingsType = mngr.GetBegrotingsTypeFromGemeente(year, zip);
                begrotingsType = new Begroting(begrotingsType.Jaartal)
                {
                    Totaal = begrotingsType.Totaal,
                    CategorieInformaties = begrotingsType.CategorieInformaties,
                    Acties = begrotingsType.Acties
                };
                return begrotingsType;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET: api/BudgetApiYear
        [Route("api/Budget/{zip}/{year}/{avg}")]
        public BegrotingsType Get(short zip, int year, bool avg)
        {
            try
            {
                BegrotingManager mngr = new BegrotingManager(uowMgr);
                GemeenteManager gemeentemgr = new GemeenteManager(uowMgr);
                //Bereken gemiddelde van cluster van linkse gemeente
                Begroting avgCluster = new Begroting();
                Gemeente g1 = gemeentemgr.GetGemeente(zip);
                if (g1.Cluster != null)
                {
                    var categorieInformatieId = mngr.GetAllInformaties().Last().CategorieInformatieId + 1;
                    List<BegrotingsType> begrotingenCluster = mngr.GetAllBegrotingsTypes().Where(b => b.Gemeente.Cluster.Equals(g1.Cluster)).ToList<BegrotingsType>();
                    List<CategorieInformatie> informaties = new List<CategorieInformatie>();
                    foreach (BegrotingsType bt in begrotingenCluster)
                    {
                        informaties.AddRange(bt.CategorieInformaties);
                    }
                    foreach (Categorie c in mngr.GetAllCategorieën())
                    {
                        CategorieInformatie categorieInfoAvg = new CategorieInformatie { CategorieInformatieId = categorieInformatieId, Categorie = c };
                        List<CategorieInformatie> _info = informaties.Where(cinfo => cinfo.Categorie.CategorieId == c.CategorieId).ToList<CategorieInformatie>();
                        if (_info != null && _info.Count > 0)
                        {
                            categorieInfoAvg.Bedrag = Math.Round(_info.Select(cinfo2 => cinfo2.Bedrag).Average(), 2);
                        }
                        avgCluster.CategorieInformaties.Add(categorieInfoAvg);
                        categorieInformatieId++;
                    }
                    avgCluster.Totaal = Math.Round(avgCluster.CategorieInformaties.Where(ccccc => ccccc.Categorie.CategorieNiveau == CategorieNiveau.A).Select(cccc => cccc.Bedrag).Sum(), 2);
                    return avgCluster;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        [Route("api/Budget/Category/{categorieId}/{zip}")]
        public CategorieInformatie Get(int categorieId, short zip)
        {
            BegrotingManager begrotingMgr = new BegrotingManager(uowMgr);
            Begroting begroting = (Begroting)begrotingMgr.GetBegrotingsTypeFromGemeente(DateTime.Now.Year, zip);
            CategorieInformatie categorieInformatie = begroting.CategorieInformaties.Single(info => info.Categorie.CategorieId == categorieId);
            return categorieInformatie;
        }
    }
}
