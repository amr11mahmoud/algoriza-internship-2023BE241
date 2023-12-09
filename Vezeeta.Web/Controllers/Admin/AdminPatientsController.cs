using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Service.Users;
using Vezeeta.Service.Dtos.Response.Doctors;
using Vezeeta.Service.Dtos.Response.Patients;

namespace Vezeeta.Web.Controllers.Admin
{
    [Route("Api/Admin/Patients/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class AdminPatientsController : ApplicationController
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public AdminPatientsController(IPatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPatientDto>>> GetAll(int page = 1, int pageSize = 10, string search = "")
        {
            var getPatientsResult = await _patientService.GetAllPatientsAsync(page, pageSize, search);

            if (getPatientsResult.IsFailure)
            {
                return BadRequest(getPatientsResult.Error);
            }

            var response = _mapper.Map<IEnumerable<GetPatientDto>>(getPatientsResult.Value);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<GetPatientAndRequestsDto>> GetById(int id)
        {
            var getPatientResult = await _patientService.GetPatientWithBookingsAsync(id);

            if (getPatientResult.IsFailure)
            {
                return BadRequest(getPatientResult.Error);
            }

            var (patient, bookings) = getPatientResult.Value;

            List<PatientRequestDto> requests = new List<PatientRequestDto>();

            bookings.ForEach(b =>
            {
                var request = new PatientRequestDto
                {
                    Day = b.Time.Appointment.Day.ToString(),
                    DiscountCode = b.Coupon != null ? b.Coupon.Code : string.Empty,
                    DoctorName = b.Doctor.FullName,
                    ImageUrl = b.Doctor.ImageUrl,
                    Specialization = _mapper.Map<SpecializationDto>(b.Doctor.Specialization),
                    Price = b.Doctor.Price,
                    FinalPrice = b.FinalPrice,
                    Status = b.Status.ToString(),
                    Time = b.Time.Time
                };

                requests.Add(request);
            });

            var response = new GetPatientAndRequestsDto
            {
                Details = _mapper.Map<GetPatientDto>(patient),
                Requests = requests
            };

            return Ok(response);
        }
    }
}
