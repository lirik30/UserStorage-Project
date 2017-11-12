using System;
using UserStorageServices.Notifications;
using UserStorageServices.Repositories;
using UserStorageServices.Validators;

namespace UserStorageServices.Services
{
    [Serializable]
    public class UserStorageServiceSlave : UserStorageServiceBase
    {
        private INotificationReceiver _receiver;

        public UserStorageServiceSlave(
            INotificationReceiver receiver = null,
            IUserValidator validator = null,
            IUserRepository userRepository = null) : base(validator, userRepository)
        {
            _receiver = receiver ?? new NotificationReceiver();
            ((NotificationReceiver)_receiver).Subscribe(NotificationReceived);
        }

        public override StorageMode StorageMode => StorageMode.SlaveNode;

        public override void Add(User user)
        {
            throw new NotSupportedException();
        }

        public override void Remove(User user)
        {
            throw new NotSupportedException();
        }

        public void AddFromMaster(User user)
        {
            Repository.Add(user);
        }

        public void RemoveFromMaster(User user)
        {
            Repository.Remove(user);
        }

        public void NotificationReceived(NotificationContainer container)
        {
            foreach (var notification in container.Notifications)
            {
                if (notification.Type == NotificationType.AddUser)
                {
                    var action = notification.Action as AddUserActionNotification;
                    if (action != null)
                        AddFromMaster(action.User);
                }

                if (notification.Type == NotificationType.DeleteUser)
                {
                    var action = notification.Action as DeleteUserActionNotification;
                    if (action != null)
                        RemoveFromMaster(action.User);
                }
            }
        }
    }
}
