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
            
            var validateAttributes = userFirstNameInfo.GetCustomAttributes();
            foreach (var attribute in validateAttributes)
            {
                (attribute as IUserValidator)?.Validate(user);
            }
        }
    }
}
