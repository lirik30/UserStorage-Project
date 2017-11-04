using System;
using System.Diagnostics;
using System.Linq;

namespace UserStorageServices
{
    public class UserStorageServiceSlave : UserStorageServiceBase, ISubscriber
    {
        public UserStorageServiceSlave(
            IUserValidator validator = null,
            IUserRepository userRepository = null) : base(validator, userRepository)
        {  
        }

        public override StorageMode StorageMode => StorageMode.SlaveNode;

        public override void Add(User user)
        {
            if (IsCallFromMaster())
                Repository.Add(user);

            else
                throw new NotSupportedException();
        }

        public override void Remove(User user)
        {
            if (IsCallFromMaster())
                Repository.Remove(user);

            else
                throw new NotSupportedException();
        }

        public void UserAdded(object sender, StorageChangeEventArgs eventArgs) => Add(eventArgs.User);

        public void UserRemoved(object sender, StorageChangeEventArgs eventArgs) => Remove(eventArgs.User);

        private bool IsCallFromMaster()
        {
            var stackTrace = new StackTrace();
            var callFrom = stackTrace.GetFrame(1).GetMethod();
            var suchMethodInMaster = typeof(UserStorageServiceMaster).GetMethod(callFrom.Name);
            return stackTrace.GetFrames()?.Select(x => x.GetMethod())
                .Contains(suchMethodInMaster) ?? false;
        }
    }
}
