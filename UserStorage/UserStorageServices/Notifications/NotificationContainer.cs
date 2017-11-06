using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    [Serializable]
    public class Notification
    {
        [XmlIgnore]
        public NotificationType Type { get; set; }

        [XmlElement("addUser", typeof(AddUserActionNotification))]
        [XmlElement("deleteUser", typeof(DeleteUserActionNotification))]
        [XmlChoiceIdentifier("Type")]
        public object Action { get; set; }
    }

    [Serializable]
    [XmlType(IncludeInSchema = false)]
    public enum NotificationType
    {
        [XmlEnum("addUser")]
        AddUser,

        [XmlEnum("deleteUser")]
        DeleteUser
    }

    [Serializable]
    public class DeleteUserActionNotification
    {
        [XmlElement("user")]
        public User User { get; set; }
    }

    [Serializable]
    public class AddUserActionNotification
    {
        [XmlElement("user")]
        public User User { get; set; }
    }
}
