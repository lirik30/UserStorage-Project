using System;
using UserStorageServices.Validation_exceptions;
using UserStorageServices.Validators;

namespace UserStorageServices.Attributes.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateNotNullOrEmptyOrWhitespaceAttribute : Attribute, IUserValidator
    {
        public void Validate(User user)
        {
            if(string.IsNullOrWhiteSpace(user.FirstName))
                throw new FirstNameIsNullOrEmptyException("First name of user is null or empty or whitespace");

            if (string.IsNullOrWhiteSpace(user.LastName))
                throw new LastNameIsNullOrEmptyException("Last name of user is null or empty or whitespace");
        }
    }
}
