using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    [Serializable]
    public class NotificationSender : INotificationSender
    {
        private readonly INotificationReceiver _receiver;

        public NotificationSender(INotificationReceiver receiver = null)
        {
            _receiver = receiver ?? new NotificationReceiver();
        }

        public void Send(NotificationContainer container)
        {
            var receiveString = Serialize(container);
            _receiver.Receive(receiveString);
        }

        private string Serialize(NotificationContainer container)
        {
            var memoryStream = new MemoryStream();
            var formatter = new XmlSerializer(typeof(NotificationContainer));
            using (memoryStream)
            {
                formatter.Serialize(memoryStream, container);
            }

            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }
}
