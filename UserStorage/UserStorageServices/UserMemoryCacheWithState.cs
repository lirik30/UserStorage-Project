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
        public override void Start() //path - repository.bin
        {
            var path = ConfigurationManager.AppSettings["RepositoryBinDataFile"];
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                Console.WriteLine(path);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    HashSet<User> temp = (HashSet<User>)formatter.Deserialize(fs);
                    Storage = temp;
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

        public override void Stop()
        {
            if(Storage == null)
                throw new InvalidOperationException();

            var path = ConfigurationManager.AppSettings["RepositoryBinDataFile"];
            Console.WriteLine(path);
            FileStream fs = new FileStream(path, FileMode.Create);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Console.WriteLine(Storage.Count);
                formatter.Serialize(fs, Storage);
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
    }
}
