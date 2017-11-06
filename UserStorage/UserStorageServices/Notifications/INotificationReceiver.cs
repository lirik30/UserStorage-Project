﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Notifications
{
    public interface INotificationReceiver
    {
        void Receive(NotificationContainer container);
    }
}
