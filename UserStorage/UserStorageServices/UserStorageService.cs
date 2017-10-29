using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UserStorageServices.Validation_exceptions;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService : IUserStorageService
    {
        private readonly BooleanSwitch loggingSwitch = new BooleanSwitch("enableLogging", "Switch for enable/disable logging");

        /// <summary>
        /// Provides an identifier generation strategy
        /// </summary>
        private readonly IGenerateIdentifier _identifier;

        /// <summary>
        /// Provides a validation strategy
        /// </summary>
        private readonly IUserValidator _validator;

        /// <summary>
        /// User store
        /// </summary>
        private HashSet<User> _storage = new HashSet<User>();

        public UserStorageService(
            IGenerateIdentifier identifier = null, 
            IUserValidator validator = null)
        {
            _identifier = identifier ?? new GuidGenerate();
            _validator = validator ?? new CompositeValidator(new IUserValidator[]{new AgeValidator(), new LastNameValidator(), new FirstNameValidator()});
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => _storage.Count;

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            if (loggingSwitch.Enabled)
            {
                Console.WriteLine("Add() method is called.");
            }

            _validator.Validate(user);

            user.Id = _identifier.Generate();
            _storage.Add(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public void Remove(User user)
        {
            if (loggingSwitch.Enabled)
            {
                Console.WriteLine("Remove() method is called.");
            }

            if (user == null)
            {
                throw new UserIsNullException("User cannot be null");
            }

            var storageContainsUser = _storage.Contains(user);

            if (!storageContainsUser)
            {
                throw new InvalidOperationException("There is no record of such user in the storage");
            }

            _storage.Remove(user);
        }

        /// <summary>
        /// Search through the storage for the users with the same first name
        /// </summary>
        /// <param name="firstName">First name for the search</param>
        /// <returns>Set of the users</returns>
        public IEnumerable<User> SearchByFirstName(string firstName)
        {
            if (loggingSwitch.Enabled)
            {
                Console.WriteLine("SearchByFirstName() method is called.");
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new FirstNameIsNullOrEmptyException("FirstName is null or empty or whitespace");
            }

            return Search(user => user.FirstName == firstName);
        }

        /// <summary>
        /// Search through the storage for the users with the same last name
        /// </summary>
        /// <param name="lastName">Last name for the search</param>
        /// <returns>Set of the users</returns>
        public IEnumerable<User> SearchByLastName(string lastName)
        {
            if (loggingSwitch.Enabled)
            {
                Console.WriteLine("SearchByLastName() method is called.");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new LastNameIsNullOrEmptyException("LastName is null or empty or whitespace");
            }

            return Search(user => user.LastName == lastName);
        }

        /// <summary>
        /// Search through the storage for the users with the same age
        /// </summary>
        /// <param name="age">Age for the search</param>
        /// <returns>Set of the users</returns>
        public IEnumerable<User> SearchByAge(int age)
        {
            if (loggingSwitch.Enabled)
            {
                Console.WriteLine("SearchByAge() method is called.");
            }

            if (age < 0)
            {
                throw new AgeExceedsLimitException("Age is less than zero");
            }

            return Search(user => user.Age == age);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        private IEnumerable<User> Search(Func<User, bool> predicate)
        {
            var searchResult = _storage.Where(predicate);

            if (searchResult.Count() == 0)
            {
                // TODO: realize exception when the search result is empty. or not
            }

            return searchResult;
        }
    }
}
