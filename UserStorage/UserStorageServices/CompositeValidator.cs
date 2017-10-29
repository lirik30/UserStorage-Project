using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class CompositeValidator : IUserValidator
    {
        private IUserValidator[] _validators;

        public CompositeValidator(IUserValidator[] validators)
        {
            _validators = validators;
        }


        public void Validate(User user)
        {
            foreach (var validator in _validators)
            {
                validator.Validate(user);
            }
        }
    }
}
