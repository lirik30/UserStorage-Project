using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService
    {
        /// <summary>
        /// User store
        /// </summary>
        private HashSet<User> _storage = new HashSet<User>();

        private readonly IGenerateIdentifier _identifier;
        private readonly IUserValidator _validator;

        public UserStorageService(IGenerateIdentifier identifier = null, 
                                  IUserValidator validator = null)
        {
            _identifier = identifier ?? new GuidGenerate();
            _validator = validator ?? new UserValidator();
        }

        public bool IsLoggingEnabled { get; set; }

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
            if (IsLoggingEnabled)
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
            if (IsLoggingEnabled)
            {
                Console.WriteLine("Remove() method is called.");
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var storageContainsUser = _storage.Contains(user);

            if (!storageContainsUser)
            {
                throw new InvalidOperationException("There is no record of such user in the storage");
            }

            _storage.Remove(user);
        }

        public IEnumerable<User> SearchByFirstName(string firstName)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("SearchByFirstName() method is called.");
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("FirstName is null or empty or whitespace");
            }

            return Search(user => user.FirstName == firstName);
        }

        public IEnumerable<User> SearchByLastName(string lastName)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("SearchByLastName() method is called.");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("LastName is null or empty or whitespace");
            }

            return Search(user => user.LastName == lastName);
        }

        public IEnumerable<User> SearchByAge(int age)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("SearchByAge() method is called.");
            }

            if (age < 0)
            {
                throw new ArgumentException("Age is less than zero");
            }

            return Search(user => user.Age == age);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public IEnumerable<User> Search(Func<User, bool> predicate)
        {
            var searchResult = _storage.Where(x => predicate(x));

            if (searchResult.Count() == 0)
            {
                // TODO: realize exception when the search result is empty
            }

            return searchResult;
        }
    }
}
