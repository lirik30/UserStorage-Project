using System;
using System.Text.RegularExpressions;
using UserStorageServices.Validation_exceptions;
using UserStorageServices.Validators;

namespace UserStorageServices.Attributes.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateRegexAttribute : Attribute, IUserValidator
    {
        public ValidateRegexAttribute(string regexString)
        {
            RegexString = regexString;
        }

        public string RegexString { get; }

        public void Validate(User user)
        {
            if (!new Regex(RegexString).IsMatch(user.FirstName))
                throw new FirstNameWrongFormatException("Wrong format. Try using only letters");

            if (!new Regex(RegexString).IsMatch(user.LastName))
                throw new LastNameWrongFormatException("Wrong format. Try using only letters");
        }
    }
}
