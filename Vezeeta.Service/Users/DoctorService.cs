using System.Collections.Generic;
using System.Linq.Expressions;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Domain.Lookup;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Repository;
using Vezeeta.Core.Service;
using Vezeeta.Core.Service.Users;
using Vezeeta.Core.Shared;

namespace Vezeeta.Service.Users
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public DoctorService(
            IDoctorRepository doctorRepository, 
            IUserService userService,
            IMailService mailService,
            IBookingRepository bookingRepository)
        {
            _doctorRepository = doctorRepository;
            _userService = userService;
            _bookingRepository = bookingRepository;
            _mailService = mailService;
        }

        public async Task<IEnumerable<User>> GetAllDoctorsAsync(int page, int pageSize, string search, string[]? includes = null)
        {
            Expression<Func<User, bool>> searchCondition = (u) => u.Discriminator == UserDiscriminator.Doctor;

            if (!string.IsNullOrEmpty(search)) searchCondition = (u) => u.Discriminator == UserDiscriminator.Doctor && u.FullName.Contains(search);

            IEnumerable<User> doctors = await _doctorRepository.FindAllAsync(searchCondition, page, pageSize, includes);

            return doctors;
        }

        public async Task<User?> GetDoctorAsync(int id, string[]? includes = null)
        {
            User? doctor = await _doctorRepository.FindAsync(d => d.Id == id && d.Discriminator == UserDiscriminator.Doctor, includes);

            return doctor;
        }

        public async Task<int> GetDoctorsCountAsync()
        {
            var doctorsCount = await _doctorRepository.CountAsync(u => u.Discriminator == UserDiscriminator.Doctor);

            return doctorsCount;
        }

        public async Task<Result<bool>> AddDoctorAsync(User user)
        {

            Result<User> registerDoctorResult =
                await _userService.RegisterUserAsync(user, AppConsts.User.DoctorBasePassword, UserDiscriminator.Doctor, new[] { AppConsts.Roles.Doctor });

            if (registerDoctorResult.IsFailure) return Result.Failure<bool>(registerDoctorResult.Error);

            await _mailService.SendAsync("Your Doctor Account Credentials", $"Email: {user.Email} Password: {AppConsts.User.DoctorBasePassword}", user);

            return Result.Success(true);
        }

        public async Task<Result<bool>> UpdateDoctorAsync(User user)
        {
            var updateDoctorResult = await _userService.UpdateUserAsync(user);

            if (updateDoctorResult.IsFailure) return Result.Failure<bool>(updateDoctorResult.Error);

            return Result.Success(true);
        }

        public async Task<Result<bool>> DeleteDoctorAsync(int id)
        {
            bool doctorHasRequests = await _bookingRepository.AnyAsync(b => b.DoctorId == id);

            if (doctorHasRequests)
            {
                return Result.Failure<bool>(Error.Errors.Doctors.DoctorHasRequests());
            }

            var doctor = await GetDoctorAsync(id);

            if (doctor == null)
            {
                return Result.Failure<bool>(Error.Errors.Doctors.DoctorNotFound());
            }

            var deleteDoctorResult = await _userService.DeleteUserAsync(id);

            if (deleteDoctorResult.IsFailure) return Result.Failure<bool>(deleteDoctorResult.Error);

            return Result.Success(true);
        }

        public async Task<Result<bool>> UpdateDoctorPriceAsync(int doctorId, float price)
        {
            User? doctor = await GetDoctorAsync(doctorId);
            if (doctor == null) return Result.Failure<bool>(Error.Errors.Doctors.DoctorNotFound());

            doctor.AddDoctorPrice(price);

            Result<bool> updateDoctorResult = await UpdateDoctorAsync(doctor);
            if (updateDoctorResult.IsFailure) return Result.Failure<bool>(updateDoctorResult.Error);

            return Result.Success(true);
        }

        public async Task<IEnumerable<SpecializationRequestCount>> GetTopSpecialization(int number)
        {
            IEnumerable<SpecializationRequestCount> specializationRequestCounts = await _bookingRepository.CountSpecializationRequestsAsync(number);

            return specializationRequestCounts;
        }

        public IEnumerable<DoctorRequestCount> GetTopDoctors(int number)
        {
            IEnumerable<DoctorRequestCount> doctorRequestCounts = _bookingRepository.CountDoctorsRequests(number);

            return doctorRequestCounts;
        }
    }
}
