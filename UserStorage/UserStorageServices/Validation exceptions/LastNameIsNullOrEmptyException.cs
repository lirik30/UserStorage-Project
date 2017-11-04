using System;
using System.Runtime.Serialization;

namespace UserStorageServices.Validation_exceptions
{
    public class LastNameIsNullOrEmptyException : Exception
    {
        public LastNameIsNullOrEmptyException()
        {
        }

        public LastNameIsNullOrEmptyException(string message) : base(message)
        {
        }

        public LastNameIsNullOrEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LastNameIsNullOrEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
