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
    public class EmailProcessor : IEmailProcessor
    {
        private readonly IEmailProviderPublisher _emailProviderPublisher;
        private readonly IEmailReader _emailReader;

        public EmailProcessor(IEmailProviderPublisher emailProvider, IEmailReader emailReader)
        {
            _emailProviderPublisher = emailProvider;
            _emailReader = emailReader;
        }

        public EmailProcessor(string applicationName, AzureQueueAndBlobIntegrationSettings settings, ILog logger)
        {
            IAzureQueueAndBlobSerializer<SerializedMailMessage> serializer = new MailMessageSerializer();
            _emailProviderPublisher = new EmailProviderPublisher(applicationName, settings, serializer, logger);
            _emailReader = new EmailReader(settings);
        }

        public async Task WriteEmail(EmailMessage emailMessage)
        {
            SerializedMailMessage serializedMailMessage = new SerializedMailMessage(emailMessage);
            await _emailProviderPublisher.WriteEmail(serializedMailMessage);
        }

        public async Task<EmailMessage> ReadEmail(string emailId)
        {
            SerializedMailMessage serializedMailMessage = await _emailReader.ReadEmail(emailId);
            return serializedMailMessage.EmailMessage;
        }
    }
}
