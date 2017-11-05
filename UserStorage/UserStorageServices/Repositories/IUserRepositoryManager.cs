using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Repositories
{
    public interface IUserRepositoryManager
    {
        void Start();

        void Stop();
    }
}
