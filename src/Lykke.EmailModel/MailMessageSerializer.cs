using Common;
using Lykke.EmailProvider.Models;
using Lykke.Integration.AzureQueueAndBlobs.Publisher;
using Lykke.Integration.AzureQueueAndBlobs.Subscriber;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.EmailProvider
{
    public class MailMessageSerializer : IAzureQueueAndBlobSerializer<SerializedMailMessage>,
        IAzureQueueAndBlobDeserializer<SerializedMailMessage>
    {
        public MailMessageSerializer()
        {
        }

        public SerializedMailMessage Deserialize(byte[] data)
        {
            string json = Encoding.UTF8.GetString(data);
            SerializedMailMessage contract = json.DeserializeJson<SerializedMailMessage>();

            return contract;
        }

        public byte[] Serialize(SerializedMailMessage model)
        {
            string json = JsonConvert.SerializeObject(model);
            byte[] blob = json.ToUtf8Bytes();
            return blob;
        }
    }
}
