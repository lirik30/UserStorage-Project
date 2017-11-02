using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorageServices;

namespace UserRepository.Tests
{
    [TestClass]
    public class MemoryWithStateTests
    {
        [TestMethod]
        public void LoadRepositoryState_RepositoryBin_PositiveRes()
        {
            IUserRepository repository = new UserMemoryCacheWithState();
            repository.Start();
        }

        [TestMethod]
        public void SaveRepositoryState_RepositoryBin_PossibleRes()
        {
            IUserRepository repository = new UserMemoryCacheWithState();
            repository.Stop();
        }
    }
}
