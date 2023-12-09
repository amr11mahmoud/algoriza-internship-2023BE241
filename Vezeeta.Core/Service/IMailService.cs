using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Shared;

namespace Vezeeta.Core.Service
{
    public interface IMailService
    {
        Task<bool> SendAsync(string subject, string message, User user);
    }
}
