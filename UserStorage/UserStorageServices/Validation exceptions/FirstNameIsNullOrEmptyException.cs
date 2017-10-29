using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
