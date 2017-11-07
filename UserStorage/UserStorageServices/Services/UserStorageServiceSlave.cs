using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UserStorageServices.Notifications;
using UserStorageServices.Repositories;
using UserStorageServices.Validators;

namespace UserStorageServices.Services
{
    public class UserStorageServiceSlave : UserStorageServiceBase
    {
        private INotificationReceiver _receiver;

        public UserStorageServiceSlave(
            INotificationReceiver receiver = null,
            IUserValidator validator = null,
            IUserRepository userRepository = null) : base(validator, userRepository)
        {
            _receiver = receiver ?? new NotificationReceiver();
            ((NotificationReceiver)_receiver).Received += NotificationReceived;
        }

        public override StorageMode StorageMode => StorageMode.SlaveNode;

        public override void Add(User user)
        {
            if (IsCallFromMaster())
            {
                Repository.Add(user);
            }

            else
                throw new NotSupportedException();
        }

        public override void Remove(User user)
        {
            if (IsCallFromMaster())
                Repository.Remove(user);

            else
                throw new NotSupportedException();
        }

        public void NotificationReceived(NotificationContainer container)
        {
            foreach (var notification in container.Notifications)
            {
                if (notification.Type == NotificationType.AddUser)
                {
                    var action = notification.Action as AddUserActionNotification;
                    if (action != null)
                        Add(action.User);
                }

                if (notification.Type == NotificationType.DeleteUser)
                {
                    var action = notification.Action as DeleteUserActionNotification;
                    if (action != null)
                        Remove(action.User);
                }
            }
        }

        private bool IsCallFromMaster()
        {
            var stackTrace = new StackTrace();
            var addMethod = typeof(UserStorageServiceMaster).GetMethod("Add");
            var onUserAddedMethod = typeof(UserStorageServiceMaster).GetMethod("OnUserAdded", BindingFlags.Instance | BindingFlags.NonPublic);
            var methods = stackTrace.GetFrames()?.Select(x => x.GetMethod());
            return methods.Contains(addMethod) || methods.Contains(onUserAddedMethod);
        }
    }
}
