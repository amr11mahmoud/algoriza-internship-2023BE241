using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Service.Users;

namespace Vezeeta.Web.Controllers.Doctor
{
    [Route("api/[controller]")]
    public class DoctorsController:ApplicationController
    {
        // get all bookings
        // confirm checkup
        // add/update/delete appointments
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("bookings/getAll")]
        public async Task<ActionResult> GetAll(DateTime? date, int page = 1, int pageSize = 10)
        {
            int doctorId = 3;

            var getBookingsResult = 
                await _doctorService.GetDoctorBookings(doctorId, page, pageSize, date, new[] { AppConsts.DomainModels.BookingAppointment } );
            if (getBookingsResult.IsFailure) return BadRequest(getBookingsResult.Error);

            return Ok(getBookingsResult.Value);
        }

    }
}
