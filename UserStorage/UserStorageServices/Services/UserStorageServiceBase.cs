using System;
using System.Collections.Generic;
using System.Linq;
using UserStorageServices.Repositories;
using UserStorageServices.Validation_exceptions;
using UserStorageServices.Validators;

namespace UserStorageServices.Services
{
    public enum StorageMode
    {
        MasterNode,
        SlaveNode
    }

    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    [Serializable]
    public abstract class UserStorageServiceBase : MarshalByRefObject, IUserStorageService
    {
        /// <summary>
        /// Users repository
        /// </summary>
        protected readonly IUserRepository Repository;

        /// <summary>
        /// Provides a validation strategy
        /// </summary>
        protected readonly IUserValidator Validator;

        protected UserStorageServiceBase(
            IUserValidator validator = null,
            IUserRepository repository = null)
        {
            Repository = repository ?? new UserMemoryCacheWithState();
            Validator = validator ?? new CompositeValidator(new IUserValidator[] { new AgeValidator(), new LastNameValidator(), new FirstNameValidator() });
        }

        /// <summary>
        /// Mode of the working
        /// </summary>
        public abstract StorageMode StorageMode { get; }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => Repository.Count;

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public virtual void Add(User user)
        {
            Validator.Validate(user);
            user.Id = ++((UserMemoryCache)Repository).PreviousIdentifier;
            Repository.Add(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public virtual void Remove(User user)
        {
            if (user == null)
                throw new UserIsNullException("User cannot be null");

            var resultUsers = 
                Search(u => u.FirstName == user.FirstName && u.LastName == user.LastName && u.Age == user.Age);

            if (!resultUsers.Any())
                throw new ArgumentException("There is no record of such user in the storage");

            if (resultUsers.Count() != 1)
                throw new InvalidOperationException("Operation is invalid. Multiple choices possible");

            Repository.Remove(resultUsers.First());
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public IEnumerable<User> Search(Func<User, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate must be not null");

            return Repository.Search(predicate);
        }
    }
}
