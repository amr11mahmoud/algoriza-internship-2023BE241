using System.Globalization;
using Vezeeta.Core.Domain.Appointments;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Shared;
using Vezeeta.Service.Dtos.Request.Appointments;
using Vezeeta.Service.Dtos.Request.Doctors;
using Vezeeta.Service.Dtos.Response.Appointments;

namespace Vezeeta.Service.Helpers
{
    public static class DateTimeHelper
    {
        public static bool CanConvertStringToTime(string time)
        {
            string format = "HH:mm";
            CultureInfo invariant = CultureInfo.InvariantCulture;
            DateTime dateTime;

            return DateTime.TryParseExact(time, format, invariant, DateTimeStyles.None, out dateTime);
        }

        public static bool CanConvertStringToDay(string dayAsString)
        {
            Days day;

            return Enum.TryParse(dayAsString, out day);
        }

        public static Result<bool> ValidateDayAndTimeForAppointments(IEnumerable<AddAppointmentDto> appointments)
        {
            foreach (var appointment in appointments)
            {
                if (!CanConvertStringToDay(appointment.Day)) 
                    return Result.Failure<bool>(Error.Errors.Appointments.InvalidDayFormat(appointment.Day));

                foreach (var time in appointment.Times)
                {
                    if (!CanConvertStringToTime(time.Time)) 
                        return Result.Failure<bool>(Error.Errors.Appointments.InvalidTimeFormat(time.Time));
                }
            }

            return Result.Success(true);
        }
    }
}
