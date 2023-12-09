using Vezeeta.Core.Domain.Appointments;
using Vezeeta.Core.Shared;

namespace Vezeeta.Core.Service.Appointments
{
    public interface IAppointmentService
    {
        Task<Result<bool>> AddAppointmentsAsync(int doctorId, IEnumerable<Appointment> appointments, float price);
        Task<Appointment?> GetAppointmentAsync(int appointmentId, string[]? includes = null);
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(int doctorId);
        Task<Result<bool>> DeleteAppointmentTimeAsync(int timeId, int doctorId);
        Task<Result<bool>> UpdateAppointmentTimeAsync(int timeId, int doctorId, string newTime);
        Task<AppointmentTime?> GetTimeAsync(int timeId, string[]? includes = null);
        bool IsTimeBooked(AppointmentTime time);
    }
}
