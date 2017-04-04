using Common.Log;
using Lykke.EmailProvider.Models;
using Lykke.EmailProvider.Providers;
using Lykke.Integration.AzureQueueAndBlobs;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lykke.EmailProvider.Test
{
    [TestFixture]
    public class EmailProviderTest
    {
        private readonly string _applicationName;
        private AzureQueueAndBlobIntegrationSettings _settings;

        public EmailProviderTest()
        {
            _applicationName = "Lykke.EmailModel.Test";
            _settings = new AzureQueueAndBlobIntegrationSettings()
            {
                BlobContainer = "test-email-container",
                ConnectionString = "UseDevelopmentStorage=true",
                QueueName = "test-queue",
            };

        }

        [Test]
        public void EmailProvider_SendsMessage_Moq()
        {
            EmailMessage emailMessage = new EmailMessage()
            {
                Body = "Test Email"
            };
            SerializedMailMessage serializedeMailMessage = new SerializedMailMessage(emailMessage);
            Mock<IEmailProviderPublisher> emailProviderPublisherMock = new Mock<IEmailProviderPublisher>();
            emailProviderPublisherMock.Setup(x => x.WriteEmail(It.IsAny<SerializedMailMessage>()))
                .Returns(Task.FromResult(0)).Verifiable();

            EmailProcessor emailProvider = new EmailProcessor(emailProviderPublisherMock.Object, null);

            emailProvider.WriteEmail(emailMessage).Wait();
            //ASSERT
            emailProviderPublisherMock.Verify(x => x.WriteEmail(It.IsAny<SerializedMailMessage>()));

        }

        [Test]
        public void EmailProvider_SendsMessage_TestQueue()
        {
            EmailMessage emailMessage = new EmailMessage()
            {
                Body = "Test Email",
                Subject = "test"
            };
            emailMessage.To.Add("test@test.com");

            EmailProcessor emailProvider = new EmailProcessor(_applicationName,
                _settings, new FakeLogger());

            emailProvider.WriteEmail(emailMessage).Wait();
            Task.Delay(10000).Wait();
        }
    }

    #region Fake
    internal class FakeLogger : ILog
    {
        public Task WriteErrorAsync(string component, string process, string context, Exception exeption, DateTime? dateTime = default(DateTime?))
        {
            return Task.FromResult(0);
        }

        public Task WriteFatalErrorAsync(string component, string process, string context, Exception exeption, DateTime? dateTime = default(DateTime?))
        {
            return Task.FromResult(0);
        }

        public Task WriteInfoAsync(string component, string process, string context, string info, DateTime? dateTime = default(DateTime?))
        {
            return Task.FromResult(0);
        }

        public Task WriteWarningAsync(string component, string process, string context, string info, DateTime? dateTime = default(DateTime?))
        {
            return Task.FromResult(0);
        }
    }
    #endregion
}
