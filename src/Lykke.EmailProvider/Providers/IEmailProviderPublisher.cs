using System.Threading.Tasks;
using Lykke.EmailProvider.Models;

namespace Lykke.EmailProvider.Providers
{
    public interface IEmailProviderPublisher
    {
        Task WriteEmail(SerializedMailMessage serializedMessage);
    }
}