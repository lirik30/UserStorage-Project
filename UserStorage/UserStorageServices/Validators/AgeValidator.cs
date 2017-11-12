using System;
using UserStorageServices.Validation_exceptions;

namespace UserStorageServices.Validators
{
    [Serializable]
    public class AgeValidator : IUserValidator
    {
        public void Validate(User user)
        {
            if (user.Age < 0)
                throw new AgeExceedsLimitException("Age of user must be greater than zero");
        }
    }
}
