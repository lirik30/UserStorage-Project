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
        [ExpectedException(typeof(FileNotFoundException))]
        public void LoadRepositoryState_RepositorBin_ExceptionThrown()
        {
            IUserRepository repository = new UserMemoryCacheWithState();
            repository.Start("repositor.bin");
        }

        [TestMethod]
        public void LoadRepositoryState_RepositoryBin_PositiveRes()
        {
            IUserRepository repository = new UserMemoryCacheWithState();
            repository.Start("repository.bin");
        }

        [TestMethod]
        public void SaveRepositoryState_RepositoryBin_PossibleRes()
        {
            IUserRepository repository = new UserMemoryCacheWithState();
            repository.Stop("repository.bin");
        }
    }
}
