using System;
using System.Collections.Generic;

namespace UserStorageServices.Repositories
{
    public interface IUserRepository
    {
        int Count { get; }

        void Add(User user);

        void Remove(User user);

        IEnumerable<User> Search(Func<User, bool> predicate);
    }
}
