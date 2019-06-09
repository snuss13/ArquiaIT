using System;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ArquiaIT.Models;
using ArquiaIT.Models.Business;
using System.Data.Entity;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(ArquiaIT.Startup))]
namespace ArquiaIT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
            CreateDBSystemConfiguration();
        }

        private void CreateDBSystemConfiguration()
        {
            ArquiaEntities businessDB = new ArquiaEntities();
            SystemConfiguration sysConf = businessDB.SystemConfiguration.OrderByDescending(x =>x.Id).FirstOrDefault();

            if (sysConf == null)
            {
                sysConf = new SystemConfiguration();
                sysConf.LastChangeRate = 45;

                businessDB.SystemConfiguration.Add(sysConf);
                businessDB.SaveChanges();
            }
        }

        private void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Administrator"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Administrator";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@admin.com";

                string userPWD = "Admin#123";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
                
            }

        }
    }
}
