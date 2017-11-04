using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace UserStorageServices
{
    public class XmlUserSerializer : ISerializer
    {
        public void SerializeUsers(HashSet<User> users)
        {
            if (users == null)
                throw new InvalidOperationException();

            var path = ConfigurationManager.AppSettings["RepositoryXmlDataFile"];
            var fs = new FileStream(path, FileMode.Create);
            try
            {
                var formatter = new XmlSerializer(typeof(HashSet<User>));
                formatter.Serialize(XmlWriter.Create(fs), users);
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

        public HashSet<User> DeserializeUsers()
        {
            var path = ConfigurationManager.AppSettings["RepositoryXmlDataFile"];
            try
            {
                var fs = new FileStream(path, FileMode.Open);
                try
                {
                    var formatter = new XmlSerializer(typeof(HashSet<User>));
                    var temp = (HashSet<User>)formatter.Deserialize(XmlReader.Create(fs));
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
