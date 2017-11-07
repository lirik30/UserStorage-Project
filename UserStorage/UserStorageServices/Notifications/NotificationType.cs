using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
