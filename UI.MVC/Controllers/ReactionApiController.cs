using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BEP.BL;
using BEP.BL.Models.ParticipatieProjecten;

namespace BEP.UI.MVC.Controllers
{
  public class ReactionApiController : ApiController
  {
    UnitOfWorkManager uowMgr = new UnitOfWorkManager();
    public ReactionApiController()
    {
    }

    public class ReactieLike
    {
      public int ReactieId { get; set; }
      public bool Increment { get; set; }
    }
    [HttpGet]
    [Route("api/reactionapicontroller/all")]
    public IEnumerable<Reactie> Get()
    {
      ProjectManager manager = new ProjectManager(uowMgr);
      List<Reactie> reacties = manager.GetAllReacties().ToList();
      return reacties;
    }

    //Route ongewijzigd laten
    [Route("api/Reactionlike")]
    public void Post(ReactieLike like)
    {
      ProjectManager mgr = new ProjectManager(uowMgr);
      string CurrentUserId = RequestContext.Principal.Identity.GetUserId();

      if (CurrentUserId != null)
      {
        if (like.Increment == true)
        {
          mgr.AddLikesToReactie(CurrentUserId, like.ReactieId);
          mgr.VermeerderDraagvlakReactie(like.ReactieId);
        }
        else
        {
          mgr.RemoveLikesFromReactie(CurrentUserId, like.ReactieId);
          mgr.VerminderDraagvlakReactie(like.ReactieId);
        }
        uowMgr.Save();
      }
    }

    [Route("api/reaction/ReactionlikeAndroid")]
    public void Post(int reactieId,bool increment,string userId)
    {
      ProjectManager mgr = new ProjectManager(uowMgr);

     
        if (increment == true)
        {
          mgr.AddLikesToReactie(userId, reactieId);
          mgr.VermeerderDraagvlakReactie(reactieId);
        }
        else
        {
          mgr.RemoveLikesFromReactie(userId, reactieId);
          mgr.VerminderDraagvlakReactie(reactieId);
        }
        uowMgr.Save();
      
    }
  }
}
