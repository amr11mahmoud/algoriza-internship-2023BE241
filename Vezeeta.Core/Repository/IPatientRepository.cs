using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Bookings;
using Vezeeta.Core.Domain.Users;

namespace Vezeeta.Core.Repository
{
    public interface IPatientRepository:IBaseRepository<User>
    {
        Task<IEnumerable<User>> FindAllPatientsAsync(int page, int pageSize, string search, string[]? includes = null);
        Task<(User?, List<Booking>)> FindPatientWithBookings(int id);
    }
}
