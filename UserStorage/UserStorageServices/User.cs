﻿using System;

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
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a user first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a user last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a user age.
        /// </summary>
        public int Age { get; set; }
    }
}
