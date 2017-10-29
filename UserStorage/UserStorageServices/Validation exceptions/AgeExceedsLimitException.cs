using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Validation_exceptions
{
    public class AgeExceedsLimitException : Exception
    {
        public AgeExceedsLimitException()
        {
        }

        public AgeExceedsLimitException(string message) : base(message)
        {
        }

        public AgeExceedsLimitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AgeExceedsLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
