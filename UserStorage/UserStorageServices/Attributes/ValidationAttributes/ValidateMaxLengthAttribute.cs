using System;
using UserStorageServices.Validation_exceptions;
using UserStorageServices.Validators;

namespace UserStorageServices.Attributes.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateMaxLengthAttribute : Attribute, IUserValidator
    {
        public ValidateMaxLengthAttribute(int maxLength = 50)
        {
            MaxLength = maxLength;
        }

        public int MaxLength { get; }

        public void Validate(User user)
        {
            if (user.FirstName.Length > MaxLength)
                throw new FirstNameIsNullOrEmptyException($"First name of user must be less than {MaxLength} symbols");
            if (user.LastName.Length > MaxLength)
                throw new LastNameIsNullOrEmptyException($"Last name of user must be less than {MaxLength} symbols");
        }
    }
}
