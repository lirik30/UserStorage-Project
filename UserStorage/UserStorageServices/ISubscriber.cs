using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public interface ISubscriber
    {
        void UserAdded(object sender, StorageChangeEventArgs eventArgs);

        void UserRemoved(object sender, StorageChangeEventArgs eventArgs);
    }
}
