using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorageServices.Validation_exceptions;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserStorageServiceTests
    {
        // Add tests
        [TestMethod]
        [ExpectedException(typeof(UserIsNullException))]
        public void Add_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(FirstNameIsNullOrEmptyException))]
        public void Add_UserFirstNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

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
        [ExpectedException(typeof(LastNameIsNullOrEmptyException))]
        public void Add_UserLastNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

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
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "name",
                LastName = "name",
                Age = -10
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Add_SomeUser_SuccessfulAddition()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            //Act
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
            Assert.AreEqual(3, userStorageService.Count);
        }

        // Remove tests
        [TestMethod]
        [ExpectedException(typeof(UserIsNullException))]
        public void Remove_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Remove(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Remove_UserDoesNotExist_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Remove(new User());

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Remove_SevaralUsersWithTheSameData_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();
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
            var userStorageService = new UserStorageService();
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

        //Search tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Search_PredicateIsNull_ExceptionThrows()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Search(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Search_LastNameIsNull_ZeroCollection()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            var result = userStorageService.Search(u => u.LastName == null);

            // Assert - 0
            Assert.AreEqual(result.Count(), 0);
        }

        [TestMethod]
        public void Search_AgeIsLessThanZeroFirstNameIsArbitrary_ZeroCollection()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            var result = userStorageService.Search(u => u.Age == -15 && u.FirstName == "Somename");

            // Assert - 0
            Assert.AreEqual(result.Count(), 0);
        }

        [TestMethod]
        public void Search_AgeIs25_TwoElements()
        {
            // Arrange
            var userStorageService = new UserStorageService();
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
            Assert.AreEqual(result.Count(), 2);
        }
    }
}