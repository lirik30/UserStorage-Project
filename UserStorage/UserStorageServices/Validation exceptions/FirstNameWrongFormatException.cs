using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Validation_exceptions
{
    public class FirstNameWrongFormatException : Exception
    {
        public FirstNameWrongFormatException()
        {
        }

        public FirstNameWrongFormatException(string message) : base(message)
        {
        }

        public FirstNameWrongFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FirstNameWrongFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
