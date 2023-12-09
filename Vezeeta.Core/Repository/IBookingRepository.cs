using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Shared;

namespace Vezeeta.Core.Repository
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<List<RequestCount>> CountBookingRequestsAsync();
        IEnumerable<DoctorRequestCount> CountDoctorsRequests(int number);
        Task<List<SpecializationRequestCount>> CountSpecializationRequestsAsync(int number);
    }
}
