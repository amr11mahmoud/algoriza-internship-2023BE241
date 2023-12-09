using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Service.Users;
using Vezeeta.Core.Shared;
using Vezeeta.Service.Dtos.Request.Doctors;
using Vezeeta.Service.Dtos.Response.Doctors;
using Vezeeta.Web.Helpers;

namespace Vezeeta.Web.Controllers.Admin
{
    [Route("Api/Admin/Doctors/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class AdminDoctorsController : ApplicationController
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        private readonly IImageHelper _imageHelper;


        public AdminDoctorsController(IDoctorService doctorService, IMapper mapper, IImageHelper imageHelper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            _imageHelper = imageHelper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDoctorDto>>> GetAll(int page = 1, int pageSize = 10, string search = "")
        {
            var doctors = await _doctorService.GetAllDoctorsAsync(page, pageSize, search, new[] { AppConsts.DomainModels.Specialization });
            var response = _mapper.Map<IEnumerable<GetDoctorDto>>(doctors);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<GetDoctorDto>> GetById(int id)
        {
            var doctor = await _doctorService.GetDoctorAsync(id, new[] { AppConsts.DomainModels.Specialization });
            var response = _mapper.Map<GetDoctorDto>(doctor);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Add([FromForm] AddDoctorDto request)
        {
            User user = _mapper.Map<User>(request);

            Result<string> uploadImageResult = _imageHelper.UploadImage(request.Image);

            if (uploadImageResult.IsFailure) return BadRequest(uploadImageResult.Error);

            user.ImageUrl = uploadImageResult.Value;

            Result<bool> AddDoctorResult = await _doctorService.AddDoctorAsync(user);

            if (AddDoctorResult.IsFailure) return BadRequest(AddDoctorResult.Error);

            return Ok(true);
        }


        [HttpPut]
        public async Task<ActionResult<bool>> Edit([FromForm] EditDoctorDto request)
        {
            User user = _mapper.Map<User>(request);

            if (request.Image != null)
            {
                Result<string> uploadImageResult = _imageHelper.UploadImage(request.Image);

                if (uploadImageResult.IsFailure) return BadRequest(uploadImageResult.Error);

                user.ImageUrl = uploadImageResult.Value;
            }

            Result<bool> AddDoctorResult = await _doctorService.UpdateDoctorAsync(user);

            if (AddDoctorResult.IsFailure) return BadRequest(AddDoctorResult.Error);

            return Ok(true);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            Result<bool> deleteDoctorResult = await _doctorService.DeleteDoctorAsync(id);

            if (deleteDoctorResult.IsFailure) return BadRequest(deleteDoctorResult.Error);

            return Ok(true);
        }
    }
}
