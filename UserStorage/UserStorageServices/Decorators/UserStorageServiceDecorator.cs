using System;
using System.Collections.Generic;
using UserStorageServices.Services;

namespace UserStorageServices.Decorators
{
    public abstract class UserStorageServiceDecorator : IUserStorageService
    {
        protected readonly IUserStorageService Wrappee;

        protected UserStorageServiceDecorator(IUserStorageService wrappee = null)
        {
            var slave1 = new UserStorageServiceSlave();
            var slave2 = new UserStorageServiceSlave();
            Wrappee = wrappee ?? new UserStorageServiceMaster();
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
