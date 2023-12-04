using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Repository
{
    public interface IDoctorRepository: IBaseRepository<User>
    {
        Task<IEnumerable<User>> FindAllDoctorsAsync(int page, int pageSize, string search, string[]? includes = null);
    }
}
