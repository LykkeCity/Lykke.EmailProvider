using System;
using System.Runtime.Serialization;

namespace Lykke.EmailProvider.Models
{
    [Serializable]
    [DataContract]
    public class SerializedMailMessage
    {
        [DataMember(Name = "mailMessage")]
        public EmailMessage EmailMessage { get; set; }

        public SerializedMailMessage(EmailMessage emailMessage)
        {
            EmailMessage = emailMessage;
        }
    }
}
