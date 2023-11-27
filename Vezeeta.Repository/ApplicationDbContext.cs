using Castle.Core.Resource;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Core.Domain.User;

namespace Vezeeta.Repository
{
    public interface IApplicationDbContext
    {
        //Task<int> SaveChangesAsync();
    }

    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
