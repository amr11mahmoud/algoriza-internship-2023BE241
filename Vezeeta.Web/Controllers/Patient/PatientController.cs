using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Service.Bookings;
using Vezeeta.Core.Service.Users;
using Vezeeta.Core.Shared;
using Vezeeta.Service.Dtos.Request.Appointments;
using Vezeeta.Service.Dtos.Response.Doctors;
using Vezeeta.Service.Helpers;

namespace Vezeeta.Web.Controllers.Patient
{
    [Route("Api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Patient")]
    public class PatientController : ApplicationController
    {
        private readonly IDoctorService _doctorService;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public PatientController(IDoctorService doctorService, IMapper mapper, IBookingService bookingService)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            _bookingService = bookingService;
        }

        [HttpGet("Doctors/GetAll")]
        public async Task<ActionResult<IEnumerable<GetDoctorWithAppointmentsDto>>> GetAll(int page = 1, int pageSize = 10, string search = "")
        {
            IEnumerable<User> doctors = await _doctorService.GetAllDoctorsAsync(
                page,
                pageSize,
                search,
                new[]
                {
                    AppConsts.DomainModels.Specialization,
                    AppConsts.DomainModels.AppointmentsAndTimes
                });

            var response = _mapper.Map<IEnumerable<GetDoctorWithAppointmentsDto>>(doctors);

            return Ok(response);
        }

        [HttpPost("Bookings/Book")]
        public async Task<ActionResult<bool>> Booking(BookAppointmentDto request)
        {
            int patientId = GetUserId();

            Result<bool> bookResult = await _bookingService.BookAsync(request.TimeId, patientId, request.CouponCode);

            if (bookResult.IsFailure) return BadRequest(bookResult.Error);

            return Ok(true);
        }

        [HttpGet("Bookings/GetAll")]
        public async Task<ActionResult<bool>> GetAllBookings()
        {
            int patientId = GetUserId();

            var bookings = await _bookingService.GetAllBookingsAsync(patientId, UserDiscriminator.Patient, 1, int.MaxValue, null, new[]
            {
                AppConsts.DomainModels.BookingAppointment,
                AppConsts.DomainModels.Coupon,
                AppConsts.DomainModels.DoctorSpecialization
            });

            var response = BookingHelper.MaPatientBookingsResultToResponseDto(bookings);

            return Ok(response);
        }

        [HttpGet("Bookings/Cancel")]
        public async Task<ActionResult<bool>> CancelBooking(int bookingId)
        {
            int patientId = GetUserId();

            Result<bool> cancelBookingResult = await _bookingService.CancelBookingAsync(bookingId, patientId);
            if (cancelBookingResult.IsFailure) return BadRequest(cancelBookingResult.Error);

            return Ok(true);
        }
    }
}
