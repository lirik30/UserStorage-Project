using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public abstract class UserStorageServiceDecorator : IUserStorageService
    {
        protected readonly IUserStorageService Wrappee;

        protected UserStorageServiceDecorator(IUserStorageService wrappee = null)
        {
            this.Wrappee = wrappee ?? new UserStorageService(StorageMode.MasterNode);
        }

        public virtual int Count => Wrappee.Count;

        public virtual void Add(User user)
        {
            Wrappee.Add(user);
        }

        public virtual void Remove(User user)
        {
            Wrappee.Remove(user);
        }

        public virtual IEnumerable<User> Search(Func<User, bool> predicate)
        {
            return Wrappee.Search(predicate);
        }
    }
}
