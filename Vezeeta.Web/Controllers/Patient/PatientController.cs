using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Service.Users;
using Vezeeta.Service.Dtos.Response.Doctors;
using Vezeeta.Web.Helpers;

namespace Vezeeta.Web.Controllers.Patient
{
    public class PatientController : ApplicationController
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public PatientController(IDoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }



        [HttpGet("api/[controller]/doctors/[action]")]
        //[Authorize(Roles = AppConsts.Roles.Patient)]
        public async Task<ActionResult<IEnumerable<GetDoctorWithAppointmentsDto>>> GetAll(int page, int pageSize, string search)
        {
            var doctors = await _doctorService.GetAllDoctorsAsync(
                page,
                pageSize,
                search,
                new[] { AppConsts.DomainModels.DoctorSpecialization, AppConsts.DomainModels.AppointmentsAndTimes });

            return Ok(_mapper.Map<IEnumerable<GetDoctorWithAppointmentsDto>>(doctors));
        }
    }
}
