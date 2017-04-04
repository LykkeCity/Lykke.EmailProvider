using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using Lykke.EmailProvider.Models;

namespace Lykke.EmailProvider.Test
{
    [TestFixture]
    public class SerializationTest
    {
        public SerializationTest()
        {
        }

        [Test]
        public void SuccesfulSerializationTest()
        {
            MailMessageSerializer serializator = new MailMessageSerializer();
            EmailMessage message = new EmailMessage();

            message.To.Add("joe@familyguy.com");
            message.Cc.Add("peter@familyguy.com");
            message.Bcc.Add("quagmire@familyguy.com");
            message.Body = "Test Text Here";
            message.Subject = "Test";
            message.IsHtml = true;

            byte[] serialized = serializator.Serialize(new SerializedMailMessage(message));
            SerializedMailMessage deserializedMessage = serializator.Deserialize(serialized);

            Assert.AreEqual(message.Body, deserializedMessage.EmailMessage.Body);
            Assert.AreEqual(message.Subject, deserializedMessage.EmailMessage.Subject);
            Assert.AreEqual(message.IsHtml, deserializedMessage.EmailMessage.IsHtml);
            Assert.AreEqual(message.To.First(), deserializedMessage.EmailMessage.To.First());
            Assert.AreEqual(message.Cc.First(), deserializedMessage.EmailMessage.Cc.First());
            Assert.AreEqual(message.Bcc.First(), deserializedMessage.EmailMessage.Bcc.First());
        }
    }
}
