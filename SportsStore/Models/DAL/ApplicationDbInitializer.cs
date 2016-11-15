using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SportsStore.Models.Domain;

namespace SportsStore.Models.DAL
{
   
        public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
        {
            private ApplicationUserManager userManager;
            private ApplicationRoleManager roleManager;
            protected override void Seed(ApplicationDbContext context)
            {
                userManager =
                  HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

                roleManager =
                   HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

                // InitializeIdentity();
                InitializeIdentityAndRoles();
                base.Seed(context);
            }

            private void InitializeIdentityAndRoles()
            {
                CreateUserAndRoles( "admin@sportsstore.be", "P@ssword1", "admin");
                for (int i = 1; i < 10; i++)
                {
                    CreateUserAndRoles("Student" + i + "@hogent.be", "P@ssword1", "customer");
                }
            }


            private void CreateUserAndRoles( string email, string password, string roleName)
            {
                //Create user
                ApplicationUser user = userManager.FindByName(email);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = email, Email = email, LockoutEnabled = false };
                    IdentityResult result = userManager.Create(user, password);
                    if (!result.Succeeded)
                        throw new ApplicationException(result.Errors.ToString());
                }

                //Create roles
                IdentityRole role = roleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new IdentityRole(roleName);
                    IdentityResult result = roleManager.Create(role);
                    if (!result.Succeeded)
                        throw new ApplicationException(result.Errors.ToString());
                }

                //Associate user with role
                IList<string> rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(role.Name))
                {
                    IdentityResult result = userManager.AddToRole(user.Id, roleName);
                    if (!result.Succeeded)
                        throw new ApplicationException(result.Errors.ToString());
                }
            }
        }
    }
