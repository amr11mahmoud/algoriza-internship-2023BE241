using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Shared;

namespace Vezeeta.Core.Service.Users
{
    public interface IDoctorService
    {
        Task<Result<int>> GetDoctorsCountAsync();
        Task<Result<IEnumerable<User>>> GetAllDoctorsAsync(int page, int pageSize, string search, string[]? includes = null);
        Task<Result<User>> GetDoctorAsync(int id, string[]? includes = null);
        Task<Result<bool>> AddDoctorAsync(User user);
        Task<Result<bool>> UpdateDoctorAsync(User user);
        Task<Result<bool>> DeleteDoctorAsync(int id);
        Task<Result<IEnumerable<Booking>>> GetDoctorBookings(int doctorId, int page, int pageSize, DateTime? date, string[]? includes = null);
    }
}
