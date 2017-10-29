using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class AgeValidator : IUserValidator
    {
        public void Validate(User user)
        {
            if (user.Age < 0)
            {
                throw new ArgumentException("Age is less than zero", nameof(user));
            }
        }
    }
}
