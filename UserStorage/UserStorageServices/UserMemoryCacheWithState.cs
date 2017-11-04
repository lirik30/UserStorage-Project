using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;

namespace UserStorageServices
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
            Storage = _serializer.DeserializeUsers();
        }

        public override void Stop()
        {
            base.Stop();
            _serializer.SerializeUsers(Storage);
        }
    }
}
