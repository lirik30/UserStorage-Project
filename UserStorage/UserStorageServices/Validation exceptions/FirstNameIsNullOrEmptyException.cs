using System;
using System.Runtime.Serialization;

namespace UserStorageServices.Validation_exceptions
{
    public class FirstNameIsNullOrEmptyException : Exception
    {
        public FirstNameIsNullOrEmptyException()
        {
        }

        public FirstNameIsNullOrEmptyException(string message) : base(message)
        {
        }

        public FirstNameIsNullOrEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FirstNameIsNullOrEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
