using Lykke.EmailProvider.Models;
using System.Threading.Tasks;

namespace Lykke.EmailProvider.Interfaces
{
    public interface IEmailReader
    {
        Task<SerializedMailMessage> ReadEmail(string key);
    }
}