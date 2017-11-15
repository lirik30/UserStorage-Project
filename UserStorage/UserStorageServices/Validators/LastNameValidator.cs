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
            
            var validateAttributes = userLastNameInfo.GetCustomAttributes();
            foreach (var attribute in validateAttributes)
            {
                (attribute as IUserValidator)?.Validate(user);
            }
        }
    }
}
