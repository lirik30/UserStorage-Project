using UserStorageServices.Serializers;

namespace UserStorageServices.Repositories
{
    public class UserMemoryCacheWithState : UserMemoryCache
    {
        private readonly ISerializer _serializer;

        public UserMemoryCacheWithState(ISerializer serializer = null)
        {
            _serializer = serializer ?? new XmlUserSerializer();
        }

        public override void Start()
        {
            base.Start();
            storage = _serializer.DeserializeUsers();
        }

        public override void Stop()
        {
            base.Stop();
            _serializer.SerializeUsers(storage);
        }
    }
}
