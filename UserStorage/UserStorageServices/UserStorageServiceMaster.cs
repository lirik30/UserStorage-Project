using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserStorageServiceMaster : UserStorageServiceBase
    {
        /// <summary>
        /// Notifies slaves about user addition in master
        /// </summary>
        public EventHandler<StorageChangeEventArgs> UserAddedEvent = delegate { };

        /// <summary>
        /// Notifies slaves about user removal in master
        /// </summary>
        public EventHandler<StorageChangeEventArgs> UserRemovedEvent = delegate { };

        /// <summary>
        /// Provides data replication for master node
        /// </summary>
        private readonly IEnumerable<IUserStorageService> _slaveServices;

        public UserStorageServiceMaster(
            IEnumerable<UserStorageServiceSlave> slaveServices = null, 
            IGenerateIdentifier identifier = null,
            IUserValidator validator = null) : base(identifier, validator)
        {
            _slaveServices = slaveServices ?? new List<UserStorageServiceSlave>();
            foreach (var slave in _slaveServices)
            {
                AddSubscriber(slave as ISubscriber);
            }
        }

        /// <summary>
        /// Mode of the storage
        /// </summary>
        public override StorageMode StorageMode => StorageMode.MasterNode;

        public void AddSubscriber(ISubscriber subscriber)
        {
            UserAddedEvent += subscriber.UserAdded;
            UserRemovedEvent += subscriber.UserRemoved;
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            UserAddedEvent -= subscriber.UserAdded;
            UserRemovedEvent -= subscriber.UserRemoved;
        }

        public override void Add(User user)
        {
            base.Add(user);

            OnUserAdded(new StorageChangeEventArgs(user));
        }

        public override void Remove(User user)
        {
            base.Remove(user);

            OnUserRemoved(new StorageChangeEventArgs(user));
        }

        /// <summary>
        /// Occurs when the user is added to the master
        /// </summary>
        /// <param name="eventArgs">Information about added user</param>
        private void OnUserAdded(StorageChangeEventArgs eventArgs)
        {
            EventHandler<StorageChangeEventArgs> temp = UserAddedEvent;
            temp?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// Occurs when the user is removed from the storage
        /// </summary>
        /// <param name="eventArgs">Information about removed user</param>
        private void OnUserRemoved(StorageChangeEventArgs eventArgs)
        {
            EventHandler<StorageChangeEventArgs> temp = UserRemovedEvent;
            temp?.Invoke(this, eventArgs);
        }
    }
}
