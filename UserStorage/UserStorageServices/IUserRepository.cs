using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public interface IUserRepository
    {
        void Start();

        void Stop();

        void Get();

        void Set();

        void Query();
    }
}
