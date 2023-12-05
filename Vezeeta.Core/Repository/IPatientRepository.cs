using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Domain.Users;

namespace Vezeeta.Core.Repository
{
    public interface IPatientRepository : IBaseRepository<User>
    {
        Task<(User?, List<Booking>)> FindPatientWithBookings(int id);
    }
}
