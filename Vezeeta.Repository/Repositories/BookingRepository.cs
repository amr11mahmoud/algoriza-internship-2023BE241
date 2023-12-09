using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

        public IEnumerable<DoctorRequestCount> CountDoctorsRequests(int number)
        {
            var doctorsList = _context.Bookings
                .Include(AppConsts.DomainModels.DoctorSpecialization)
                .ToList()
                .GroupBy(b => b.Doctor)
                .Select(g => new DoctorRequestCount
                {
                    FullName = g.Key.FullName,
                    Requests = g.Count(),
                    Image = g.Key.ImageUrl,
                    Specialization = g.Key.Specialization.Name
                }).OrderByDescending(g => g.Requests)
                .Take(number);

            return doctorsList;
        }

        public async Task<List<SpecializationRequestCount>> CountSpecializationRequestsAsync(int number)
        {
            var specializationList = await _context.Bookings.GroupBy(b => b.Doctor.Specialization)
                .Select(g => new SpecializationRequestCount
                {
                    FullName = g.Key.Name,
                    Requests = g.Count()
                }).OrderByDescending(g => g.Requests)
                .Take(number)
                .ToListAsync();

            return specializationList;
        }
    }
}
