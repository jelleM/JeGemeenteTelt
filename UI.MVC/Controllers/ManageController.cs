using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.IO;
using BEP.BL;
using BEP.BL.Models;
using BEP.BL.Models.Gebruikers;
using BEP.BL.Models.Gemeentes;
using System.Data;
using BEP.BL.Models.ParticipatieProjecten;
using BEP.BL.Models.Begrotingen;
using System.Net;

namespace BEP.UI.MVC.Controllers
{
  [CustomAuthorize]
  public class ManageController : Controller
  {
    UnitOfWorkManager uowMgr = new UnitOfWorkManager();
    private ApplicationSignInManager _signInManager;
    private ApplicationUserManager _userManager;

    public ManageController()
    {

    }

    public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
    {
      UserManager = userManager;
      SignInManager = signInManager;
      GemeenteManager gemeentemgr = new GemeenteManager(uowMgr);
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

    // GET: /Manage/Index
    public async Task<ActionResult> Index(ManageMessageId? message)
    {
      ViewBag.StatusMessage =
          message == ManageMessageId.ChangePasswordSuccess ? "Uw wachtwoord is succesvol gewijzigd."
          : message == ManageMessageId.SetPasswordSuccess ? "Uw wachtwoord is succesvol ingesteld."
          : message == ManageMessageId.SetTwoFactorSuccess ? "Uw dubbele authenticatie provider is succesvol ingesteld."
          : message == ManageMessageId.Error ? "Er is een fout opgetreden."
          : message == ManageMessageId.AddPhoneSuccess ? "Uw telefoon nummer is succesvol ingesteld."
          : message == ManageMessageId.RemovePhoneSuccess ? "Uw telefoon nummer is succesvol verwijderd."
          : "";

      var userId = User.Identity.GetUserId();

      //Image toevoegen
      var user = UserManager.FindById(userId);
      string image = null;
      if (user.Image != null)
      {
        image = user.Image;
      }
      var model = new IndexViewModel
      {
        HasPassword = HasPassword(),
        PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
        TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
        Logins = await UserManager.GetLoginsAsync(userId),
        BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),
        Image = image
      };
      ViewBag.Name = user.Firstname + " " + user.Lastname;
      ViewBag.Zip = user.Zip;
      return View(model);
    }

    // POST: /Manage/RemoveLogin
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
    {
      ManageMessageId? message;
      var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
      if (result.Succeeded)
      {
        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        if (user != null)
        {
          await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        }
        message = ManageMessageId.RemoveLoginSuccess;
      }
      else
      {
        message = ManageMessageId.Error;
      }
      return RedirectToAction("ManageLogins", new { Message = message });
    }

    // GET: /Manage/ChangePassword
    public ActionResult ChangePassword()
    {
      return View();
    }

    // POST: /Manage/ChangePassword
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
      if (result.Succeeded)
      {
        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        if (user != null)
        {
          await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        }
        return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
      }
      AddErrors(result);
      return View(model);
    }

    // GET: /Manage/SetPassword
    public ActionResult SetPassword()
    {
      return View();
    }

    // POST: /Manage/SetPassword
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
    {
      if (ModelState.IsValid)
      {
        var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
        if (result.Succeeded)
        {
          var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
          if (user != null)
          {
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
          }
          return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
        }
        AddErrors(result);
      }
      return View(model);
    }

    // GET: /Manage/ManageLogins
    public async Task<ActionResult> ManageLogins(ManageMessageId? message)
    {
      ViewBag.StatusMessage =
          message == ManageMessageId.RemoveLoginSuccess ? "De externe loginmogelijkheid is verwijderd."
          : message == ManageMessageId.Error ? "Er is een fout opgetreden."
          : "";
      var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
      if (user == null)
      {
        return View("Error");
      }
      var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
      var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
      ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
      return View(new ManageLoginsViewModel
      {
        CurrentLogins = userLogins,
        OtherLogins = otherLogins
      });
    }

    // POST: /Manage/LinkLogin
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult LinkLogin(string provider)
    {
      // Request a redirect to the external login provider to link a login for the current user
      return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
    }

    // GET: /Manage/LinkLoginCallback
    public async Task<ActionResult> LinkLoginCallback()
    {
      var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
      if (loginInfo == null)
      {
        return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
      }
      var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
      return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && _userManager != null)
      {
        _userManager.Dispose();
        _userManager = null;
      }

      base.Dispose(disposing);
    }

    //Aanpassen roles
    public void DefineRoles()
    {
      List<SelectListItem> rolesList = new List<SelectListItem>();
      //Checken welke rollen user heeft en op basis daarvan de rollenlijst samenstellen
      //zodat een gebruiker nooit een rol kan toekennen boven zichzelf
      if (User.IsInRole("Superadmin"))
      {
        rolesList = UserManager.GetAllRoles().OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
      }
      else if (User.IsInRole("Admin"))
      {
        rolesList = UserManager.GetAllRoles().Where(rrr => rrr.Name != "Superadmin").OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();

      }
      else if (User.IsInRole("Moderator"))
      {
        rolesList = UserManager.GetAllRoles().Where(rrr => rrr.Name != "Superadmin" && rrr.Name != "Admin").OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
      }
      else
      {
        rolesList = UserManager.GetAllRoles().Where(rrr => rrr.Name != "Superadmin" && rrr.Name != "Admin" && rrr.Name != "Moderator").OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
      }

      ViewBag.Roles = rolesList;
    }

    public void DefineUsers()
    {
      //Een gebruiker mag geen rollen kunnen aanpassen van gebruikers met een hogere rol
      List<SelectListItem> usersList = new List<SelectListItem>();
      //Lijsten met de users die zichtbaar zijn voor de betreffende rol
      var superAdmins = new List<ApplicationUser>();
      var admins = new List<ApplicationUser>();
      var moderator = new List<ApplicationUser>();
      var gebruikers = new List<ApplicationUser>();

      foreach (ApplicationUser user in UserManager.Users.ToList())
      {
        IList<String> rollen = UserManager.GetRolesAsync(user.Id).Result;
        //UserManager.GetAllRoles().Where(r => r.Users.)


        if (rollen[0] == "Superadmin")
        {
          superAdmins.Add(user);
        }
        else if (rollen[0] == "Admin")
        {

          admins.Add(user);
          superAdmins.Add(user);

        }
        else if (rollen[0] == "Moderator")
        {
          moderator.Add(user);
          superAdmins.Add(user);
          admins.Add(user);

        }
        else
        {
          gebruikers.Add(user);
          superAdmins.Add(user);
          admins.Add(user);
          moderator.Add(user);
        }


      }

      //Toepassen op gebruikers naargelang hun rols

      if (User.IsInRole("Superadmin"))
      {
        usersList = UserManager.Users.Where(u => !(u.UserName.ToString().Equals(User.Identity.Name.ToString()))).OrderBy(uu => uu.UserName).ToList().Select(uuu => new SelectListItem { Value = uuu.UserName.ToString(), Text = uuu.UserName }).ToList();
      }
      else if (User.IsInRole("Admin"))
      {
        usersList = admins.Where(u => !(u.UserName.ToString().Equals(User.Identity.Name.ToString()))).OrderBy(uu => uu.UserName).ToList().Select(uuu => new SelectListItem { Value = uuu.UserName.ToString(), Text = uuu.UserName }).ToList();
      }
      else if (User.IsInRole("Moderator"))
      {
        usersList = moderator.Where(u => !(u.UserName.ToString().Equals(User.Identity.Name.ToString()))).OrderBy(uu => uu.UserName).ToList().Select(uuu => new SelectListItem { Value = uuu.UserName.ToString(), Text = uuu.UserName }).ToList();
      }
      else
      {
        usersList = gebruikers.Where(u => !(u.UserName.ToString().Equals(User.Identity.Name.ToString()))).OrderBy(uu => uu.UserName).ToList().Select(uuu => new SelectListItem { Value = uuu.UserName.ToString(), Text = uuu.UserName }).ToList();
      }
      ViewBag.Users = usersList;
    }

    [CustomAuthorize(Roles = "Admin,Superadmin,Moderator")]
    public ActionResult ManageUserRoles()
    {
      //Lijst met roles
      DefineRoles();

      //Lijst met users
      DefineUsers();
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [CustomAuthorize(Roles = "Admin,Superadmin,Moderator")]
    public ActionResult GetRoles(string UserName)
    {
      GemeenteManager gemeentemgr = new GemeenteManager(uowMgr);
      if (!string.IsNullOrWhiteSpace(UserName))
      {
        ApplicationUser user = UserManager.GetUserByUsername(UserName);
        ViewBag.RolesForThisUser = UserManager.GetRoles(user.Id);
        ViewBag.SearchedUser = user.UserName;
        short zip;
        short.TryParse(user.Zip, out zip);
        ViewBag.Gemeente = gemeentemgr.GetGemeente(zip);
        DefineRoles();
        DefineUsers();
      }
      return View("ManageUserRoles");
    }

    [CustomAuthorize(Roles = "Admin,Superadmin,Moderator")]
    public ActionResult ChangeRoleForUser(string UserName, string RoleName)
    {
      ApplicationUser user = UserManager.GetUserByUsername(UserName);

      //Verwijder huidige role
      foreach(string s in UserManager.GetRolesAsync(user.Id).Result)
      {
        UserManager.RemoveFromRole(user.Id, s);
      }
      //Voeg nieuwe role
      UserManager.AddToRole(user.Id, RoleName);
      DefineRoles();
      DefineUsers();
      return View("ManageUserRoles");
    }

    //Wijzig zip
    // GET: /Manage/ChangeZip
    [CustomAuthorize(Roles = "Gebruiker,Superadmin")]
    public ActionResult ChangeZip()
    {
      ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
      ChangeZipViewModel model = new ChangeZipViewModel
      {
        Zip = user.Zip
      };
      return View(model);
    }

    // POST: /Manage/ChangeZip
    [HttpPost]
    [ValidateAntiForgeryToken]
    [CustomAuthorize(Roles = "Gebruiker,Superadmin")]
    public ActionResult ChangeZip(FormCollection formData)
    {
      GemeenteManager gemeentemgr = new GemeenteManager(uowMgr);
      ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
      if (gemeentemgr.GetAllGemeentes().ToList<Gemeente>().FindIndex(g => g.Postcode == short.Parse(formData["postcode"].Substring(0, 4))) >= 0)
      {
        //Als bestaat
        UserManager.ChangeZipOfUser(user.Id, formData["postcode"].Substring(0, 4));
        return RedirectToAction("Index");
      }
      //Als niet bestaat
      ChangeZipViewModel model = new ChangeZipViewModel
      {
        Zip = user.Zip
      };
      ModelState.AddModelError("Foutieve postcode", "Gelieve een geldige postcode te kiezen.");
      return View(model);
    }

    // GET: /Manage/ChangeKerninfo
    [CustomAuthorize(Roles = "Admin,Superadmin")]
    public ActionResult ChangeKerninfo()
    {
      GemeenteManager gemeentemgr = new GemeenteManager(uowMgr);
      ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
      Gemeente gemeente = gemeentemgr.GetGemeente(short.Parse(user.Zip));
      return View(gemeente);
    }

    // POST: /Manage/ChangeKerninfo
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult ChangeKerninfo(Gemeente gemeente)
    {
      GemeenteManager gemeentemgr = new GemeenteManager(uowMgr);
      ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
      gemeentemgr.ChangeGemeente(gemeente);
      uowMgr.Save();
      return RedirectToAction("Index");
    }

    // GET: /Manage/UploadFiles
    [CustomAuthorize(Roles = "Admin,Superadmin,Moderator")]
    public ActionResult UploadFiles(UploadfileModel model = null)
    {
      return View(model);
    }

    //Action voor verschillende Excels te lezen
    [HttpPost]
    public ActionResult UploadCityVAT(HttpPostedFileBase upload)
    {
      GemeenteManager gemeentemgr = new GemeenteManager(uowMgr);
      //Maak model aan
      UploadfileModel model = new UploadfileModel();
      if (upload != null && upload.ContentLength > 0)
      {
        model.Filename = upload.FileName;
        //Maak stream aan
        Stream stream = upload.InputStream;
        if (upload.FileName.EndsWith(".xlsx"))
        {
          gemeentemgr.AddBelastingpercentagesToGemeenteViaStream(stream);
          model.Message = "Gemeentebelastingen succescol opgeladen.";
          uowMgr.Save();
          return RedirectToAction("UploadFiles", model);
        }
        else
        {
          uowMgr.Save();
          model.Message = "Dit type bestand wordt niet ondersteund. Gelieve een bestand met de extensie .xlsx te gebruiken.";
          return RedirectToAction("UploadFiles", model);
        }
      }
      else
      {
        ModelState.AddModelError("File", "Gelieve een bestand te selecteren.");
        uowMgr.Save();
        return RedirectToAction("UploadFiles", model);
      }
    }

    [HttpPost]
    public ActionResult UploadBudgets(HttpPostedFileBase upload)
    {
      GemeenteManager gemeentemgr = new GemeenteManager(uowMgr);
      BegrotingManager begrotingmgr = new BegrotingManager(uowMgr);
      //Maak model aan
      UploadfileModel model = new UploadfileModel();
      if (upload != null && upload.ContentLength > 0)
      {
        model.Filename = upload.FileName;
        //Maak stream aan
        Stream stream = upload.InputStream;
        if (upload.FileName.EndsWith(".xlsx"))
        {
          begrotingmgr.GetBegrotingenFromExcelViaStream(stream);
          model.Message = "Begrotingen succescol opgeladen.";
          uowMgr.Save();
          return RedirectToAction("UploadFiles", model);
        }
        else
        {
          model.Message = "Dit type bestand wordt niet ondersteund. Gelieve een bestand met de extensie .xlsx te gebruiken.";
          uowMgr.Save();
          return RedirectToAction("UploadFiles", model);
        }
      }
      else
      {
        ModelState.AddModelError("File", "Gelieve een bestand te selecteren.");
        return RedirectToAction("UploadFiles", model);
      }
    }
    [CustomAuthorize(Roles = "Admin,Superadmin,Moderator")]
    public ActionResult CheckProposal(int Id)
    {
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      Begrotingsvoorstel voorstel = projectMgr.GetBegrotingsvoorstel(Id);
      return View(voorstel);
    }

    [HttpPost]
    [CustomAuthorize(Roles = "Admin,Superadmin,Moderator")]
    public ActionResult SendMail(string userId, FormCollection data)
    {
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      //return userId + " " + id + " " + data[0] + " " + data[1];
      var choice = data[0];
      var subject = data[1];
      var mail = data[2];
      var budgetpropId = int.Parse(data[3]);
      var user = UserManager.FindById(userId);
      if (user != null)
      {
        //Mail
        string messageBody = String.Format("<div style=\"background-color:#ece8d4;width:100%;height:100%;padding:30px; margin-top:30px;\">"
          + "<p>Geachte heer/mevrouw {0},<p>{1}<p><br/><p> Met vriendelijke groeten,<br/> De Wakkere Burger</p></div>"
          , user.Lastname, mail);
        UserManager.SendEmailAsync(user.Id, subject, messageBody);
        if (choice.Equals("Afwijzen"))
        {
          projectMgr.ChangeBegrotingsvoorstelStaatToAfgekeurd(budgetpropId);
        }
        else
        {
          projectMgr.ChangeBegrotingsvoorstelStaatToGoedgekeurd(budgetpropId);
        }
        uowMgr.Save();
        return RedirectToAction("indexproposals", "Manage");
      }
      else
      {
        ModelState.AddModelError("User not found", new Exception("De gebruiker kon niet gevonden worden."));
        if (choice.Equals("Afwijzen"))
        {
          projectMgr.ChangeBegrotingsvoorstelStaatToAfgekeurd(budgetpropId);
        }
        else
        {
          projectMgr.ChangeBegrotingsvoorstelStaatToGoedgekeurd(budgetpropId);
        }
        uowMgr.Save();
      }
      return RedirectToAction("indexproposals", "Manage");
    }
    [HttpPost]
    public ActionResult EditProposal(FormCollection formData)
    {
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      int draagvlak, totaalBudgetWijziging, id;
      DateTime datum;
      bool idGeparsed = int.TryParse(formData[1], out id);
      int.TryParse(formData[4], out draagvlak);
      int.TryParse(formData[6], out totaalBudgetWijziging);
      DateTime.TryParse(formData[5], out datum);
      Begrotingsvoorstel voorstel = new Begrotingsvoorstel()
      {
        BegrotingsVoorstelId = id,
        KorteBeschrijving = formData[2],
        LangeBeschrijving = formData[3],
        Draagvlak = draagvlak,
        Datum = datum,
        TotaalBudgetwijziging = totaalBudgetWijziging,
        Voorsteller = formData[7],
        GoedKeuringsStaat = BegrotingsvoorstelStaat.InBehandeling
      };
      projectMgr.ChangeBegrotingsvoorstel(voorstel);
      uowMgr.Save();
      return RedirectToAction("checkproposal", "Manage", new { Id = id });
    }

    [CustomAuthorize(Roles = "Admin,Superadmin,Moderator")]
    public ActionResult IndexProposals()
    {
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      List<Begrotingsvoorstel> proposals = new List<Begrotingsvoorstel>();
      List<Begrotingsvoorstel> inProgress = new List<Begrotingsvoorstel>();
      List<Begrotingsvoorstel> accepted = new List<Begrotingsvoorstel>();
      List<Begrotingsvoorstel> declined = new List<Begrotingsvoorstel>();
      proposals = projectMgr.GetAllBegrotingsvoorstellen().ToList();
      inProgress = proposals.Where(v => v.GoedKeuringsStaat == BegrotingsvoorstelStaat.InBehandeling).ToList();
      ViewBag.inProgress = inProgress;
      accepted = proposals.Where(v => v.GoedKeuringsStaat == BegrotingsvoorstelStaat.Goedgekeurd).ToList();
      ViewBag.accepted = accepted;
      declined = proposals.Where(v => v.GoedKeuringsStaat == BegrotingsvoorstelStaat.Afgekeurd).ToList();
      ViewBag.declined = declined;
      return View();
    }
    // GET: /Manage/CreateProject
    [CustomAuthorize(Roles = "Admin,Superadmin")]
    public ActionResult CreateProject(int year = 0)
    {
      BegrotingManager begrotingMgr = new BegrotingManager(uowMgr);
      ApplicationUser user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
      if (year == 0)
      {
        year = DateTime.Now.Year;
      }
      ViewBag.SelectedYear = year;
      Begroting begroting = (Begroting)begrotingMgr.GetBegrotingsTypeFromGemeente(year, short.Parse(user.Zip));
      ParticipatieProject project = new ParticipatieProject
      {
        Begroting = begroting,
        Startmoment = DateTime.Now
      };
      foreach (CategorieInformatie catinfo in begroting.CategorieInformaties)
      {
        //Min - en max bepalen
        double minbedrag = 0;
        double maxbedrag = 0;
        if (catinfo.Bedrag == 0)
        {
          minbedrag = 0;
          maxbedrag = 20000;
        }
        else
        {
          maxbedrag = Math.Round(catinfo.Bedrag + catinfo.Bedrag / 2);
          minbedrag = Math.Round(catinfo.Bedrag - catinfo.Bedrag / 2);
        }
        project.StartBudgetten.Add(
          new StartBudget
          {
            CategorieInformatie = catinfo,
            Bedrag = catinfo.Bedrag,
            MaximumBedrag = Math.Round(catinfo.Bedrag + catinfo.Bedrag / 2),
            MinimumBedrag = Math.Round(catinfo.Bedrag - catinfo.Bedrag / 2),
            Aanpasbaarheid = StartBudgetAanpasbaarheid.Manueel
          });
      }
      ViewBag.Years = begrotingMgr.GetAllBegrotingsTypes().Where(bbb => bbb.Gemeente.Postcode == short.Parse(user.Zip)).OrderBy(bb => bb.Jaartal).Select(b => b.Jaartal).Distinct().Where(y => y >= DateTime.Now.Year);
      uowMgr.Save();
      return View(project);
    }

    //POST: Create participatieproject
    [HttpPost]
    public ActionResult Create(ParticipatieProject participatieproject, FormCollection formdata)
    {
      BegrotingManager begrotingMgr = new BegrotingManager(uowMgr);
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      GemeenteManager gemeenteMgr = new GemeenteManager(uowMgr);
      ApplicationUser user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
      Begroting begroting = (Begroting)begrotingMgr.GetBegrotingsTypeFromGemeente(DateTime.Now.Year, short.Parse(user.Zip));
      participatieproject.Begroting = begroting;
      //Als besparing negatief maken
      if (participatieproject.Type == ProjectType.Besparing)
      {
        participatieproject.Bedrag = (0 - participatieproject.Bedrag);
      }
      //Startbudgetten uit formcollection toevoegen project
      var ivalue = 8;

      if(participatieproject.Type == ProjectType.Herschikking)
      {
        ivalue = 7;
      }
      List<StartBudget> startbudgetten = new List<StartBudget>();
      var catinfonr = 0;
      for (var i = ivalue; i < formdata.Count; i += 3)
      {
        //Bedrag is een verplicht veld
        var bedrag = begroting.CategorieInformaties.ToList<CategorieInformatie>()[catinfonr].Bedrag;
        var maximumbedrag = double.Parse(formdata[i + 1]);
        if (maximumbedrag == 0)
        {
          maximumbedrag = 20000;
        }
        startbudgetten.Add(new StartBudget { MinimumBedrag = double.Parse(formdata[i]), MaximumBedrag = maximumbedrag, Aanpasbaarheid = (StartBudgetAanpasbaarheid)byte.Parse(formdata[i + 2]), CategorieInformatie = begroting.CategorieInformaties.ToList<CategorieInformatie>()[catinfonr], Bedrag = bedrag });
        catinfonr++;

        if (double.Parse(formdata[i]) > double.Parse(formdata[i + 1]))
        {
          ModelState.AddModelError("Minimumbedrag > maximumbedrag", new Exception("Minimumbedrag moet kleiner zijn dan maximumbedrag."));
        }
      }
      participatieproject.StartBudgetten = startbudgetten;
      if (ModelState.IsValid)
      {
        projectMgr.AddParticipatieProjectToGemeente(participatieproject, short.Parse(user.Zip));
        uowMgr.Save();
        return RedirectToAction("Index");
      }
      else
      {
        return View("CreateProject", participatieproject);
      }
    }
    [CustomAuthorize(Roles = "Admin,Superadmin")]
    public ActionResult ChangeCategoryInfo(int Id)
    {
      BegrotingManager begrotingMgr = new BegrotingManager(uowMgr);
      CategorieInformatie categorieInfo = begrotingMgr.GetCategorieInformatie(Id);
      return View(categorieInfo);
    }
    [CustomAuthorize(Roles = "Admin,Superadmin")]
    public ActionResult Categories(int selected = 0, int year = 0)
    {
      ApplicationUser user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
      BegrotingManager begrotingMgr = new BegrotingManager(uowMgr);
      if (year == 0)
      {
        year = DateTime.Now.Year;
      }
      ViewBag.SelectedYear = year;

      Begroting begroting = (Begroting)begrotingMgr.GetBegrotingsTypeFromGemeente(year, short.Parse(user.Zip));
      ViewBag.CategorieInformaties = begroting.CategorieInformaties;
      ViewBag.Zip = user.Zip;
      ViewBag.Years = begrotingMgr.GetAllBegrotingsTypes().Where(bbb => bbb.Gemeente.Postcode == short.Parse(user.Zip) && bbb.Jaartal >= DateTime.Now.Year).OrderBy(bb => bb.Jaartal).Select(b => b.Jaartal).Distinct();
      if (selected == 0)
      {
        return View(begroting.CategorieInformaties.ToList()[0]);
      }
      else
      {
        return View(begroting.CategorieInformaties.Single(c => c.CategorieInformatieId == selected));
      }
    }
    [HttpPost]
    public ActionResult Categories(PostCategorieInfoModel model)
    {
      BegrotingManager mgr = new BegrotingManager(uowMgr);
      CategorieInformatie categorieInfo;
      if (model.Afbeelding != null)
      {
        byte[] image = Convert.FromBase64String(model.Afbeelding);
        // Get the object used to communicate with the server.
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://10.134.216.25/content/images/categorieinfo/" + model.CategorieInformatieId + ".jpg");
        request.UseBinary = true;
        request.Method = WebRequestMethods.Ftp.UploadFile;
        request.Credentials = new NetworkCredential("bitsplease", "Agronant1");
        request.ContentLength = image.Length;
        Stream reqStream = request.GetRequestStream();
        reqStream.Write(image, 0, image.Length);
        reqStream.Close();
        categorieInfo = new CategorieInformatie()
        {
          CategorieInformatieId = model.CategorieInformatieId,
          Informatie = model.Informatie,
          Afbeelding = "/content/images/categorieinfo/" + model.CategorieInformatieId + ".jpg",
          YoutubeLink = model.YoutubeLink,
          Bedrag = model.Bedrag
        };
      }
      else
      {
        categorieInfo = new CategorieInformatie()
        {
          CategorieInformatieId = model.CategorieInformatieId,
          Informatie = model.Informatie,
          YoutubeLink = model.YoutubeLink,
          Bedrag = model.Bedrag
        };
      }
      mgr.ChangeCategorieInformatie(categorieInfo);
      uowMgr.Save();
      return RedirectToAction("Categories", "manage", new { selected = model.CategorieInformatieId });
    }
    #region Helpers
    // Used for XSRF protection when adding external logins
    private const string XsrfKey = "XsrfId";

    private IAuthenticationManager AuthenticationManager
    {
      get
      {
        return HttpContext.GetOwinContext().Authentication;
      }
    }

    private void AddErrors(IdentityResult result)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError("", error);
      }
    }


    //Add city pic

    [CustomAuthorize(Roles = "Admin,Superadmin")]
    public ActionResult AddCityPic(HttpPostedFileBase file)
    {
      BegrotingManager begrotingmgr = new BegrotingManager(uowMgr);
      GemeenteManager gemeentemgr = new GemeenteManager(uowMgr);
      var user = UserManager.FindById(User.Identity.GetUserId());
      string gemeenteNaam = gemeentemgr.GetGemeente(short.Parse(user.Zip)).Naam;
      if (file != null)
      {
        // Get the object used to communicate with the server.
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://10.134.216.25/content/images/gemeentes/" + gemeenteNaam + ".jpg");
        request.UseBinary = true;
        request.Method = WebRequestMethods.Ftp.UploadFile;
        request.Credentials = new NetworkCredential("bitsplease", "Agronant1");
        StreamReader rdr = new StreamReader(file.InputStream);

        byte[] image;
        using (MemoryStream ms = new MemoryStream())
        {
          file.InputStream.CopyTo(ms);
          image = ms.GetBuffer();


        }


        rdr.Close();
        request.ContentLength = image.Length;
        Stream reqStream = request.GetRequestStream();
        reqStream.Write(image, 0, image.Length);
        reqStream.Close();

      }




      gemeentemgr.ChangeImageOfCity(short.Parse(user.Zip), "/content/images/gemeentes/" + gemeenteNaam + ".jpg");


      uowMgr.Save();

      return RedirectToAction("Index", "City");

    }
    [CustomAuthorize]
    public ActionResult AddProfilePic(HttpPostedFileBase file)
    {


      var user = UserManager.FindById(User.Identity.GetUserId());

      if (file != null)
      {
        // Get the object used to communicate with the server.
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://10.134.216.25/content/images/profielfotos/" + user.Id + ".jpg");
        request.UseBinary = true;
        request.Method = WebRequestMethods.Ftp.UploadFile;
        request.Credentials = new NetworkCredential("bitsplease", "Agronant1");
        StreamReader rdr = new StreamReader(file.InputStream);

        byte[] image;
        using (MemoryStream ms = new MemoryStream())
        {
          file.InputStream.CopyTo(ms);
          image = ms.GetBuffer();


        }


        rdr.Close();
        request.ContentLength = image.Length;
        Stream reqStream = request.GetRequestStream();
        reqStream.Write(image, 0, image.Length);
        reqStream.Close();

      }



      UserManager.ChangeImageOfUser(user.Id, "/content/images/profielfotos/" + user.Id + ".jpg");


      return RedirectToAction("Index", "Manage");

    }

    private bool HasPassword()
    {
      var user = UserManager.FindById(User.Identity.GetUserId());
      if (user != null)
      {
        return user.PasswordHash != null;
      }
      return false;
    }

    private bool HasPhoneNumber()
    {
      var user = UserManager.FindById(User.Identity.GetUserId());
      if (user != null)
      {
        return user.PhoneNumber != null;
      }
      return false;
    }

    public enum ManageMessageId
    {
      AddPhoneSuccess,
      ChangePasswordSuccess,
      SetTwoFactorSuccess,
      SetPasswordSuccess,
      RemoveLoginSuccess,
      RemovePhoneSuccess,
      Error
    }

    #endregion
  }
}