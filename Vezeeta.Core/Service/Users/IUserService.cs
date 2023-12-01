using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Shared;

namespace Vezeeta.Core.Service.Users
{
    public interface IUserService
    {
        Task<Result<bool>> RegisterUserAsync(User user, string password);
        Task<Result<UserJwtToken>> LoginUserAsync(string email, string password);
        //Task<UserJwtToken> GenerateUserTokensAsync(User user);
        //JWTToken GenerateAccessToken(User user);
        //Task<JWTToken> GenerateRefreshTokenAsync(User user);
        //Task StoreRefreshToken(UserRefreshToken token);
    }
}
