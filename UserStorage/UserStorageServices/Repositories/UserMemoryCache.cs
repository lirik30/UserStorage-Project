using System;
using System.Collections.Generic;
using System.Linq;

namespace UserStorageServices.Repositories
{
    [Serializable]
    public class UserMemoryCache : IUserRepository
    {
        /// <summary>
        /// User store
        /// </summary>
        protected HashSet<User> storage = new HashSet<User>();

        private object _lockObject = new Object();

        public int PreviousIdentifier { get; set; }

        public int Count => storage.Count;

        public void Add(User user)
        {
            lock (_lockObject)
            {
                storage.Add(user);
            }
        }

        public void Remove(User user)
        {
            lock (_lockObject)
            {
                storage.Remove(user);
            }
        }

        public IEnumerable<User> Search(Func<User, bool> predicate)
        {
            return storage.Where(predicate).ToList();
        }
    }
}
