using System;

namespace UserStorageServices.Attributes.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateMaxLengthAttribute : Attribute
    {
        public int MaxLength { get; }

        public ValidateMaxLengthAttribute(int maxLength = 50)
        {
            MaxLength = maxLength;
        }
    }
}
