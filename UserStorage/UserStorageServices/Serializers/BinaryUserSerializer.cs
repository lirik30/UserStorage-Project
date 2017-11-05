using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace UserStorageServices.Serializers
{
    /// <summary>
    /// Provides functionality to saving and loading users collection to the binary file
    /// </summary>
    public class BinaryUserSerializer : ISerializer<HashSet<User>>
    {
        public void Serialize(HashSet<User> users)
        {
            if (users == null)
                throw new InvalidOperationException();

            var path = ConfigurationManager.AppSettings["RepositoryBinDataFile"];
            var fs = new FileStream(path, FileMode.Create);
            try
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fs, users);
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

        public HashSet<User> Deserialize()
        {
            var path = ConfigurationManager.AppSettings["RepositoryBinDataFile"];
            try
            {
                var fs = new FileStream(path, FileMode.Open);
                try
                {
                    var formatter = new BinaryFormatter();
                    var temp = (HashSet<User>)formatter.Deserialize(fs);
                    return temp;
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
    }
}
