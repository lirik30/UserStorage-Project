using System;
using UserStorageServices.Validation_exceptions;

namespace UserStorageServices.Validators
{
    public class CompositeValidator : IUserValidator
    {
        private IUserValidator[] _validators;

        public CompositeValidator(IUserValidator[] validators)
        {
            _validators = validators;
        }

        public void Validate(User user)
        {
            if (user == null)
                throw new UserIsNullException("User cannot be null");

            foreach (var validator in _validators)
                validator.Validate(user);
        }
    }
}
