﻿using System;
using System.Collections.Generic;
using UserStorageServices.Serializers;

namespace UserStorageServices.Repositories
{
    [Serializable]
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
            _lock.EnterWriteLock();
            var users = new HashSet<User>();
            try
            {
                PreviousIdentifier = _identifierSerializer.Deserialize();
                users = _serializer.Deserialize();
            }
            finally
            {
                _lock.ExitWriteLock();
            }

            foreach (var user in users)
            {
                Add(user);
            }
        }

        /// <summary>
        /// Stop working of the repository. Saving an identifier and the users
        /// </summary>
        public void Stop()
        {
            _lock.EnterWriteLock();
            try
            {
                _identifierSerializer.Serialize(PreviousIdentifier);
                _serializer.Serialize(storage);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
