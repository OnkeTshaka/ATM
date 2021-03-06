using ATM.Models;
using ATM.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Security.Claims;
using System.Web;

[assembly: OwinStartupAttribute(typeof(ATM.Startup))]
namespace ATM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        // In this method we will create default User roles and Admin user for login
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User 
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin role
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website				

                var user = new ApplicationUser();
                user.UserName = "firewalls@gmail.com";
                user.Email = "firewalls@gmail.com";
                user.UserPhoto = null;

                string userPWD = "Fire101Walls#@";

                var chkUser = UserManager.Create(user, userPWD);
                UserManager.AddClaim(user.Id, new Claim(ClaimTypes.GivenName, "Admin"));

                var service = new CheckingAccountService(context);
                service.CreateCheckingAccount("Admin", "User", user.Id, 1000);
                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

        }
    }
}
