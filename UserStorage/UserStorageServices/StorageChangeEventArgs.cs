using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class StorageChangeEventArgs : EventArgs
    {
        public StorageChangeEventArgs(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}
