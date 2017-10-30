using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using UserStorageServices.Validation_exceptions;

namespace UserStorageServices
{

    public enum StorageMode
    {
        MasterNode,
        SlaveNode
    };

    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService : IUserStorageService
    {
        /// <summary>
        /// Master or slave mode of the storage
        /// </summary>
        private readonly StorageMode _storageMode;

        /// <summary>
        /// Subscribers of the UserStorageService
        /// </summary>
        private List<ISubscriber> _subscribers;

        /// <summary>
        /// Provides an identifier generation strategy
        /// </summary>
        private readonly IGenerateIdentifier _identifier;

        /// <summary>
        /// Provides data replication for master node
        /// </summary>
        private readonly IEnumerable<IUserStorageService> _slaveServices;

        /// <summary>
        /// Provides a validation strategy
        /// </summary>
        private readonly IUserValidator _validator;

        /// <summary>
        /// User store
        /// </summary>
        private HashSet<User> _storage = new HashSet<User>();

        public UserStorageService(
            StorageMode storageMode,
            IEnumerable<IUserStorageService> slaveServices = null,
            IGenerateIdentifier identifier = null, 
            IUserValidator validator = null )
        {
            _subscribers = new List<ISubscriber>();
            _storageMode = storageMode;
            _slaveServices = slaveServices ?? new List<IUserStorageService>();
            _identifier = identifier ?? new GuidGenerate();
            _validator = validator ?? new CompositeValidator(new IUserValidator[] { new AgeValidator(), new LastNameValidator(), new FirstNameValidator() });
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => _storage.Count;

        private void AddInSlave(User user)
        {
            Console.WriteLine("slave addition");
            _storage.Add(user); 
        }

        private void RemoveInSlave(User user)
        {
            Console.WriteLine("slave removal");
            _storage.Remove(user);
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }


        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            if (_storageMode != StorageMode.MasterNode)
            {
                throw new NotSupportedException("The slave node isn't support write operation.");
            }

            _validator.Validate(user);
            user.Id = _identifier.Generate();

            
            foreach (var slave in _slaveServices)
            {
                ((UserStorageService)slave).AddInSlave(user);  // !!!!!!!
            }

            _storage.Add(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public void Remove(User user)
        {
            if (_storageMode != StorageMode.MasterNode)
            {
                throw new NotSupportedException("The slave node isn't support remove operation.");
            }

            if (user == null)
            {
                throw new UserIsNullException("User cannot be null");
            }

            var resultUsers = 
                Search(u => u.FirstName == user.FirstName && u.LastName == user.LastName && u.Age == user.Age);

            if (!resultUsers.Any())
            {
                throw new ArgumentException("There is no record of such user in the storage");
            }

            if (resultUsers.Count() != 1)
            {
                throw new InvalidOperationException("Operation is invalid. Multiple choices possible");
            }

            
            foreach (var slave in _slaveServices)
            {
                ((UserStorageService)slave).RemoveInSlave(resultUsers.First());  // !!!!!!!
            }

            _storage.Remove(resultUsers.First());
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public IEnumerable<User> Search(Func<User, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), "Predicate must be not null");
            }

            var searchResult = _storage.Where(predicate);

            return searchResult;
        }
    }
}
