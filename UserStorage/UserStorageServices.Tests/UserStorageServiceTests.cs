using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserStorageServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidGenerate(), new UserValidator());

            // Act
            userStorageService.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserFirstNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidGenerate(), new UserValidator());

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
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserLastNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidGenerate(), new UserValidator());

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
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserAgeIsLessThanZero_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidGenerate(), new UserValidator());

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
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidGenerate(), new UserValidator());

            // Act
            userStorageService.Remove(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Remove_UserDoesNotExist_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidGenerate(), new UserValidator());

            // Act
            userStorageService.Remove(new User());

            // Assert - [ExpectedException]
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Search_FirstNameIsNull_ExceptionThrows()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidGenerate(), new UserValidator());

            // Act
            userStorageService.SearchByFirstName(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Search_LastNameIsNull_ExceptionThrows()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidGenerate(), new UserValidator());

            // Act
            userStorageService.SearchByLastName(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Search_AgeIsLessThanZero_ExceptionThrows()
        {
            // Arrange
            var userStorageService = new UserStorageService(new GuidGenerate(), new UserValidator());

            // Act
            userStorageService.SearchByAge(-15);

            // Assert - [ExpectedException]
        }
    }
}