using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Validation_exceptions;

namespace UserStorageServices
{
    public class AgeValidator : IUserValidator
    {
        public void Validate(User user)
        {
            if (user.Age < 0)
                throw new AgeExceedsLimitException("Age of user must be greater than zero");
        }
    }
}
