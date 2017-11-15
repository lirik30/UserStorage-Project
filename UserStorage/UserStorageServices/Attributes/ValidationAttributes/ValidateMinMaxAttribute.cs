using System;
using UserStorageServices.Validation_exceptions;
using UserStorageServices.Validators;

namespace UserStorageServices.Attributes.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateMinMaxAttribute : Attribute, IUserValidator
    {
        public int MinLimit { get; }
        public int MaxLimit { get; }

        public ValidateMinMaxAttribute(int minLimit, int maxLimit)
        {
            MinLimit = minLimit;
            MaxLimit = maxLimit;
        }

        public void Validate(User user)
        {
            if (user.Age < MinLimit || user.Age > MaxLimit)
                throw new AgeExceedsLimitException($"Age of user must be greater than {MinLimit} and less than {MaxLimit}");
        }
    }
}
