using System;
using System.Runtime.Serialization;

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
