using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Shared;

namespace Vezeeta.Core.Repository
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<List<RequestCount>> CountBookingRequestsAsync();
    }
}
