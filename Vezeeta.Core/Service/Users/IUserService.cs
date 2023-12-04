using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Shared;

namespace Vezeeta.Core.Service.Users
{
    public interface IUserService
    {
        Task<Result<User>> RegisterUserAsync(User user, string password, UserDiscriminator discriminator);
        Task<Result<UserJwtToken>> LoginUserAsync(string email, string password);
        Task<Result<bool>> AddUserToRoleAsync(User user, string role);
        Task<Result<bool>> UpdateUserAsync(User user);
        Task<Result<bool>> DeleteUserAsync(int id);
    }
}
