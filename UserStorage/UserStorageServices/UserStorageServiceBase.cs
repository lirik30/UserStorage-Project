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
    public abstract class UserStorageServiceBase : IUserStorageService
    {
        /// <summary>
        /// Master or slave mode of the storage
        /// </summary>
        protected readonly StorageMode _storageMode;


        /// <summary>
        /// Provides an identifier generation strategy
        /// </summary>
        protected readonly IGenerateIdentifier _identifier;

        /// <summary>
        /// Provides a validation strategy
        /// </summary>
        protected readonly IUserValidator _validator;

        /// <summary>
        /// User store
        /// </summary>
        protected HashSet<User> _storage = new HashSet<User>();

        public UserStorageServiceBase(
            StorageMode storageMode,
            IGenerateIdentifier identifier = null, 
            IUserValidator validator = null )
        {
            _storageMode = storageMode;
            _identifier = identifier ?? new GuidGenerate();
            _validator = validator ?? new CompositeValidator(new IUserValidator[] { new AgeValidator(), new LastNameValidator(), new FirstNameValidator() });
        }

        public abstract StorageMode StorageMode { get; }

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


        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public virtual void Add(User user)
        {
            _validator.Validate(user);
            user.Id = _identifier.Generate();

            _storage.Add(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public virtual void Remove(User user)
        {
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
