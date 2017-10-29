using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Validation_exceptions
{
    public class LastNameExceedsLimitException : Exception
    {
        public LastNameExceedsLimitException()
        {
        }

        public LastNameExceedsLimitException(string message) : base(message)
        {
        }

        public LastNameExceedsLimitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LastNameExceedsLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
