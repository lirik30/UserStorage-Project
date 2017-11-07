using System;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace UserStorageServices.Serializers
{
    /// <summary>
    /// Provides functionality for the saving and loading of the last identifier
    /// </summary>
    public class IdentifierSerializer : ISerializer<int>
    {
        /// <summary>
        /// Save the identifier
        /// </summary>
        /// <param name="data">Data to the serialize</param>
        public void Serialize(int data)
        {
            var fs = new FileStream("identifier.bin", FileMode.Create);
            try
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fs, data);
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

        /// <summary>
        /// Load the identifier
        /// </summary>
        /// <returns>Deserialized identifier</returns>
        public int Deserialize()
        {
            try
            {
                var path = ConfigurationManager.AppSettings["IdentifierFile"];
                var fs = new FileStream(path, FileMode.Open);
                try
                {
                    var formatter = new BinaryFormatter();
                   return (int)formatter.Deserialize(fs);
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
            catch (FileNotFoundException)
            {
                return 0;
            }
        }
    }
}
