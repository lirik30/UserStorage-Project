using System;

namespace UserStorageServices.Attributes.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateMinMaxAttribute : Attribute
    {
        public int MinLimit { get; }
        public int MaxLimit { get; }

        public ValidateMinMaxAttribute(int minLimit, int maxLimit)
        {
            MinLimit = minLimit;
            MaxLimit = maxLimit;
        }
    }
}
