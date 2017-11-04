﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserMemoryCache : IUserRepository
    {
        /// <summary>
        /// User store
        /// </summary>
        protected HashSet<User> Storage = new HashSet<User>();

        public int PreviousIdentifier { get; set; }

        public int Count => Storage.Count;

        public virtual void Start()
        {
            try
            {
                var fs = new FileStream("identifier.bin", FileMode.Open);
                try
                {
                    var formatter = new BinaryFormatter();
                    var temp = (int)formatter.Deserialize(fs);
                    PreviousIdentifier = temp;
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
        }

        public virtual void Stop()
        {
            var fs = new FileStream("identifier.bin", FileMode.Create);
            try
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fs, PreviousIdentifier);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        public void Add(User user)
        {
            Storage.Add(user);
        }

        public void Remove(User user)
        {
            Storage.Remove(user);
        }

        public IEnumerable<User> Search(Func<User, bool> predicate)
        {
            return Storage.Where(predicate);
        }
    }
}
