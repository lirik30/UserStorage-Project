using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Notifications
{
    public class CompositeNotificationSender : INotificationSender
    {
        private readonly IEnumerable<INotificationSender> _senders;

        public CompositeNotificationSender(IEnumerable<INotificationSender> senders)
        {
            _senders = senders ?? new List<INotificationSender>();
        }

        public void Send(NotificationContainer container)
        {
            foreach (var sender in _senders)
            {
                sender.Send(container);
            }
        }
    }
}
