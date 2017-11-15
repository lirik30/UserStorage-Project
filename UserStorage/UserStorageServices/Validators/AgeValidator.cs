using System;
using System.Linq;
using System.Reflection;
using UserStorageServices.Attributes.ValidationAttributes;
using UserStorageServices.Validation_exceptions;

namespace UserStorageServices.Validators
{
    [Serializable]
    public class AgeValidator : IUserValidator
    {
        public void Validate(User user)
        {
            PropertyInfo userAgeInfo = typeof(User).GetProperty("Age");
            
            var validateAttributes = userAgeInfo.GetCustomAttributes();
            foreach (var attribute in validateAttributes)
            {
                (attribute as IUserValidator)?.Validate(user);
            }
        }
    }
}
