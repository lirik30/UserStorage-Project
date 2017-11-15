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

            //var minMaxAttribute = userAgeInfo.GetCustomAttributes<ValidateMinMaxAttribute>().FirstOrDefault();
            //if(minMaxAttribute != null)
            //    if (user.Age < minMaxAttribute.MinLimit || user.Age > minMaxAttribute.MaxLimit)
            //        throw new AgeExceedsLimitException("Age of user must be greater than zero");

            var validateAttributes = userAgeInfo.GetCustomAttributes(); // how to get only validate attributes?
            foreach (var attribute in validateAttributes)
            {
                (attribute as IUserValidator)?.Validate(user);
            }
        }
    }
}
