using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UserStorageServices.Attributes.ValidationAttributes;
using UserStorageServices.Validation_exceptions;

namespace UserStorageServices.Validators
{
    [Serializable]
    public class FirstNameValidator : IUserValidator
    {
        public void Validate(User user)
        {
            PropertyInfo userFirstNameInfo = typeof(User).GetProperty("FirstName");

            //var notNullAttribute = userAgeInfo.GetCustomAttributes<ValidateNotNullOrEmptyOrWhitespaceAttribute>().FirstOrDefault();
            //if(notNullAttribute != null)
            //    if (string.IsNullOrWhiteSpace(user.FirstName))
            //        throw new FirstNameIsNullOrEmptyException("FirstName is null or empty or whitespace");

            //var lengthAttribute = userAgeInfo.GetCustomAttributes<ValidateMaxLengthAttribute>().FirstOrDefault();
            //if(lengthAttribute != null)
            //    if (user.FirstName.Length > lengthAttribute.MaxLength)
            //        throw new FirstNameExceedsLimitException($"First name of user must be less than {lengthAttribute.MaxLength} symbols");

            //var regexAttribute = userAgeInfo.GetCustomAttributes<ValidateRegexAttribute>().FirstOrDefault();
            //var regex = new Regex(regexAttribute.RegexString);
            //if (regexAttribute != null)
            //    if (!regex.IsMatch(user.FirstName))
            //        throw new FirstNameWrongFormatException("Wrong format. Try using only letters");

            var validateAttributes = userFirstNameInfo.GetCustomAttributes(); // how to get only validate attributes?
            foreach (var attribute in validateAttributes)
            {
                (attribute as IUserValidator)?.Validate(user);
            }
        }
    }
}
