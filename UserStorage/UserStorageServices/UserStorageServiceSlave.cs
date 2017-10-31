using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserStorageServiceSlave : UserStorageServiceBase, ISubscriber
    {
        public UserStorageServiceSlave(IGenerateIdentifier identifier = null,
            IUserValidator validator = null) : base(StorageMode.SlaveNode, identifier, validator) { }

        public override StorageMode StorageMode => _storageMode;

        public override void Add(User user)
        {
            if (IsCallFromMaster())
            {
                base.Add(user);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public override void Remove(User user)
        {
            if (IsCallFromMaster())
            {
                base.Remove(user);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void UserAdded(object sender, StorageChangeEventArgs eventArgs)
        {
            Add(eventArgs.User);
        }

        public void UserRemoved(object sender, StorageChangeEventArgs eventArgs)
        {
            Remove(eventArgs.User);
        }

        private bool IsCallFromMaster()
        {
            var stTrace = new StackTrace();
            var callFrom = stTrace.GetFrame(1).GetMethod();
            var suchMethodInMaster = typeof(UserStorageServiceMaster).GetMethod(callFrom.Name);
            return stTrace.GetFrames()?.Select(x => x.GetMethod())
                .Contains(suchMethodInMaster) ?? false;
        }
    }
}
