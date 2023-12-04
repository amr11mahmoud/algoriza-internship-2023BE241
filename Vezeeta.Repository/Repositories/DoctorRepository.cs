using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Enums;
using Vezeeta.Core.Repository;

namespace Vezeeta.Repository.Repositories
{
    public class DoctorRepository : BaseRepository<User>, IDoctorRepository
    {
        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> FindAllDoctorsAsync(int page, int pageSize, string search, string[]? includes = null)
        {
            Expression<Func<User, bool>> exp = (u) => u.Discriminator == UserDiscriminator.Doctor;

            if (!string.IsNullOrEmpty(search)) exp = (u) => u.Discriminator == UserDiscriminator.Doctor && u.FullName.Contains(search);

            IEnumerable<User> doctors = await FindAllAsync(exp, page, pageSize, includes);

            return doctors;
        }
    }
}
