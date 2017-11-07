using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    [Serializable]
    public class DeleteUserActionNotification
    {
        [XmlElement("user")]
        public User User { get; set; }
    }
}
