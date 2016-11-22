using Microsoft.AspNet.Identity;
using System.Web.Http;
using BEP.BL;
using BEP.BL.Models.ParticipatieProjecten;
using BEP.BL.Models.Gebruikers;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;

namespace BEP.UI.MVC.Controllers
{
  public class BudgetproposalApiController : ApiController
  {
    UnitOfWorkManager uowMgr = new UnitOfWorkManager();
    private ApplicationUserManager _userManager;
    public ApplicationUserManager UserManager
    {
      get
      {
        return _userManager ?? HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
      private set
      {
        _userManager = value;
      }
    }

    public BudgetproposalApiController()
    {

    }
    public class Like
    {
      public int BegrotingsvoorstelId { get; set; }
      public bool Increment { get; set; }
    }


    [HttpGet]
    [Route("api/Budgetproposal/{id}")]
    public Begrotingsvoorstel Get(int id)
    {
      ProjectManager manager = new ProjectManager(uowMgr);
      Begrotingsvoorstel voorstel = manager.GetBegrotingsvoorstel(id);
      return voorstel;
    }

    //Route ongewijzigd laten
    [Route("api/Budgetproposal")]
    public void Post(Like like)
    {
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      string CurrentUserId = RequestContext.Principal.Identity.GetUserId();
      if (CurrentUserId != null)
      {
        if (like.Increment == true)
        {
          projectMgr.AddLikesToBegrotingsvoorstel(CurrentUserId, like.BegrotingsvoorstelId);
          projectMgr.VermeerderDraagvlak(like.BegrotingsvoorstelId);
        }
        else
        {
          projectMgr.RemoveLikesFromBegrotingsvoorstel(CurrentUserId, like.BegrotingsvoorstelId);
          projectMgr.VerminderDraagvlak(like.BegrotingsvoorstelId);
        }

        uowMgr.Save();

      }
    }
    //Android
    [Route("api/BudgetproposalAndroid")]
    public void Post(string begrotingsvoostelId,bool increment, string userId)
    {
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      string CurrentUserId = userId;
      if (CurrentUserId != null)
      {
        if (increment == true)
        {
          projectMgr.AddLikesToBegrotingsvoorstel(CurrentUserId, int.Parse(begrotingsvoostelId));
          projectMgr.VermeerderDraagvlak(int.Parse(begrotingsvoostelId));
        }
        else
        {
          projectMgr.RemoveLikesFromBegrotingsvoorstel(CurrentUserId, int.Parse(begrotingsvoostelId));
          projectMgr.VerminderDraagvlak(int.Parse(begrotingsvoostelId));
        }

        uowMgr.Save();

      }
    }

    [HttpGet]
    [Route("api/Budgetproposal/Status/{id}/{condition}")]
    public void Status(int id, string condition)
    {
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      if (condition.Equals("accept"))
      {

        projectMgr.ChangeBegrotingsvoorstelStaatToGoedgekeurd(id);
      }
      else {

        projectMgr.ChangeBegrotingsvoorstelStaatToAfgekeurd(id);
      }

      uowMgr.Save();
    }

    [HttpGet]
    [Route("api/Budgetproposal/Reactionusers/{begrotingsvoorstelId}")]
    public IEnumerable<ApplicationUser> Get(int begrotingsvoorstelId,bool getreacties = true)
    {
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      Begrotingsvoorstel voorstel = projectMgr.GetBegrotingsvoorstel(begrotingsvoorstelId);
      List<ApplicationUser> users = new List<ApplicationUser>();
      if(voorstel != null)
      {
        foreach(Reactie r in voorstel.Reacties)
        {
          //User van reactie
          ApplicationUser user = UserManager.GetAllUsers().SingleOrDefault(u => u.Id == r.UserId);
          if(user != null)
          {
            users.Add(user);
          }

          //User van subreactie
          foreach (Reactie subr in r.SubReacties)
          {
            ApplicationUser userSub = UserManager.GetAllUsers().SingleOrDefault(u => u.Id == subr.UserId);
            if (userSub != null)
            {
              users.Add(userSub);
            }
          }

        }
      }
      
      return users.Distinct();
    }
  }
}
