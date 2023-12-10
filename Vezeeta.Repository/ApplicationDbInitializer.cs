using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Domain.Users;

namespace Vezeeta.Repository
{
    public static class ApplicationDbInitializer
    {
        public static async Task<bool> SeedAdmin(UserManager<User> userManager)
        {
            var admin = await userManager.FindByEmailAsync(AppConsts.User.AdminEmail);
            if (admin == null)
            {
                User user = new User
                {
                    UserName = "admin",
                    Email = "admin@test.com",
                };

                IdentityResult result = await userManager.CreateAsync(user, AppConsts.User.AdminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            return true;
        }
    }
}
