using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Notifications
{
    public class NotificationReceiver : INotificationReceiver
    {
        public event Action<NotificationContainer> received = delegate{ }; 

        public void Receive(NotificationContainer container)
        {
            received(container);
        }
    }
}
