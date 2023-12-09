using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Service.Bookings;
using Vezeeta.Core.Service.Users;
using Vezeeta.Service.Dtos.Response.Admin;

namespace Vezeeta.Web.Controllers.Admin
{
    [Route("Api/Admin/Dashboard")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class AdminDashboardController : ApplicationController
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        public AdminDashboardController(
            IDoctorService doctorService,
            IPatientService patientService,
            IBookingService bookingService,
            IMapper mapper)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _bookingService = bookingService;
            _mapper = mapper;
        }


        [HttpGet("Doctors/Count")]
        public async Task<ActionResult<int>> GetNumberOfDoctors()
        {
            int numOfDoctors = await _doctorService.GetDoctorsCountAsync();
            return Ok(numOfDoctors);
        }

        [HttpGet("Patients/Count")]
        public async Task<ActionResult<int>> GetNumberOfPatients()
        {
            int numberOfPatients = await _patientService.GetPatientsCountAsync();
            return Ok(numberOfPatients);
        }


        [HttpGet("Bookings/Count")]
        public async Task<ActionResult<NumberOfRequestsResponseDto>> GetNumberOfRequests()
        {
            var bookingsCountList = await _bookingService.GetBookingsCountAsync();

            var response = new NumberOfRequestsResponseDto
            {
                CompletedRequests = bookingsCountList[0],
                PendingRequests = bookingsCountList[1],
                CancelledRequests = bookingsCountList[2],
                TotalRequests = bookingsCountList[3],
            };

            return Ok(response);
        }

        [HttpGet("Specialization/GetTopFive")]
        public async Task<ActionResult<IEnumerable<TopSpecializationResponseDto>>> TopFiveSpecializations()
        {
            var specializations = await _doctorService.GetTopSpecialization(5);

            var response = _mapper.Map<IEnumerable<TopSpecializationResponseDto>>(specializations);

            return Ok(response);
        }

        [HttpGet("Doctors/GetTopTen")]
        public ActionResult<IEnumerable<TopDoctorsResponseDto>> TopTenDoctors()
        {
            var doctors = _doctorService.GetTopDoctors(10);

            var response = _mapper.Map<IEnumerable<TopDoctorsResponseDto>>(doctors);

            return Ok(response);
        }
    }
}
