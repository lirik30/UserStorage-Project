﻿using UserStorageServices.Validation_exceptions;

namespace UserStorageServices.Validators
{
    public class FirstNameValidator : IUserValidator
    {
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName))
                throw new FirstNameIsNullOrEmptyException("FirstName is null or empty or whitespace");

            if (user.FirstName.Length > 50)
                throw new FirstNameExceedsLimitException("First name of user must be less than 50 symbols");
        }
    }
}