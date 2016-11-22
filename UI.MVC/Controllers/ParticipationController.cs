using BEP.BL;
using BEP.BL.Models.Gemeentes;
using BEP.BL.Models.ParticipatieProjecten;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using BEP.BL.Models.Begrotingen;
using BEP.BL.Models.Gebruikers;
using BEP.BL.Models;
using System.Net;
using System.IO;

namespace BEP.UI.MVC.Controllers
{
  public class ParticipationController : Controller
  {
    private ApplicationSignInManager _signInManager;
    private ApplicationUserManager _userManager;
    UnitOfWorkManager uowMgr = new UnitOfWorkManager();
    public ParticipationController()
    {

    }
    public ParticipationController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
    {
      UserManager = userManager;
      SignInManager = signInManager;
    }
    public ApplicationSignInManager SignInManager
    {
      get
      {
        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
      }
      private set
      {
        _signInManager = value;
      }
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

    // GET: Index
    public ActionResult Index(short zip = 2000, string sortMethod = "Draagvlak")
    {
      GemeenteManager gemeenteMgr = new GemeenteManager(uowMgr);
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      //City ophalen
      Gemeente city = gemeenteMgr.GetGemeente(zip);
      // Participatieprojecten
      ViewBag.ProjectList = city.ParticipatieProjecten.Where(p => p.Eindmoment > DateTime.Now).ToList<ParticipatieProject>();
      //Begrotingsvoorstellen
      List<Begrotingsvoorstel> voorstellen = projectMgr.GetAllBegrotingsvoorstellen().Where(b => b.GoedKeuringsStaat != BegrotingsvoorstelStaat.Afgekeurd && b.Begroting.Gemeente.Postcode == city.Postcode).ToList<Begrotingsvoorstel>();
      //Sorteer lijst alvorens door te sturen
      List<Begrotingsvoorstel> orderedList = new List<Begrotingsvoorstel>();
      switch (sortMethod)
      {
        case "Draagvlak":
          orderedList = voorstellen.OrderBy(v => v.Draagvlak).Reverse().ToList(); ;
          break;
        case "Datum":
          orderedList = voorstellen.OrderBy(v => v.Datum).Reverse().ToList();
          break;
        case "Budgetwijziging":
          orderedList = voorstellen.OrderBy(v => v.TotaalBudgetwijziging).Reverse().ToList();
          break;
        default:
          orderedList = voorstellen.OrderBy(v => v.Draagvlak).Reverse().ToList(); ;
          break;
      }
      ViewBag.BudgetProposals = orderedList;
      ViewBag.SortMethod = sortMethod;
      return View(city);
    }

    // POST: /Participatie/SorteerVoorstellen
    [HttpPost]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult SortProposals(FormCollection form)
    {

      string selectedSort = form["sortAnswer"];
      return RedirectToAction("Index", new { sortMethod = selectedSort });
    }

    // GET: Details
    [HttpGet]
    public ActionResult Details(int begrotingsVoorstelId)
    {
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      BegrotingManager begrotingMgr = new BegrotingManager(uowMgr);
      ViewBag.AllUsers = UserManager.GetAllUsers();
      Begrotingsvoorstel voorstel = (projectMgr.GetBegrotingsvoorstel(begrotingsVoorstelId));
      List<Categorie> categorien = begrotingMgr.GetAllCategorieën().ToList();
      return View(projectMgr.GetBegrotingsvoorstel(begrotingsVoorstelId));
    }

    // POST: Reactie
    [HttpPost]
    public void AddReactie(string tekst, int begrotingsvoorstelId)
    {
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      string UserId = User.Identity.GetUserId();
      if (UserId != null && !UserId.Equals(""))
      {
        projectMgr.AddReactie(tekst, DateTime.Now, UserId, begrotingsvoorstelId);
        uowMgr.Save();
        RedirectToAction("Details");
      }
    }

    // POST: SubReactie
    [HttpPost]
    public void AddSubReactie(string tekst, int reactieId)
    {
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      string UserId = User.Identity.GetUserId();
      if (UserId != null && !UserId.Equals(""))
      {
        projectMgr.AddSubReactie(tekst, DateTime.Now, UserId, reactieId);
        uowMgr.Save();
        RedirectToAction("Details");
      }
    }
    //GET:Create voorstel
    [CustomAuthorize]
    public ActionResult Create(ParticipatieProject participatieproject = null, short zip = 0)
    {
      BegrotingManager begrotingMgr = new BegrotingManager(uowMgr);
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      ApplicationUser user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
      //Als je een eigen begrotingsvoorstel doet is het altijd een herschikking
      ParticipatieProject project = new ParticipatieProject();
      if (participatieproject.ParticipatieProjectId == 0)
      {
        Begroting begroting = (Begroting)begrotingMgr.GetBegrotingsTypeFromGemeente(DateTime.Now.Year, zip);
        project.Begroting = begroting;
        project.Type = ProjectType.Herschikking;
        project.Titel = "Eigen voorstel";
        project.Startmoment = DateTime.Now;
        project.Eindmoment = DateTime.Now;
        project.ParticipatieProjectId = 0;
        project.Bedrag = 0;
        //Startbudgetten bepalen adhv categorieinformaties in begroting
        foreach (CategorieInformatie catinfo in begroting.CategorieInformaties)
        {
          if (catinfo.Bedrag == 0)
          {
            project.StartBudgetten.Add(new StartBudget { CategorieInformatie = catinfo, Bedrag = catinfo.Bedrag, MaximumBedrag = 20000, MinimumBedrag = 0 });
          }
          else
          {
            project.StartBudgetten.Add(new StartBudget { CategorieInformatie = catinfo, Bedrag = catinfo.Bedrag, MaximumBedrag = catinfo.Bedrag + (catinfo.Bedrag / 2), MinimumBedrag = catinfo.Bedrag - (catinfo.Bedrag / 2) });
          }
        }
      }
      else
      {
        //project = participatieproject;
        project = projectMgr.GetParticipatieProject(participatieproject.ParticipatieProjectId);
      }

      //Startbudgetten van project in voorstel laden
      List<Budget> budgetten = new List<Budget>();
      foreach (StartBudget sb in project.StartBudgetten)
      {
        Budget budget = new Budget { CategorieInformatie = sb.CategorieInformatie, Bedrag = sb.Bedrag };
        budgetten.Add(budget);
      }
      //Aanmaken voorstel
      Begrotingsvoorstel voorstel = new Begrotingsvoorstel
      {
        BegrotingsVoorstelId = 0,
        Begroting = project.Begroting,
        ParticipatieProject = project,
        GoedKeuringsStaat = BegrotingsvoorstelStaat.InBehandeling,
        Draagvlak = 0,
        Datum = DateTime.Now,
        TotaalBudgetwijziging = 0,
        Budgetten = budgetten,
        UserId = user.Id,
        Voorsteller = user.Firstname + " " + user.Lastname,
        KorteBeschrijving = "",
        LangeBeschrijving = ""
      };

      uowMgr.Save();
      return View(voorstel);
    }

    //POST:Create voorstel
    [HttpPost]
    public ActionResult Create(PostBegrotingsvoorstelModel begrotingsvoorstelModel)
    {
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      BegrotingManager begrotingMgr = new BegrotingManager(uowMgr);
      ApplicationUser user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
      List<Budget> budgettenList = new List<Budget>();
      string imgPath = null;
      var id = 0;
      //Bij allereerste begrotingsvoorstel geeft dit een error
      try
      {
        id = projectMgr.GetAllBegrotingsvoorstellen().Last().BegrotingsVoorstelId + 1;
      }
      catch
      {
        id = 0;
      }
      if (begrotingsvoorstelModel.Afbeelding != null)
      {
        byte[] img = Convert.FromBase64String(begrotingsvoorstelModel.Afbeelding);
        // Get the object used to communicate with the server.
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://10.134.216.25/content/images/Begrotingsvoorstel/" + id + ".jpg");
        request.UseBinary = true;
        request.Method = WebRequestMethods.Ftp.UploadFile;
        request.Credentials = new NetworkCredential("bitsplease", "Agronant1");
        request.ContentLength = img.Length;
        Stream reqStream = request.GetRequestStream();
        reqStream.Write(img, 0, img.Length);
        reqStream.Close();
        imgPath = "/content/images/Begrotingsvoorstel/" + id + ".jpg";
      }
      if (ModelState.IsValid)
      {
        Begrotingsvoorstel begrotingsvoorstel = new Begrotingsvoorstel
        {
          Begroting = (Begroting)begrotingMgr.GetBegrotingsTypeFromGemeente(begrotingsvoorstelModel.begrotingJaartal, (short)begrotingsvoorstelModel.Postcode),
          ParticipatieProject = projectMgr.GetParticipatieProject(begrotingsvoorstelModel.ParticipatieProjectId),
          GoedKeuringsStaat = (BegrotingsvoorstelStaat)begrotingsvoorstelModel.GoedKeuringsStaat,
          Draagvlak = 0,
          Afbeelding = imgPath,
          Datum = DateTime.Now,
          TotaalBudgetwijziging = begrotingsvoorstelModel.TotaalBudgetwijziging,
          UserId = user.Id,
          Voorsteller = user.Firstname + " " + user.Lastname,
          KorteBeschrijving = begrotingsvoorstelModel.KorteBeschrijving,
          LangeBeschrijving = begrotingsvoorstelModel.LangeBeschrijving
        };
        if (begrotingsvoorstel.ParticipatieProject != null)
        {
          for (var i = 0; i < begrotingsvoorstel.ParticipatieProject.StartBudgetten.Count; i++)
          {
            CategorieInformatie catinfo = begrotingsvoorstel.Begroting.CategorieInformaties.ToList<CategorieInformatie>()[i];
            budgettenList.Add(new Budget
            {
              Bedrag = begrotingsvoorstelModel.Budgetten[i],
              CategorieInformatie = catinfo,
              BudgetWijziging = begrotingsvoorstelModel.Budgetten[i] - begrotingsvoorstel.ParticipatieProject.StartBudgetten.ToList<StartBudget>()[i].Bedrag
            });
          };
        }
        else
        {
          for (var i = 0; i < begrotingsvoorstel.Begroting.CategorieInformaties.Count; i++)
          {
            CategorieInformatie catinfo = begrotingsvoorstel.Begroting.CategorieInformaties.ToList<CategorieInformatie>()[i];
            budgettenList.Add(new Budget { Bedrag = begrotingsvoorstelModel.Budgetten[i], CategorieInformatie = catinfo });
          };
        }
        begrotingsvoorstel.Budgetten = budgettenList;
        if (begrotingsvoorstel.ParticipatieProject != null)
        {
          ParticipatieProject participatieproject = begrotingsvoorstel.ParticipatieProject;
          participatieproject.Begrotingsvoorstellen.Add(begrotingsvoorstel);
          projectMgr.ChangeParticipatieProject(participatieproject);
          uowMgr.Save();
          return RedirectToAction("Index");
        }
        else
        {
          projectMgr.AddBegrotingsvoorstel(begrotingsvoorstel);
          uowMgr.Save();
          return RedirectToAction("Index");
        }
      }
      return View();
    }
  }
}