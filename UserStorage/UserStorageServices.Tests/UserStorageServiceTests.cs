using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorageServices.Notifications;
using UserStorageServices.Repositories;
using UserStorageServices.Services;
using UserStorageServices.Validation_exceptions;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserStorageServiceTests
    {
        // Add tests
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Add_AdditionInSlaveNode_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceSlave();

            // Act
            userStorageService.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Add_AdditionInMasterNode_OneElementInSlaveNode()
        {
            // Arrange
            var repository = new UserMemoryCacheWithState();
            var receiver = new NotificationReceiver();
            var sender = new NotificationSender(receiver);

            var slave1 = new UserStorageServiceSlave(receiver);
            var slave2 = new UserStorageServiceSlave(receiver);
            var userStorageService = new UserStorageServiceMaster(userRepository: repository, sender: sender);

            // Act
            userStorageService.Add(new User
            {
                FirstName = "A",
                LastName = "B",
                Age = 10
            });
            
            // Assert - slave nodes user count must increase to 1 user
            Assert.AreEqual(repository.Count, slave1.Count);
            Assert.AreEqual(repository.Count, slave2.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(UserIsNullException))]
        public void Add_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(FirstNameIsNullOrEmptyException))]
        public void Add_UserFirstNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Add(new User
            {
                FirstName = null,
                LastName = "name",
                Age = 1
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(FirstNameWrongFormatException))]
        public void Add_UserFirstNameWrongFormat_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "111",
                LastName = "name",
                Age = 1
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(LastNameWrongFormatException))]
        public void Add_UserLastNameWrongFormat_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "name",
                LastName = "$24562@#%34",
                Age = 1
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(LastNameIsNullOrEmptyException))]
        public void Add_UserLastNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Add(new User
            {
                LastName = null,
                FirstName = "name",
                Age = 1
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(AgeExceedsLimitException))]
        public void Add_UserAgeIsLessThanZero_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "name",
                LastName = "Name",
                Age = -10
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Add_SomeUser_SuccessfulAddition()
        {
            // Arrange
            var repository = new UserMemoryCacheWithState();
            var userStorageService = new UserStorageServiceMaster(userRepository: repository);

            // Act
            userStorageService.Add(new User
            {
                FirstName = "Pavel",
                LastName = "Pavlov",
                Age = 25
            });

            userStorageService.Add(new User
            {
                FirstName = "Andrey",
                LastName = "Andreev",
                Age = 40
            });

            userStorageService.Add(new User
            {
                FirstName = "Pavel",
                LastName = "Pavlov",
                Age = 25
            });

            // Assert - Collection length should increase to 3 members
            Assert.AreEqual(repository.Count, userStorageService.Count);
        }

        // Remove tests
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Remove_RemovalInSlaveNode_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceSlave();

            // Act
            userStorageService.Remove(new User());

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Remove_RemovalInMasterNode_OneElementInSlaveNode()
        {
            // Arrange
            IUserRepository repository = new UserMemoryCache();

            var slave1 = new UserStorageServiceSlave(userRepository: repository);
            var slave2 = new UserStorageServiceSlave(userRepository: repository);
            var userStorageService = new UserStorageServiceMaster(userRepository: repository);
            userStorageService.Add(new User
            {
                FirstName = "Pavel",
                LastName = "Pavlov",
                Age = 25
            });

            userStorageService.Add(new User
            {
                FirstName = "Andrey",
                LastName = "Andreev",
                Age = 40
            });

            userStorageService.Add(new User
            {
                FirstName = "Pavel",
                LastName = "Pavlov",
                Age = 30
            });

            // Act
            userStorageService.Remove(new User
            {
                FirstName = "Pavel",
                LastName = "Pavlov",
                Age = 25
            });

            userStorageService.Remove(new User
            {
                FirstName = "Pavel",
                LastName = "Pavlov",
                Age = 30
            });

            // Assert - slave nodes user count must increase to 1 user
            Assert.AreEqual(1, slave1.Count);
            Assert.AreEqual(1, slave2.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(UserIsNullException))]
        public void Remove_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Remove(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Remove_UserDoesNotExist_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();

            // Act
            userStorageService.Remove(new User());

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Remove_SevaralUsersWithTheSameData_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();
            userStorageService.Add(new User
            {
                FirstName = "Pavel",
                LastName = "Pavlov",
                Age = 25
            });

            userStorageService.Add(new User
            {
                FirstName = "Andrey",
                LastName = "Andreev",
                Age = 40
            });

            userStorageService.Add(new User
            {
                FirstName = "Pavel",
                LastName = "Pavlov",
                Age = 25
            });

            // Act
            userStorageService.Remove(new User
            {
                FirstName = "Pavel",
                LastName = "Pavlov",
                Age = 25
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Remove_SomeUsers_SuccessfulRemoval()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();
            userStorageService.Add(new User
            {
                FirstName = "Pavel",
                LastName = "Pavlov",
                Age = 25
            });

            userStorageService.Add(new User
            {
                FirstName = "Andrey",
                LastName = "Andreev",
                Age = 40
            });

            userStorageService.Add(new User
            {
                FirstName = "Pavel",
                LastName = "Pavlov",
                Age = 25
            });

            // Act
            userStorageService.Remove(new User
            {
                FirstName = "Andrey",
                LastName = "Andreev",
                Age = 40
            });

            // Assert - Collection length should decrease to 2 members
            Assert.AreEqual(2, userStorageService.Count);
        }

        // Search tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Search_PredicateIsNull_ExceptionThrows()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();
            userStorageService.Add(new User
            {
                FirstName = "Pavel",
                LastName = "Pavlov",
                Age = 25
            });

            userStorageService.Add(new User
            {
                FirstName = "Andrey",
                LastName = "Andreev",
                Age = 40
            });

            userStorageService.Add(new User
            {
                FirstName = "Nikita",
                LastName = "Nikitin",
                Age = 25
            });

            // Act
            userStorageService.Search(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Search_LastNameIsNull_ZeroCollection()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();
            userStorageService.Add(new User
            {
                FirstName = "Pavel",
                LastName = "Pavlov",
                Age = 25
            });

            userStorageService.Add(new User
            {
                FirstName = "Andrey",
                LastName = "Andreev",
                Age = 40
            });

            userStorageService.Add(new User
            {
                FirstName = "Nikita",
                LastName = "Nikitin",
                Age = 25
            });

            // Act
            var result = userStorageService.Search(u => u.LastName == null);

            // Assert - 0
            Assert.AreEqual(result.Count(), 0);
        }

        [TestMethod]
        public void Search_AgeIsLessThanZeroFirstNameIsArbitrary_ZeroCollection()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster();
            userStorageService.Add(new User
            {
                FirstName = "Pavel",
                LastName = "Pavlov",
                Age = 25
            });

            userStorageService.Add(new User
            {
                FirstName = "Andrey",
                LastName = "Andreev",
                Age = 40
            });

            userStorageService.Add(new User
            {
                FirstName = "Nikita",
                LastName = "Nikitin",
                Age = 25
            });

            // Act
            var result = userStorageService.Search(u => u.Age == -15 && u.FirstName == "Somename");

            // Assert - 0
            Assert.AreEqual(result.Count(), 0);
        }

        [TestMethod]
        public void Search_AgeIs25_TwoElements()
        {
            // Arrange
            var repository = new UserMemoryCacheWithState();
            var userStorageService = new UserStorageServiceMaster(userRepository: repository);
            userStorageService.Add(new User
            {
                FirstName = "Pavel",
                LastName = "Pavlov",
                Age = 25
            });

            userStorageService.Add(new User
            {
                FirstName = "Andrey",
                LastName = "Andreev",
                Age = 40
            });

            userStorageService.Add(new User
            {
                FirstName = "Nikita",
                LastName = "Nikitin",
                Age = 25
            });

            // Act
            var result = userStorageService.Search(u => u.Age == 25);

            // Assert - 0
            Assert.AreEqual(result.Count(), repository.Search(u => u.Age == 25).Count());
        }
    }
}