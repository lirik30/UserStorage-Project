using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Validation_exceptions;

namespace UserStorageServices
{
    public class LastNameValidator : IUserValidator
    {
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.LastName))
                throw new LastNameIsNullOrEmptyException("Last name of user is null or empty or whitespace");

            if (user.LastName.Length > 50)
                throw new LastNameIsNullOrEmptyException("Last name of user must be less than 50 symbols");
        }
    }
}
