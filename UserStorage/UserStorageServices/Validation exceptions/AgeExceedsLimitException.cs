using System;
using System.Runtime.Serialization;

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
