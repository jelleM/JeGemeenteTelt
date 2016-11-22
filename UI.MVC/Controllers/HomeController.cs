using BEP.BL;
using BEP.BL.Models.Gemeentes;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BEP.UI.MVC.Controllers
{

    public class HomeController : Controller
    {
        UnitOfWorkManager uowMgr = new UnitOfWorkManager();
        public HomeController()
        {

        }

        public ActionResult Index()
        {
            GemeenteManager gemeentemgr = new GemeenteManager(uowMgr);
            List<Gemeente> gemeentes = new List<Gemeente>();
            gemeentes = gemeentemgr.GetAllGemeentes().Where(g => g.ParticipatieProjecten.Count > 0).ToList();
            ViewBag.Cities = gemeentes;
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection formData)
        {
            short postcode;
            short.TryParse(formData["postcode"].Substring(0, 4), out postcode);
            return RedirectToAction("Index", "City", new { zip = postcode });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }
    }
}