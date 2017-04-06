using System.Threading.Tasks;
using Lykke.EmailProvider.Models;

namespace Lykke.EmailProvider.Interfaces
{
    public interface IEmailProviderPublisher
    {
        Task WriteEmail(SerializedMailMessage serializedMessage);
    }
}