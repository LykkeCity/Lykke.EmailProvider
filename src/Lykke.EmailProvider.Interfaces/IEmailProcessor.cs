using System.Threading.Tasks;
using Lykke.EmailProvider.Models;

namespace Lykke.EmailProvider.Interfaces
{
    public interface IEmailProcessor
    {
        Task<EmailMessage> ReadEmail(string emailId);
        Task WriteEmail(EmailMessage emailMessage);
    }
}