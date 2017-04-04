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

namespace Lykke.EmailProvider.Providers
{
    internal class EmailProviderPublisher : IEmailProviderPublisher
    {
        private readonly AzureQueueAndBlobPublisher<SerializedMailMessage> _queueAndBlobPublisher;

        public EmailProviderPublisher(string applicationName,
            AzureQueueAndBlobIntegrationSettings settings,
            IAzureQueueAndBlobSerializer<SerializedMailMessage> serializer, ILog logger)
        {
            _queueAndBlobPublisher = new AzureQueueAndBlobPublisher<SerializedMailMessage>(applicationName, settings);
            _queueAndBlobPublisher.SetSerializer(serializer)
                                  .SetLogger(logger)
                                  .Start();
        }

        public async Task WriteEmail(SerializedMailMessage serializedMessage)
        {
            await _queueAndBlobPublisher.ProduceAsync(serializedMessage);
        }
    }
}
