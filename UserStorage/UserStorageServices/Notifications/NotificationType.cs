using System;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    [Serializable]
    [XmlType(IncludeInSchema = false)]
    public enum NotificationType
    {
        [XmlEnum("addUser")]
        AddUser,

        [XmlEnum("deleteUser")]
        DeleteUser
    }
}
