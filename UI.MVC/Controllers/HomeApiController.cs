using BEP.BL;
using BEP.BL.Models.Gemeentes;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BEP.UI.MVC.Controllers
{
    public class HomeApiController : ApiController
    {
        UnitOfWorkManager uowMgr = new UnitOfWorkManager();
        [Route("api/home/all")]
        public IEnumerable<Gemeente> Get()
        {
            GemeenteManager gemeentemgr = new GemeenteManager(uowMgr);
            List<Gemeente> gemeentes = new List<Gemeente>();
            gemeentes = gemeentemgr.GetAllGemeentes().Where(g => g.ParticipatieProjecten.Count > 0).ToList();
            return gemeentes;
        }
    }
}