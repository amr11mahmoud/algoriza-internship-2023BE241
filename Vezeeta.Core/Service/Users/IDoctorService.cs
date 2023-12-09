using Vezeeta.Core.Domain.Lookup;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Shared;

namespace Vezeeta.Core.Service.Users
{
    public interface IDoctorService
    {
        Task<int> GetDoctorsCountAsync();
        Task<IEnumerable<User>> GetAllDoctorsAsync(int page, int pageSize, string search, string[]? includes = null);
        Task<User?> GetDoctorAsync(int id, string[]? includes = null);
        Task<Result<bool>> AddDoctorAsync(User user);
        Task<Result<bool>> UpdateDoctorAsync(User user);
        Task<Result<bool>> DeleteDoctorAsync(int id);
        Task<Result<bool>> UpdateDoctorPriceAsync(int doctorId, float price);
        Task<IEnumerable<SpecializationRequestCount>> GetTopSpecialization(int number);
        IEnumerable<DoctorRequestCount> GetTopDoctors(int number);
    }
}
