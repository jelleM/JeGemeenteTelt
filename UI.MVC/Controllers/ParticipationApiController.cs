using BEP.BL;
using BEP.BL.Models.Gemeentes;
using BEP.BL.Models.ParticipatieProjecten;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BEP.UI.MVC.Controllers
{
  public class ParticipationApiController : ApiController
  {
    UnitOfWorkManager uowMgr = new UnitOfWorkManager();

    public ParticipationApiController()
    {
    }
    [Route("api/participation/{zip}/allproposals")]
    public IEnumerable<Begrotingsvoorstel> Get(short zip)
    {
      GemeenteManager gemeenteMgr = new GemeenteManager(uowMgr);
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      Gemeente city = gemeenteMgr.GetGemeente(zip);
      List<Begrotingsvoorstel> voorstellen = projectMgr.GetAllBegrotingsvoorstellen().Where(bv => bv.Begroting.Gemeente.Postcode == zip).ToList<Begrotingsvoorstel>();
      return voorstellen;
    }

    [Route("api/participation/android/{zip}/allproposals")]
    public IEnumerable<Begrotingsvoorstel> Get(short zip, bool android = true)
    {
      GemeenteManager gemeenteMgr = new GemeenteManager(uowMgr);
      ProjectManager projectMgr = new ProjectManager(uowMgr);
      Gemeente city = gemeenteMgr.GetGemeente(zip);
      List<Begrotingsvoorstel> voorstellen = projectMgr.GetAllBegrotingsvoorstellenAndroid().Where(bv => bv.Begroting.Gemeente.Postcode == zip && bv.GoedKeuringsStaat != BegrotingsvoorstelStaat.Afgekeurd).OrderBy(beg => beg.Draagvlak).Reverse().ToList<Begrotingsvoorstel>();
      //begrotingen terug op null zetten (performantie android)
      
      for (var i = 0; i < voorstellen.Count; i++)
      {
        voorstellen.ToList<Begrotingsvoorstel>()[0].Begroting = null;
      }
      return voorstellen;
    }
  }
}
