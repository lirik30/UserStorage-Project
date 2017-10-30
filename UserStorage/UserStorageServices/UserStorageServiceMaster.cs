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
        /// Subscribers of the UserStorageService
        /// </summary>
        private List<ISubscriber> _subscribers;

        /// <summary>
        /// Provides data replication for master node
        /// </summary>
        private readonly IEnumerable<IUserStorageService> _slaveServices;

        public override StorageMode StorageMode => _storageMode;

        public UserStorageServiceMaster(IEnumerable<UserStorageServiceSlave> slaveServices = null, IGenerateIdentifier identifier = null,
            IUserValidator validator = null) : base(StorageMode.MasterNode, identifier, validator)
        {
            _slaveServices = slaveServices ?? new List<UserStorageServiceSlave>();
            _subscribers = new List<ISubscriber>();
            foreach (var slave in _slaveServices)
            {
                _subscribers.Add(slave as ISubscriber);
            }
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }


        public override void Add(User user)
        {
            base.Add(user);

            foreach (var subscriber in _subscribers)
            {
                subscriber.UserAdded(user);
            }
        }


        public override void Remove(User user)
        {
            base.Remove(user);
            
            foreach (var subscriber in _subscribers)
            {
                subscriber.UserRemoved(user);
            }
        }
    }
}
