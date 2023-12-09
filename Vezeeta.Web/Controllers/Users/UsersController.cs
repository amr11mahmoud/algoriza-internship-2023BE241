using AutoMapper;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Service.Users;
using Vezeeta.Core.Shared;
using Vezeeta.Service.Dtos.Request.Base;
using Vezeeta.Service.Dtos.Request.Patients;
using Vezeeta.Web.Helpers;

namespace Vezeeta.Web.Controllers.Users
{
    [Route("Api/[controller]/[action]")]
    public class UsersController: ApplicationController
    {
        private readonly IImageHelper _imageHelper;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IImageHelper imageHelper, IUserService userService, IMapper mapper)
        {
            _imageHelper = imageHelper;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Register([FromForm] RegisterPatientDto request)
        {
            string imageUrl = "";

            if (request.Image != null)
            {
                Result<string> UploadImageResult = _imageHelper.UploadImage(request.Image);
                if (UploadImageResult.IsFailure) return BadRequest(UploadImageResult.Error);

                imageUrl = UploadImageResult.Value;
            }

            User user = _mapper.Map<User>(request);
            user.ImageUrl = imageUrl;

            Result<User> registerUserResult = 
                await _userService.RegisterUserAsync(user, request.Password, UserDiscriminator.Patient, new[] {AppConsts.Roles.Patient});

            if (registerUserResult.IsFailure) return BadRequest(registerUserResult.Error);

            return Ok(true);
        }

        [HttpPost]
        public async Task<ActionResult<UserJwtToken>> Login(LoginUserDto request)
        {
            Result<UserJwtToken> loginResult = await _userService.LoginUserAsync(request.Email, request.Password);

            if (loginResult.IsFailure) return BadRequest(loginResult.Error);

            return Ok(loginResult.Value);
        }

        [HttpGet("Google")]
        [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
        public ActionResult<bool> ExternalLogin()
        {
            return Ok(true);
        }
    }
}
