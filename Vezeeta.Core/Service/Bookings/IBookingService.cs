using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Shared;

namespace Vezeeta.Core.Service.Bookings
{
    public interface IBookingService
    {
        Task<List<int>> GetBookingsCountAsync();
    }
}
