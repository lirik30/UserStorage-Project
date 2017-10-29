using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class LoggingDecorator : UserStorageServiceDecorator
    {
        public LoggingDecorator(IUserStorageService wrappee = null) : base(wrappee) { }
        private readonly BooleanSwitch _loggingSwitch = new BooleanSwitch("enableLogging", "Switch for enable/disable logging");

        public override int Count
        {
            get
            {
                if (_loggingSwitch.Enabled)
                {
                    Console.WriteLine("Count property is called.");
                }
                return base.Count;
            }
        }

        public override void Add(User user)
        {
            if (_loggingSwitch.Enabled)
            {
                Console.WriteLine("Add() method is called.");
            }
            base.Add(user);
        }

        public override void Remove(User user)
        {
            if (_loggingSwitch.Enabled)
            {
                Console.WriteLine("Remove() method is called.");
            }
            base.Remove(user);
        }

        public override IEnumerable<User> Search(Func<User, bool> predicate)
        {
            if (_loggingSwitch.Enabled)
            {
                Console.WriteLine("Search() method is called.");
            }
            return base.Search(predicate);
        }
    }
}
