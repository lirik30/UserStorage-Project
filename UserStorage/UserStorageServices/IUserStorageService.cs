using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public interface IUserStorageService
    {
        int Count { get; }

        void Add(User user);

        void Remove(User user);

        IEnumerable<User> SearchByFirstName(string firstName);

        IEnumerable<User> SearchByLastName(string lastName);

        IEnumerable<User> SearchByAge(int age);
    }
}
