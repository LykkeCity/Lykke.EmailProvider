using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureStorage;
using AzureStorage.Blob;
using Newtonsoft.Json;
using Lykke.Integration.AzureQueueAndBlobs.Publisher;
using Lykke.Integration.AzureQueueAndBlobs;
using Common.Log;
using Lykke.EmailProvider.Models;
using Lykke.EmailProvider.Interfaces;

namespace Lykke.EmailProvider.Providers
{
    internal class EmailReader : IEmailReader
    {
        private readonly AzureBlobStorage _blobStorage;
        private readonly AzureQueueAndBlobIntegrationSettings _settings;

        public EmailReader(AzureQueueAndBlobIntegrationSettings settings)
        {
            _settings = settings;
            _blobStorage = new AzureBlobStorage(_settings.ConnectionString);
        }

        public async Task<SerializedMailMessage> ReadEmail(string key)
        {
            using (var blobStream = await _blobStorage.GetAsync(_settings.BlobContainer, key))
            {
                byte[] buffer = new byte[blobStream.Length];
                await blobStream.ReadAsync(buffer, 0, (int)blobStream.Length);
                string jsonMessage = System.Text.Encoding.UTF8.GetString(buffer);
                return JsonConvert.DeserializeObject<SerializedMailMessage>(jsonMessage);
            }
        }
    }
}
