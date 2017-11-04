using System;

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
