using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Validation_exceptions
{
    public class FirstNameExceedsLimitException : Exception
    {
        public FirstNameExceedsLimitException()
        {
        }

        public FirstNameExceedsLimitException(string message) : base(message)
        {
        }

        public FirstNameExceedsLimitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FirstNameExceedsLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
