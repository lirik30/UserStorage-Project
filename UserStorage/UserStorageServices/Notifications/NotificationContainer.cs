using System;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    [Serializable]
    [XmlRoot("NotificationContainer", IsNullable = false, Namespace = "http://tempuri.org/userService/notification")]
    public class NotificationContainer
    {
        [XmlArray("notifications")]
        [XmlArrayItem("notification")]
        public Notification[] Notifications { get; set; }
    }
}
