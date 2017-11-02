using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserMemoryCache : IUserRepository
    {
        /// <summary>
        /// User store
        /// </summary>
        private readonly HashSet<User> _storage = new HashSet<User>();

        public int Count => _storage.Count;

        public void Start()
        {
            
        }

        public void Stop()
        {

        }

        public void Add(User user)
        {
            _storage.Add(user);
        }

        public void Remove(User user)
        {
            _storage.Remove(user);
        }

        public IEnumerable<User> Search(Func<User, bool> predicate)
        {
            return _storage.Where(predicate);
        }
    }
}
