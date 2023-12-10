using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Vezeeta.Core.Enums;

namespace Vezeeta.Service.Dtos.Request.Base
{
    public class AddUserDto
    {
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
