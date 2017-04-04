using System.Collections.Generic;

namespace Lykke.EmailProvider.Models
{
    public class EmailMessage
    {
        public string Subject { get; set; }
        public bool IsHtml { get; set; }
        public string Body { get; set; }
        public List<string> To { get; set; } = new List<string>();
        public List<string> Cc { get; set; } = new List<string>();
        public List<string> Bcc { get; set; } = new List<string>();
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

        public EmailMessage()
        {
        }
    }

    public class Attachment
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public string Base64Value { get; set; }
    }
}
