using System.Linq.Expressions;
using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Repository;
using Vezeeta.Core.Service.Users;
using Vezeeta.Core.Shared;

namespace Vezeeta.Service.Users
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Result<IEnumerable<User>>> GetAllPatientsAsync(int page, int pageSize, string search, string[]? includes = null)
        {
            Expression<Func<User, bool>> searchCondition = (u) => u.Discriminator == UserDiscriminator.Patient;

            if (!string.IsNullOrEmpty(search)) searchCondition = (u) => u.Discriminator == UserDiscriminator.Patient && u.FullName.Contains(search);

            IEnumerable<User> patients = await _patientRepository.FindAllAsync(searchCondition, page, pageSize, includes);

            return Result.Success(patients);
        }

        public async Task<Result<(User? patient, List<Booking> bookings)>> GetPatientWithBookingsAsync(int id)
        {
            (User? patient, List<Booking> bookings) = await _patientRepository.FindPatientWithBookings(id);

            if (patient == null)
            {
                return Result.Failure<(User? patient, List<Booking> bookings)>(Error.Errors.Patients.PatientNotFound());
            }

            return Result.Success<(User? patient, List<Booking> bookings)>((patient, bookings));
        }

        public async Task<int> GetPatientsCountAsync()
        {
            int numberOfPatients = await _patientRepository.CountAsync(u => u.Discriminator == UserDiscriminator.Patient);

            return numberOfPatients;
        }
    }
}
