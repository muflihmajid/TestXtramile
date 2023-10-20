using System.Threading.Tasks;
using SceletonAPI.Application.Models.Notifications;

namespace SceletonAPI.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailMessage message);
    }
}
