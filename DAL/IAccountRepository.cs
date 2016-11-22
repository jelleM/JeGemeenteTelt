using BEP.BL.Models.Gebruikers;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace BEP.DAL
{
    public interface IAccountRepository
    {
        IEnumerable<IdentityRole> ReadRoles();
        IEnumerable<ApplicationUser> ReadUsers();
        void DeleteRolesOfUser(string userId);
        void ChangeZip(string userId, string newZip);
        void ChangeImage(string userId, string image);
        ApplicationUser FindUserByUsername(string username);
    }
}
