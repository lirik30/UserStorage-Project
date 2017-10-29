using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public abstract class UserStorageServiceDecorator : IUserStorageService
    {
        protected readonly IUserStorageService _wrappee;

        protected UserStorageServiceDecorator(IUserStorageService wrappee = null)
        {
            _wrappee = wrappee ?? new UserStorageService();
        }

        public virtual int Count => _wrappee.Count;

        public virtual void Add(User user)
        {
            _wrappee.Add(user);
        }

        public virtual void Remove(User user)
        {
            _wrappee.Remove(user);
        }

        public virtual IEnumerable<User> Search(Func<User, bool> predicate)
        {
            return _wrappee.Search(predicate);
        }
    }
}
