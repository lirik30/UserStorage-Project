using System;

namespace UserStorageServices.Attributes.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateRegexAttribute : Attribute
    {
        public string RegexString { get; }

        public ValidateRegexAttribute(string regexString)
        {
            RegexString = regexString;
        }
    }
}
