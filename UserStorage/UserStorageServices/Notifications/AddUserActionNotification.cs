using System;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    [Serializable]
    public class AddUserActionNotification
    {
        [XmlElement("user")]
        public User User { get; set; }
    }
}
