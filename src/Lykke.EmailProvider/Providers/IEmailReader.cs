using Lykke.EmailProvider.Models;
using System.Threading.Tasks;

namespace Lykke.EmailProvider.Providers
{
    public interface IEmailReader
    {
        Task<SerializedMailMessage> ReadEmail(string key);
    }
}