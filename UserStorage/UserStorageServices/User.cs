using System;
using UserStorageServices.Attributes.ValidationAttributes;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        /// Unique identifier of the user in the storage
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a user first name.
        /// </summary>
        [ValidateNotNullOrEmptyOrWhitespace]
        [ValidateMaxLength]
        [ValidateRegex("[a-zA-Z]+")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a user last name.
        /// </summary>
        [ValidateNotNullOrEmptyOrWhitespace]
        [ValidateMaxLength]
        [ValidateRegex("[a-zA-Z]+")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a user age.
        /// </summary>
        [ValidateMinMax(0, 100)]
        public int Age { get; set; }
    }
}
