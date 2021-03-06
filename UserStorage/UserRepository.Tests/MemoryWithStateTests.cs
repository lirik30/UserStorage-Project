﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorageServices.Repositories;
using UserStorageServices.Serializers;

namespace UserRepository.Tests
{
    [TestClass]
    public class MemoryWithStateTests
    {
        [TestMethod]
        public void LoadRepositoryState_RepositoryBin_PositiveRes()
        {
            IUserRepositoryManager repository = new UserMemoryCacheWithState(new BinaryUserSerializer());
            repository.Start();
        }

        [TestMethod]
        public void SaveRepositoryState_RepositoryBin_PossibleRes()
        {
            IUserRepositoryManager repository = new UserMemoryCacheWithState(new BinaryUserSerializer());
            repository.Stop();
        }

        [TestMethod]
        public void LoadRepositoryState_RepositoryXml_PositiveRes()
        {
            IUserRepositoryManager repository = new UserMemoryCacheWithState(new XmlUserSerializer());
            repository.Start();
        }

        [TestMethod]
        public void SaveRepositoryState_RepositoryXml_PossibleRes()
        {
            IUserRepositoryManager repository = new UserMemoryCacheWithState(new XmlUserSerializer());
            repository.Stop();
        }
    }
}
