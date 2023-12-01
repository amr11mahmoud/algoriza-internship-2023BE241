using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Vezeeta.Core.Domain.Lookup;
using Vezeeta.Core.Domain.Users.ValueObjects;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Shared;
using static Vezeeta.Core.Shared.Error;

namespace Vezeeta.Core.Domain.Users
{
    public class User : IdentityUser<Guid>
    {
        private const byte NameMinLength = 4;
        private const byte NameMaxLength = 32;

        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public UserDiscriminator Discriminator { get; set; }

        [ForeignKey("Specialization")]
        public int? Specialize { get; set; }
        public string? ImageUrl { get; set; }
        public virtual Specialization? Specialization { get; set; }

        private static Result<User> Create(string fName, string lName, Email email)
        {
            Result nameResult = IsValidName(fName, lName);

            if (nameResult.IsFailure) return Result.Failure<User>(nameResult.Error);

            User user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = fName,
                LastName = lName,
                Email = email.Value,
                UserName = Guid.NewGuid().ToString()
            };

            return Result.Success(user);
        }

        public static Result<User> Create(string fName, string lName, string email)
        {
            Result<Email> emailResult = ValueObjects.Email.Create(email);
            if (emailResult.IsFailure) return Result.Failure<User>(emailResult.Error);

            return Create(fName, lName, emailResult.Value);
        }

        public static Result SetUserPassword(User user, string password)
        {
            user.PasswordHash = password;

            return Result.Success();
        }

        private static Result IsValidName(string fName, string lName)
        {
            if (string.IsNullOrEmpty(fName)) return Result.Failure(Errors.General.IsRequiredError("First Name"));
            if (string.IsNullOrEmpty(lName)) return Result.Failure(Errors.General.IsRequiredError("Last Name"));

            if (fName.Length > NameMaxLength || fName.Length < NameMinLength)
                return Result.Failure(Errors.General.LengthError("First Name", NameMinLength, NameMaxLength));

            if (lName.Length > NameMaxLength || lName.Length < NameMinLength)
                return Result.Failure(Errors.General.LengthError("Last Name", NameMinLength, NameMaxLength));

            return Result.Success();
        }
    }
}
