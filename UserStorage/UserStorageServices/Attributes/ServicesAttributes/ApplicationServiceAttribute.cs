using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Attributes.ServicesAttributes
{
    public class ApplicationServiceAttribute : Attribute
    {
        public ApplicationServiceAttribute(string serviceType)
        {
            ServiceType = serviceType;
        }

        public string ServiceType { get; }
    }
}
