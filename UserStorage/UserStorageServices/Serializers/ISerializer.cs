using System.Collections.Generic;

namespace UserStorageServices.Serializers
{
    public interface ISerializer
    {
        void SerializeUsers(HashSet<User> users);

        HashSet<User> DeserializeUsers();
    }
}
