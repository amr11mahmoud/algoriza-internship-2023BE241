using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Domain.Appointments;
using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Service.Appointments;
using Vezeeta.Core.Service.Bookings;
using Vezeeta.Core.Shared;
using Vezeeta.Service.Dtos.Request.Appointments;
using Vezeeta.Service.Dtos.Request.Doctors;
using Vezeeta.Service.Dtos.Response.Appointments;
using Vezeeta.Service.Dtos.Response.Doctors;
using Vezeeta.Service.Helpers;
using static Vezeeta.Core.Consts.AppConsts;

namespace Vezeeta.Web.Controllers.Doctor
{
    [Route("Api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Doctor")]
    public class DoctorsController : ApplicationController
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;
        private readonly IBookingService _bookingService;

        public DoctorsController(IMapper mapper, IAppointmentService appointmentService, IBookingService bookingService)
        {
            _appointmentService = appointmentService;
            _bookingService = bookingService;
            _mapper = mapper;
        }

        [HttpGet("Bookings/GetAll")]
        public async Task<ActionResult<IEnumerable<GetDoctorBookingDto>>> GetAllBookings(int page = 1, int pageSize = 10, string? day = null)
        {
            int doctorId = GetUserId();

            if (!string.IsNullOrEmpty(day))
            {
                bool isValidDayFormat = DateTimeHelper.CanConvertStringToDay(day);
                if (!isValidDayFormat) return BadRequest(Error.Errors.Appointments.InvalidDayFormat(day));
            }

            IEnumerable<Booking> bookings = await _bookingService.GetAllBookingsAsync(
                doctorId,
                UserDiscriminator.Doctor,
                page,
                pageSize,
                day,
                new[] { DomainModels.BookingAppointment, DomainModels.Patient });

            var response = BookingHelper.MapDoctorBookingsResultToResponseDto(bookings);

            return Ok(response);
        }

        [HttpGet("Bookings/ConfirmCheckUp")]
        public async Task<ActionResult<bool>> ConfirmBookingCheckUp(int bookingId)
        {
            int doctorId = GetUserId();

            Result<bool> confirmCheckUpResult = await _bookingService.ConfirmCheckUp(bookingId, doctorId);

            if (confirmCheckUpResult.IsFailure) return BadRequest(confirmCheckUpResult.Error);

            return Ok(true);
        }

        [HttpGet("Appointments/Get")]
        public async Task<ActionResult<GetAppointmentDto>> GetAppointment(int id)
        {
            int doctorId = GetUserId();

            Appointment? appointment = await _appointmentService.GetAppointmentAsync(id, doctorId, new[] { DomainModels.AppointmentTimes });

            if (appointment == null) return BadRequest(Error.Errors.Appointments.AppointmentNotFound());

            var response = _mapper.Map<GetAppointmentDto>(appointment);

            return Ok(response);
        }

        [HttpGet("Appointments/GetAll")]
        public async Task<ActionResult<GetAppointmentDto>> GetAllAppointments()
        {
            int doctorId = GetUserId();

            IEnumerable<Appointment> appointments = await _appointmentService.GetAllAppointmentsAsync(doctorId);

            var response = _mapper.Map<IEnumerable<GetAppointmentDto>>(appointments);

            return Ok(response);
        }

        [HttpPost("Appointments/Add")]
        public async Task<ActionResult<bool>> AddAppointments(AddDoctorAppointmentsDto request)
        {
            int doctorId = GetUserId();

            Result<bool> validateDayAndTimeFormatResult = DateTimeHelper.ValidateDayAndTimeForAppointments(request.Appointments);

            if (validateDayAndTimeFormatResult.IsFailure) return BadRequest(validateDayAndTimeFormatResult.Error);

            IEnumerable<Appointment> appointments = _mapper.Map<IEnumerable<Appointment>>(request.Appointments);

            Result<bool> addAppointmentsResult = await _appointmentService.AddAppointmentsAsync(doctorId, appointments, request.Price);

            if (addAppointmentsResult.IsFailure) return BadRequest(addAppointmentsResult.Error);

            return Ok(true);
        }

        [HttpPut("Appointments/Update")]
        public async Task<ActionResult<bool>> UpdateAppointment(UpdateAppointmentTimeDto request)
        {
            int doctorId = GetUserId();

            bool isValidTimeFormat = DateTimeHelper.CanConvertStringToTime(request.Time);
            if (!isValidTimeFormat) return BadRequest(Error.Errors.Appointments.InvalidTimeFormat(request.Time));

            var updateAppointmentTimeResult = await _appointmentService.UpdateAppointmentTimeAsync(request.Id, doctorId, request.Time);
            if (updateAppointmentTimeResult.IsFailure) return BadRequest(updateAppointmentTimeResult.Error);

            return Ok(true);
        }

        [HttpDelete("Appointments/Delete")]
        public async Task<ActionResult<bool>> DeleteAppointment(int appointmentTimeId)
        {
            int doctorId = GetUserId();

            var deleteAppointmentTimeResult = await _appointmentService.DeleteAppointmentTimeAsync(appointmentTimeId, doctorId);
            if (deleteAppointmentTimeResult.IsFailure) return BadRequest(deleteAppointmentTimeResult.Error);

            return Ok(true);
        }
    }
}
