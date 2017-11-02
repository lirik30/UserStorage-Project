using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserMemoryCacheWithState : UserMemoryCache
    {
        public override void Start(string path) //path - repository.bin
        {
            path = path ?? "repository.bin";
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
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

        public override void Stop(string path)
        {
            if(Storage == null)
                throw new InvalidOperationException();

            path = path ?? "repository.bin";
            
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
