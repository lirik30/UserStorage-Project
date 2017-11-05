﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UserStorageServices.Serializers;

namespace UserStorageServices.Repositories
{
    public class UserMemoryCacheWithState : UserMemoryCache, IUserRepositoryManager
    {
        /// <summary>
        /// Serializer for the users
        /// </summary>
        private readonly ISerializer<HashSet<User>> _serializer;

        /// <summary>
        /// Serializer for the identifier
        /// </summary>
        private readonly ISerializer<int> _identifierSerializer = new IdentifierSerializer();

        public UserMemoryCacheWithState(ISerializer<HashSet<User>> serializer = null)
        {
            _serializer = serializer ?? new XmlUserSerializer();
        }

        /// <summary>
        /// Start working of the repository. Loading an identifier and the users
        /// </summary>
        public void Start()
        {
            PreviousIdentifier = _identifierSerializer.Deserialize();
            storage = _serializer.Deserialize();
        }

        /// <summary>
        /// Stop working of the repository. Saving an identifier and the users
        /// </summary>
        public void Stop()
        {
            _identifierSerializer.Serialize(PreviousIdentifier);
            _serializer.Serialize(storage);
        }
    }
}
