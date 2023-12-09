using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Shared;

namespace Vezeeta.Core.Service.Bookings
{
    public interface IBookingService
    {
        Task<List<int>> GetBookingsCountAsync();
        Task<int> GetBookingsCountAsync(int patientId);
        Task<Result<bool>> BookAsync(int timeId, int patientId, string? couponCode = null);
        Task<Result<bool>> ConfirmCheckUp(int bookingId, int doctorId);
        Task<Result<bool>> CancelBookingAsync(int bookingId, int patientId);
        Task<IEnumerable<Booking>> GetAllBookingsAsync(int discriminatorId,
            UserDiscriminator discriminator,
            int page,
            int pageSize,
            string? day = null,
            string[]? includes = null);
    }
}
