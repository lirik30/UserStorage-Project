using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Validation_exceptions
{
    public class LastNameWrongFormatException : Exception
    {
        public LastNameWrongFormatException()
        {
        }

        public LastNameWrongFormatException(string message) : base(message)
        {
        }

        public LastNameWrongFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LastNameWrongFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
