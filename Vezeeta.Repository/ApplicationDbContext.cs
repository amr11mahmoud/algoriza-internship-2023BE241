﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Vezeeta.Core.Domain.Appointments;
using Vezeeta.Core.Domain.Base;
using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Domain.Coupons;
using Vezeeta.Core.Domain.Lookup;
using Vezeeta.Core.Domain.Users;

namespace Vezeeta.Repository
{
    public interface IApplicationDbContext
    {
    }

    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentTime> AppointmentTimes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().Property(u => u.FullName).HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

            builder.Entity<Appointment>()
                .HasIndex(p => new { p.Day, p.DoctorId }).IsUnique();

            builder.Entity<AppointmentTime>()
                .HasIndex(p => new { p.Time, p.AppointmentId }).IsUnique();

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

            builder.Entity<Booking>()
                .HasOne(b => b.Doctor)
                .WithMany()
                .HasForeignKey(b => b.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Booking>()
                .HasOne(b => b.Patient)
                .WithMany()
                .HasForeignKey(b => b.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.SeedRoles();
            builder.SeedSpecializations();
        }
    }
}
