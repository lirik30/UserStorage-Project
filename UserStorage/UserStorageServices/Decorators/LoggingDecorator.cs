using System;
using System.Collections.Generic;
using System.Diagnostics;
using UserStorageServices.Services;

namespace UserStorageServices.Decorators
{
    public class LoggingDecorator : UserStorageServiceDecorator
    {
        private readonly BooleanSwitch _loggingSwitch = new BooleanSwitch("enableLogging", "Switch for enable/disable logging");

        public LoggingDecorator(IUserStorageService wrappee = null) : base(wrappee)
        {
        }

        public override int Count
        {
            get
            {
                if (_loggingSwitch.Enabled)
                    Trace.TraceInformation("Count property is called.");

                return base.Count;
            }
        }

        public override void Add(User user)
        {
            if (_loggingSwitch.Enabled)
                Trace.TraceInformation("Add() method is called.");

            base.Add(user);
        }

        public override void Remove(User user)
        {
            if (_loggingSwitch.Enabled)
                Trace.TraceInformation("Remove() method is called.");

            base.Remove(user);
        }

        public override IEnumerable<User> Search(Func<User, bool> predicate)
        {
            if (_loggingSwitch.Enabled)
                Trace.TraceInformation("Search() method is called.");

            return base.Search(predicate);
        }
    }
}
