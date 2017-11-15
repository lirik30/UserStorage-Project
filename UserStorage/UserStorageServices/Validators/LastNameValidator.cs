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
            PropertyInfo userLastNameInfo = typeof(User).GetProperty("LastName");

            //var notNullAttribute = userLastNameInfo.GetCustomAttributes<ValidateNotNullOrEmptyOrWhitespaceAttribute>().FirstOrDefault();
            //if (notNullAttribute != null)
            //    if (string.IsNullOrWhiteSpace(user.LastName))
            //        throw new LastNameIsNullOrEmptyException("Last name of user is null or empty or whitespace");

            //var lengthAttribute = userLastNameInfo.GetCustomAttributes<ValidateMaxLengthAttribute>().FirstOrDefault();
            //if (lengthAttribute != null)
            //    if (user.LastName.Length > lengthAttribute.MaxLength)
            //        throw new LastNameExceedsLimitException($"Last name of user must be less than {lengthAttribute.MaxLength} symbols");

            //var regexAttribute = userLastNameInfo.GetCustomAttributes<ValidateRegexAttribute>().FirstOrDefault();
            //var regex = new Regex(regexAttribute.RegexString);
            //if (regexAttribute != null)
            //    if (!regex.IsMatch(user.LastName))
            //        throw new LastNameWrongFormatException("Wrong format. Try using only letters");

            var validateAttributes = userLastNameInfo.GetCustomAttributes(); // how to get only validate attributes?
            foreach (var attribute in validateAttributes)
            {
                (attribute as IUserValidator)?.Validate(user);
            }
        }
    }
}
