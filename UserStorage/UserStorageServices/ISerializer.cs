using System.Collections.Generic;

namespace UserStorageServices
{
    public interface ISerializer
    {
        void SerializeUsers(HashSet<User> users);

        HashSet<User> DeserializeUsers();
    }
}
