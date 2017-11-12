using System;
using System.IO;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    public class NotificationReceiver : MarshalByRefObject, INotificationReceiver
    {
        public event Action<NotificationContainer> Received = delegate { };

        public void Subscribe(Action<NotificationContainer> action)
        {
            Received += action;
        }

        public void Receive(string receiveString)
        {
            var container = Deserialize(receiveString);
            Received(container);
        }

        private NotificationContainer Deserialize(string receiveString)
        {
            byte[] bytes = Convert.FromBase64String(receiveString);

            using (var stream = new MemoryStream(bytes))
            {
                var formatter = new XmlSerializer(typeof(NotificationContainer));
                stream.Seek(0, SeekOrigin.Begin);
                return (NotificationContainer)formatter.Deserialize(stream);
            }
        }
    }
}
