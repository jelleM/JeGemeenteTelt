using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SendGrid;
using System.Diagnostics;
using BEP.DAL;
using BEP.DAL.EF;
using BEP.BL.Models.Gebruikers;

namespace BEP.BL
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task configSendGridasync(IdentityMessage message)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(message.Destination);
            myMessage.From = new System.Net.Mail.MailAddress(
                                "bitsplease2016@gmail.com", "Bits Please");
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;



            // Create a Web transport for sending email.
            var transportWeb = new Web("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");


            // Send the email.
            if (transportWeb != null)
            {
                await transportWeb.DeliverAsync(myMessage);
            }
            else
            {
                Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
            }
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly IAccountRepository accountrepo;
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
            accountrepo = new AccountRepository();
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        //Get all roles
        public IEnumerable<IdentityRole> GetAllRoles()
        {

            return accountrepo.ReadRoles();
        }

        //Get all users
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return accountrepo.ReadUsers();
        }

        //Rollen van user verwijderen
        public void RemoveRolesOfUser(string userId)
        {
            accountrepo.DeleteRolesOfUser(userId);
        }

        //Zip van user veranderen
        public void ChangeZipOfUser(string userId, string newZip)
        {
            accountrepo.ChangeZip(userId, newZip);
        }

        //Profile pic van user veranderen

        public void ChangeImageOfUser(string userId, string image)
        {
            accountrepo.ChangeImage(userId, image);
        }

        //Gebruiker bij username ophalen (= uniek e-mailadres)

        public ApplicationUser GetUserByUsername(string username)
        {
            return accountrepo.FindUserByUsername(username);
        }

    }
}
