using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public interface IUserRepository
    {
        int Count { get; }

        void Start();

        void Stop();

        void Add(User user);

        void Remove(User user);

        IEnumerable<User> Search(Func<User, bool> predicate);
    }
}
