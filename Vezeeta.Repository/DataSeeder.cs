using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vezeeta.Core.Domain.Users;

namespace Vezeeta.Repository
{
    public static class DataSeeder
    {
        public static void SeedRoles(this ApplicationDbContext context)
        {
            string[] roles = new string[] { "Admin", "Doctor", "Patient" };

            var rolesToAdd = new List<Role>();

            foreach (string role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    rolesToAdd.Add(new Role(role));
                }
            }

            context.Roles.AddRange(rolesToAdd);
            context.SaveChanges();
        }
    }
}
