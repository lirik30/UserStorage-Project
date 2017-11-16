using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace UserStorageServices.Repositories
{
    public class UserMemoryCache : MarshalByRefObject, IUserRepository
    {
        /// <summary>
        /// User store
        /// </summary>
        protected HashSet<User> storage = new HashSet<User>();

        protected ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        ~UserMemoryCache()
        {
            _lock.Dispose();
        }

        public int PreviousIdentifier { get; set; }

        public int Count => storage.Count;

        public void Add(User user)
        {
            _lock.EnterWriteLock();
            try
            {
                storage.Add(user);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Remove(User user)
        {
            _lock.EnterWriteLock();
            try
            {
                storage.Remove(user);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public IEnumerable<User> Search(Func<User, bool> predicate)
        {
            _lock.EnterReadLock();
            try
            {
                return storage.Where(predicate).ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }
}
