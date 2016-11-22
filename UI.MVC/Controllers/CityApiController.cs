using BEP.BL;
using BEP.BL.Models.Gemeentes;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BEP.UI.MVC.Controllers
{
    public class CityApiController : ApiController
    {
        UnitOfWorkManager uowMgr = new UnitOfWorkManager();
        public CityApiController()
        {

        }

        [Route("api/city/all")]
        public IEnumerable<string> Get()
        {
            GemeenteManager manager = new GemeenteManager(uowMgr);
            List<Gemeente> gemeentes = manager.GetAllGemeentes().ToList();
            List<string> postcodesEnNamen = new List<string>();
            foreach (var gemeente in gemeentes)
            {
                postcodesEnNamen.Add(gemeente.Postcode + " " + gemeente.Naam);
            }
            return postcodesEnNamen;
        }

        [Route("api/city/{id}")]
        public Gemeente Get(short id)
        {
            GemeenteManager manager = new GemeenteManager(uowMgr);
            Gemeente gemeente = manager.GetGemeente(id);
            return gemeente;
        }
    }
}
