using System;
using System.Web.Http;
using BEP.BL;
using BEP.BL.Models.Gebruikers;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace BEP.UI.MVC.Controllers
{
    public class AccountApiController : ApiController
    {

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

        public AccountApiController()
        {

        }

    // GET: api/Accountapi
    [Route("api/Account/Checkuser")]
    public bool Get(string username,string password)
    {
      try
      {
        string pw = password.Replace("$\\","");
        ApplicationUser user = UserManager.GetUserByUsername(username);
        bool result = UserManager.CheckPasswordAsync(user, pw).Result;
        return result;
      }
      catch (Exception)
      {
        return false;
      }

    }

        // GET: api/Accountapi
        [Route("api/Account")]
        public ApplicationUser Get(string username)
        {
            try
            {
                ApplicationUser user = UserManager.GetUserByUsername(username);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
