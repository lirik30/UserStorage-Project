using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UserStorageServices.Attributes.ValidationAttributes;
using UserStorageServices.Validation_exceptions;

namespace UserStorageServices.Validators
{
    [Serializable]
    public class LastNameValidator : IUserValidator
    {
        public void Validate(User user)
        {
            PropertyInfo userAgeInfo = typeof(User).GetProperty("LastName");

            var notNullAttribute = userAgeInfo.GetCustomAttributes<ValidateNotNullOrEmptyOrWhitespaceAttribute>().FirstOrDefault();
            if (notNullAttribute != null)
                if (string.IsNullOrWhiteSpace(user.LastName))
                    throw new LastNameIsNullOrEmptyException("Last name of user is null or empty or whitespace");

            var lengthAttribute = userAgeInfo.GetCustomAttributes<ValidateMaxLengthAttribute>().FirstOrDefault();
            if (lengthAttribute != null)
                if (user.LastName.Length > lengthAttribute.MaxLength)
                    throw new LastNameIsNullOrEmptyException($"Last name of user must be less than {lengthAttribute.MaxLength} symbols");

            var regexAttribute = userAgeInfo.GetCustomAttributes<ValidateRegexAttribute>().FirstOrDefault();
            var regex = new Regex(regexAttribute.RegexString);
            if (regexAttribute != null)
                if (!regex.IsMatch(user.LastName))
                    throw new LastNameWrongFormatException("Wrong format. Try using only letters");
        }
    }
}
