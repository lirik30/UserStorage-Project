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
        protected HashSet<User> Storage = new HashSet<User>();

        public int Count => Storage.Count;

        public virtual void Start(string path)
        {
            
        }

        public virtual void Stop(string path)
        {

        }

        public void Add(User user)
        {
            Storage.Add(user);
        }

        public void Remove(User user)
        {
            Storage.Remove(user);
        }

        public IEnumerable<User> Search(Func<User, bool> predicate)
        {
            return Storage.Where(predicate);
        }
    }
}
