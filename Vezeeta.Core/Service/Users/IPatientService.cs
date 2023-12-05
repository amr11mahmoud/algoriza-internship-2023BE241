using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Shared;

namespace Vezeeta.Core.Service.Users
{
    public interface IPatientService
    {
        Task<Result<IEnumerable<User>>> GetAllPatientsAsync(int page, int pageSize, string search, string[]? includes = null);
        Task<Result<(User? patient, List<Booking> bookings)>> GetPatientWithBookingsAsync(int id);
        Task<int> GetPatientsCountAsync();
    }
}
