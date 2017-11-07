using System;
using System.Collections.Generic;
using UserStorageServices.Notifications;
using UserStorageServices.Repositories;
using UserStorageServices.Validators;

namespace UserStorageServices.Services
{
    public class UserStorageServiceMaster : UserStorageServiceBase
    {
        private readonly INotificationSender _sender;

        public UserStorageServiceMaster(
            INotificationSender sender = null, 
            IUserRepository userRepository = null,
            IUserValidator validator = null) : base(validator, userRepository)
        {
            _sender = sender ?? new NotificationSender();
            (Repository as IUserRepositoryManager)?.Start();
            foreach (var user in Repository.Search(x => x.FirstName != null))
            {
                OnUserAdded(user);
            }
        }

        ~UserStorageServiceMaster()
        {
            (Repository as IUserRepositoryManager)?.Stop();
        }

        /// <summary>
        /// Mode of the storage
        /// </summary>
        public override StorageMode StorageMode => StorageMode.MasterNode;

        /// <summary>
        /// Addition of the user in the master node.
        /// </summary>
        /// <param name="user"></param>
        public override void Add(User user)
        {
            base.Add(user);

            OnUserAdded(user);
        }

        /// <summary>
        /// Removal user from the master node
        /// </summary>
        /// <param name="user"></param>
        public override void Remove(User user)
        {
            base.Remove(user);

            OnUserRemoved(user);
        }

        /// <summary>
        /// Occurs when the user is added to the master
        /// </summary>
        /// <param name="user">Information about added user</param>
        private void OnUserAdded(User user)
        {
            _sender.Send(new NotificationContainer
            {
                Notifications = new[]
                {
                    new Notification
                    {
                        Action = new AddUserActionNotification { User = user },
                        Type = NotificationType.AddUser
                    }
                }
            });
        }

        /// <summary>
        /// Occurs when the user is removed from the storage
        /// </summary>
        /// <param name="user">Information about removed user</param>
        private void OnUserRemoved(User user) 
        {
            _sender.Send(new NotificationContainer
            {
                Notifications = new[]
                {
                    new Notification
                    {
                        Action = new DeleteUserActionNotification { User = user },
                        Type = NotificationType.DeleteUser
                    }
                }
            });
        }
    }
}
