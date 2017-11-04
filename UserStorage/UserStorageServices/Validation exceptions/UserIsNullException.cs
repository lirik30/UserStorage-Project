using System;
using System.Runtime.Serialization;

namespace UserStorageServices.Validation_exceptions
{
    public class UserIsNullException : Exception
    {
        public UserIsNullException()
        {
        }

        public UserIsNullException(string message) : base(message)
        {
        }

        public UserIsNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserIsNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
