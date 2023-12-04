using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Vezeeta.Core.Domain.Appointments;
using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Domain.Lookup;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Shared;

namespace Vezeeta.Core.Domain.Users
{
    public class User : IdentityUser<int>
    {
        private const byte NameMinLength = 3;
        private const byte NameMaxLength = 32;

        public User()
        {
            UserName = Guid.NewGuid().ToString();
            SecurityStamp = Guid.NewGuid().ToString();
        }

        public string FullName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public UserDiscriminator Discriminator { get; set; }

        [ForeignKey("Specialization")]
        public int? SpecializationId { get; set; } = null;
        public string? ImageUrl { get; set; }
        public float Price { get; set; }
        public virtual Specialization? Specialization { get; set; }
        public virtual IEnumerable<Appointment>? Appointments { get; set; }

        public static Result SetUserPassword(User user, string password)
        {
            user.PasswordHash = password;

            return Result.Success();
        }

        public void Update(User newData)
        {
            FirstName = newData.FirstName;
            LastName = newData.LastName;
            Email = newData.Email;
            PhoneNumber = newData.PhoneNumber;
            SpecializationId = newData.SpecializationId;
            Gender = newData.Gender;
            DateOfBirth = newData.DateOfBirth;

            if (!string.IsNullOrEmpty(newData.ImageUrl)) ImageUrl = newData.ImageUrl;
        }
    }
}
