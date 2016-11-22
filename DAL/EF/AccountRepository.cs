using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using BEP.BL.Models.Gebruikers;

namespace BEP.DAL.EF
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext ctx = new ApplicationDbContext();
        public AccountRepository()
        {
            ctx = new ApplicationDbContext();
        }

        public void ChangeImage(string userId, string image)
        {
            ctx.Users.Find(userId).Image = image;
            ctx.SaveChanges();
        }

        public void ChangeZip(string userId, string newZip)
        {
            ctx.Users.Find(userId).Zip = newZip;
            ctx.SaveChanges();
        }

        public void DeleteRolesOfUser(string userId)
        {
            ctx.Users.Find(userId).Roles.Clear();
            ctx.SaveChanges();
        }

        public ApplicationUser FindUserByUsername(string username)
        {
            return ctx.Users.Single(a => a.UserName == username);
        }

        public IEnumerable<IdentityRole> ReadRoles()
        {
            return ctx.Roles.AsEnumerable();
        }

        public IEnumerable<ApplicationUser> ReadUsers()
        {
            return ctx.Users.AsEnumerable();
        }
    }
}
