using System;
using System.Collections.Generic;

namespace UserStorageServices.Services
{
    public interface IUserStorageService
    {
        StorageMode StorageMode { get; }

        int Count { get; }

        void Add(User user);

        void Remove(User user);

        IEnumerable<User> Search(Func<User, bool> predicate);
    }
}
