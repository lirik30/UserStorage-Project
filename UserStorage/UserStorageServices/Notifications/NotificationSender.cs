using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Notifications
{
    public class NotificationSender : INotificationSender
    {
        private readonly INotificationReceiver _receiver;

        public NotificationSender(INotificationReceiver receiver = null)
        {
            _receiver = receiver ?? new NotificationReceiver(); //add default value
        }

        public void Send(NotificationContainer container)
        {
            _receiver.Receive(container);
        }
    }
}
