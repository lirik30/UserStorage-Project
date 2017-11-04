using System;
using System.Runtime.Serialization;

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
