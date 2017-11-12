using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace UserStorageServices.Repositories
{
    [Serializable]
    public class UserMemoryCache : IUserRepository
    {
        /// <summary>
        /// User store
        /// </summary>
        protected HashSet<User> storage = new HashSet<User>();

        public int PreviousIdentifier { get; set; }

        public int Count => storage.Count;

        public void Add(User user)
        {
            storage.Add(user);
        }

        public void Remove(User user)
        {
            storage.Remove(user);
        }

        public IEnumerable<User> Search(Func<User, bool> predicate)
        {
            return storage.Where(predicate).ToList();
        }
    }
}
