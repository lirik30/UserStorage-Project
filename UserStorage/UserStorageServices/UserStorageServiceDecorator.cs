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
            var slave1 = new UserStorageServiceSlave();
            var slave2 = new UserStorageServiceSlave();
            this.Wrappee = wrappee ?? new UserStorageServiceMaster(new[]{slave1, slave2});
        }

        public StorageMode StorageMode => Wrappee.StorageMode;

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
