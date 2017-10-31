using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class StorageChangeEventArgs : EventArgs
    {
        public User User { get; }

        public StorageChangeEventArgs(User user)
        {
            User = user;
        }
    }

    public class UserStorageServiceMaster : UserStorageServiceBase
    {
        /// <summary>
        /// Subscribers of the UserStorageService
        /// </summary>
        //private List<ISubscriber> _subscribers;

        /// <summary>
        /// Provides data replication for master node
        /// </summary>
        private readonly IEnumerable<IUserStorageService> _slaveServices;

        public EventHandler<StorageChangeEventArgs> UserAddedEvent = delegate { };

        public EventHandler<StorageChangeEventArgs> UserRemovedEvent = delegate { };

        public override StorageMode StorageMode => _storageMode;

        public UserStorageServiceMaster(IEnumerable<UserStorageServiceSlave> slaveServices = null, IGenerateIdentifier identifier = null,
            IUserValidator validator = null) : base(StorageMode.MasterNode, identifier, validator)
        {
            _slaveServices = slaveServices ?? new List<UserStorageServiceSlave>();
            foreach (var slave in _slaveServices)
            {
                AddSubscriber(slave as ISubscriber);
            }
        }

        private void OnUserAdded(StorageChangeEventArgs eventArgs)
        {
            EventHandler<StorageChangeEventArgs> temp = UserAddedEvent;
            temp?.Invoke(this, eventArgs);
        }

        private void OnUserRemoved(StorageChangeEventArgs eventArgs)
        {
            EventHandler<StorageChangeEventArgs> temp = UserRemovedEvent;
            temp?.Invoke(this, eventArgs);
        }

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
    }
}
