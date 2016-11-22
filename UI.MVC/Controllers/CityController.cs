using BEP.BL;
using BEP.BL.Models.Gebruikers;
using BEP.BL.Models.Gemeentes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.Mvc;

namespace BEP.UI.MVC.Controllers
{
    public class CityController : Controller
    {
        UnitOfWorkManager uowMgr = new UnitOfWorkManager();
        public CityController()
        {

        }
        // GET: City
        public ActionResult Index(short zip = 0)
        {
            GemeenteManager gemeentemgr = new GemeenteManager(uowMgr);
            BegrotingManager begrotingmgr = new BegrotingManager(uowMgr);
            Gemeente city;
            ApplicationUser Gebruiker = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (User.Identity.IsAuthenticated && zip == 0)
            {
                short gebruikersPostcode;
                short.TryParse(Gebruiker.Zip, out gebruikersPostcode);
                city = gemeentemgr.GetGemeente(gebruikersPostcode);
                ViewBag.CurrentBudget = begrotingmgr.GetBegrotingsTypeFromGemeente(DateTime.Now.Year, gebruikersPostcode);
                return View(city);
            }

            //in try catch zodat hij niet crasht als hij een gemeente vindt dat nog niet in DB bestaat (tijdelijk)
            try
            {
                city = gemeentemgr.GetGemeente(zip);
                ViewBag.CurrentBudget = begrotingmgr.GetBegrotingsTypeFromGemeente(DateTime.Now.Year, zip);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(city);
        }
    }
}