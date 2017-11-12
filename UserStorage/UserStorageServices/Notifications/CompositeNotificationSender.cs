using System.Collections.Generic;

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
