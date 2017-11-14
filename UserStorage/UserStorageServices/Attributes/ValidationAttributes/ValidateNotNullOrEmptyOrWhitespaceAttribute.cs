using System;

namespace UserStorageServices.Attributes.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateNotNullOrEmptyOrWhitespaceAttribute : Attribute
    {
    }
}
