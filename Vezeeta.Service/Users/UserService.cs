using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Repository;
using Vezeeta.Core.Service.Users;
using Vezeeta.Core.Shared;
using static Vezeeta.Core.Shared.Error;

namespace Vezeeta.Service.Users
{
    public class UserService : UserManager<User>, IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private IBaseRepository<UserRefreshToken> _refreshTokenRepo;
        private readonly IConfiguration _configuration;

        public UserService(
            IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors,
            IServiceProvider services, 
            ILogger<UserManager<User>> logger, 
            SignInManager<User> signInManager,
            IBaseRepository<UserRefreshToken> refreshTokenRepo,
            IConfiguration configuration)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _signInManager = signInManager;
            _refreshTokenRepo = refreshTokenRepo;
            _configuration = configuration;
        }

        public async Task<Result<UserJwtToken>> LoginUserAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email)) return Result.Failure<UserJwtToken>(Errors.Users.EmailError());
            if (string.IsNullOrEmpty(password)) return Result.Failure<UserJwtToken>(Errors.Users.InvalidPassword());

            User? user = await FindByEmailAsync(email);
            if (user == null) return Result.Failure<UserJwtToken>(Errors.Users.UserNotFound(email));

            bool canSignIn = await _signInManager.CanSignInAsync(user);
            if (!canSignIn) return Result.Failure<UserJwtToken>(Errors.Users.UserCanNotSignIn());

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, password, true, false);
            if (signInResult.IsNotAllowed) return Result.Failure<UserJwtToken>(Errors.Users.UserSignInNotAllowed());
            if (signInResult.IsLockedOut) return Result.Failure<UserJwtToken>(Errors.Users.UserLockout());
            if (!signInResult.Succeeded) return Result.Failure<UserJwtToken>(Errors.Users.InvalidPassword());

            UserJwtToken userToken = await GenerateUserTokens(user);

            return Result.Success(userToken);
        }

        public async Task<Result<bool>> RegisterUserAsync(User user, string password)
        {
            string hashedPassword = PasswordHasher.HashPassword(user, password);

            Result setPasswordResult = User.SetUserPassword(user, hashedPassword);

            if (setPasswordResult.IsFailure) return Result.Failure<bool>(setPasswordResult.Error);

            IdentityResult createUserResult = await CreateAsync(user);

            if (!createUserResult.Succeeded)
            {
                string errorCode = createUserResult.Errors.FirstOrDefault() is null ? "NULL" : createUserResult.Errors.FirstOrDefault().Code;
                string errorMsg = createUserResult.Errors.FirstOrDefault() is null ? "NULL" : createUserResult.Errors.FirstOrDefault().Description;
                return Result.Failure<bool>(Errors.Users.RegisterUserError(errorCode, errorMsg));
            }
            return Result.Success(true);
        }

        private async Task<UserJwtToken> GenerateUserTokens(User user)
        {
            return new UserJwtToken
            {
                AccessToken = GenerateAccessToken(user),
                RefreshToken = await GenerateRefreshToken(user),
            };
        }

        private JWTToken GenerateAccessToken(User user)
        {
            var claims = new List<Claim> {
                new Claim( ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            string key = _configuration["Authentication:JwtBearer:SecurityKey"];

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(AppConsts.User.TokenExpiryDays);
            var token = new JwtSecurityToken(
                issuer: _configuration["Authentication:JwtBearer:ValidIssuer"],
                audience: _configuration["Authentication:JwtBearer:ValidAudience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JWTToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        private async Task<JWTToken> GenerateRefreshToken(User user)
        {
            var claims = new List<Claim> {
                new Claim( ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JwtBearer:RefreshSecurityKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha384);
            var expiration = DateTime.UtcNow.AddDays(AppConsts.User.RefreshTokenExpiryDays);

            var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                issuer: _configuration["Authentication:JwtBearer:Issuer"],
                audience: _configuration["Authentication:JwtBearer:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            ));

            UserRefreshToken refreshToken = new UserRefreshToken
            {
                UserId = user.Id,
                Token = token,
                ExpirationDate = expiration,
                IsUsed = false
            };

            await _refreshTokenRepo.AddAsync(refreshToken);
            await _refreshTokenRepo.SaveChangesAsync();

            return new JWTToken
            {
                Token = token,
                Expiration = expiration
            };
        }
    }
}
