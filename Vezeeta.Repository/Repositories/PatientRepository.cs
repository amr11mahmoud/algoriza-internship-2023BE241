using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Repository;

namespace Vezeeta.Repository.Repositories
{
    public class PatientRepository : BaseRepository<User>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(User?, List<Booking>)> FindPatientWithBookings(int id)
        {
            List<Booking> bookings = new List<Booking>();
            User? patient = await FindAsync(d => d.Id == id && d.Discriminator == UserDiscriminator.Patient);

            if (patient != null)
            {
                bookings = await _context.Bookings
                    .Where(b => b.PatientId == id)
                    .Include(b => b.Doctor)
                    .ThenInclude(b => b.Specialization)
                    .Include(b => b.Coupon)
                    .Include(b => b.Time)
                    .ThenInclude(b => b.Appointment)
                    .ToListAsync();
            }

            return (patient, bookings);
        }
    }
}
