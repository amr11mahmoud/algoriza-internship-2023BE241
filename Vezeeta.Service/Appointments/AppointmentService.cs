using Vezeeta.Core.Consts;
using Vezeeta.Core.Domain.Appointments;
using Vezeeta.Core.Repository;
using Vezeeta.Core.Service.Appointments;
using Vezeeta.Core.Service.Users;
using Vezeeta.Core.Shared;

namespace Vezeeta.Service.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IBaseRepository<Appointment> _appointmentRepository;
        private readonly IBaseRepository<AppointmentTime> _appointmentTimeRepository;
        private readonly IDoctorService _doctorService;
        public AppointmentService(
            IBaseRepository<Appointment> appointmentRepository,
            IBaseRepository<AppointmentTime> appointmentTimeRepository,
            IDoctorService doctorService)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentTimeRepository = appointmentTimeRepository;
            _doctorService = doctorService;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(int doctorId)
        {
            IEnumerable<Appointment> appointments = await _appointmentRepository
                .FindAllAsync(
                appointment => appointment.DoctorId == doctorId,
                1,
                int.MaxValue,
                new[] { AppConsts.DomainModels.AppointmentTimes });

            return appointments;
        }

        public async Task<Appointment?> GetAppointmentAsync(int appointmentId, int doctorId, string[]? includes = null)
        {
            Appointment? appointment = 
                await _appointmentRepository.FindAsync(appointment => appointment.Id == appointmentId && appointment.DoctorId == doctorId, includes);
            return appointment;
        }

        public async Task<Result<bool>> DeleteAppointmentTimeAsync(int timeId, int doctorId)
        {
            AppointmentTime? time = await _appointmentTimeRepository.FindAsync(time => time.Id == timeId, new[] { AppConsts.DomainModels.Appointment });

            Result<bool> validationResult = ValidateAppointmentTime(time, doctorId);
            if (validationResult.IsFailure) return validationResult;

            await _appointmentTimeRepository.DeleteAsync(timeId);
            await _appointmentRepository.SaveChangesAsync();

            return Result.Success(true);
        }

        public async Task<Result<bool>> UpdateAppointmentTimeAsync(int timeId, int doctorId, string newTime)
        {
            AppointmentTime? time = await _appointmentTimeRepository.FindAsync(time => time.Id == timeId, new[] { AppConsts.DomainModels.Appointment });

            Result<bool> validationResult = ValidateAppointmentTime(time, doctorId);
            if (validationResult.IsFailure) return validationResult;

            time.UpdateTime(newTime);

            _appointmentTimeRepository.Update(time);
            await _appointmentRepository.SaveChangesAsync();

            return Result.Success(true);
        }

        public async Task<Result<bool>> AddAppointmentsAsync(int doctorId, IEnumerable<Appointment> appointments, float price)
        {
            Result<bool> addDoctorPriceResult = await _doctorService.UpdateDoctorPriceAsync(doctorId, price);

            if (addDoctorPriceResult.IsFailure) Result.Failure<bool>(addDoctorPriceResult.Error);

            foreach (var appointment in appointments)
            {
                Appointment? appointmentExist = await _appointmentRepository
                    .FindAsync(a => a.Day == appointment.Day && a.DoctorId == doctorId, new[] { AppConsts.DomainModels.AppointmentTimes });

                if (appointmentExist != null) return Result.Failure<bool>(Error.Errors.Appointments.AppointmentDayAlreadyExist(appointment.Day.ToString()));

                appointment.DoctorId = doctorId;
            }

            await _appointmentRepository.AddRangeAsync(appointments);
            await _appointmentRepository.SaveChangesAsync();

            return Result.Success(true);
        }

        private Result<bool> ValidateAppointmentTime(AppointmentTime? time, int doctorId)
        {
            if (time == null) return Result.Failure<bool>(Error.Errors.Appointments.AppointmentTimeNotFound());
            if (time.Appointment.DoctorId != doctorId) return Result.Failure<bool>(Error.Errors.Appointments.InvalidAppointmentDoctor());
            if (time.Booked) return Result.Failure<bool>(Error.Errors.Appointments.AppointmentIsBooked());

            return Result.Success(true);
        }

        public async Task<AppointmentTime?> GetTimeAsync(int timeId, string[]? includes = null)
        {
            AppointmentTime? time = await _appointmentTimeRepository.FindAsync(time => time.Id == timeId, includes);

            return time;
        }

        public bool IsTimeBooked(AppointmentTime time)
        {
            return time.Booked;
        }
    }
}
