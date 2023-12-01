using Castle.Core.Resource;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vezeeta.Core.Domain.Lookup;
using Vezeeta.Core.Domain.Users;

namespace Vezeeta.Repository
{
    public interface IApplicationDbContext
    {
    }

    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        //public DbSet<Specialization> Specializations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            builder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            builder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<UserClaim>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<UserLogin>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<RoleClaim>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<UserToken>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            builder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }, new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Doctor",
                    NormalizedName = "DOCTOR",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }, new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Patient",
                    NormalizedName = "PATIENT",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });
        }
    }
}
