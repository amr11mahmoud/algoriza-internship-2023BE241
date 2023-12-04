using Microsoft.EntityFrameworkCore;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Repository;
using Vezeeta.Core.Shared;

namespace Vezeeta.Repository.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<RequestCount>> CountBookingRequestsAsync()
        {
            var requestsCountList = await _context.Bookings.GroupBy(b => b.Status)
                .Select(g => new RequestCount
                {
                    Status = g.Key.ToString(),
                    Count = g.Count()
                })
                .ToListAsync();

            var totalCount = new RequestCount { Count = requestsCountList.Sum(x=> x.Count), Status = AppConsts.General.Total };

            requestsCountList.Add(totalCount);

            return requestsCountList; 
        }
    }
}
