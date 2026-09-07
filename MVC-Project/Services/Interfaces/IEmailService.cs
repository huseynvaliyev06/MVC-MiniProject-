using System.Threading.Tasks;

namespace MVC_MiniProject.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string toEmail, string subject, string htmlBody);
    }
}