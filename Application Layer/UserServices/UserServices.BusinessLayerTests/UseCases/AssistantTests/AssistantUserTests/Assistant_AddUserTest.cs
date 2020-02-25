﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using RegistrationServices.BusinessLayer.UseCase;
using Moq;
using RegistrationServices.BusinessLayer;
using OnlineServices.Common.RegistrationServices.Interfaces;
using OnlineServices.Common.RegistrationServices.TransferObject;
using OnlineServices.Common.Exceptions;
using RegistrationServices.BusinessLayer.UseCase.Assistant;

namespace RegistrationServices.BusinessLayerTests.UseCase.AssistantUserTests
{
    [TestClass]
    public class Assistant_AddUserTest
    {
        private Mock<IRSUnitOfWork> MockUofW = new Mock<IRSUnitOfWork>();
        private Mock<IRSUserRepository> MockUserRepository = new Mock<IRSUserRepository>();

        [TestMethod]
        public void AddUser_ThrowException_WhenUserIDisDiferentThanZero()
        {
            //ARRANGE
            var assistant = new RSAssistantRole(new Mock<IRSUnitOfWork>().Object);
            var userToAdd = new UserTO { Id = 1, Name = "User", IsArchived = false, Company = "Company1", Role = UserRole.Assistant, Email = "user@gmail.com" };

            //ASSERT
            Assert.ThrowsException<Exception>(() => assistant.AddUser(userToAdd));
        }

        [TestMethod]
        public void AddUser_ThrowIsNullOrWhiteSpaceException_WhenUserNameIsAnEmptyString()
        {
            //ARRANGE
            var userNameWhiteSpace = new UserTO { Id = 0, Name = "" };
            var userNameNull = new UserTO { Id = 0, Name = null };

            var mockUofW = new Mock<IRSUnitOfWork>();
            var assistant = new RSAssistantRole(mockUofW.Object);

            //ASSERT
            Assert.ThrowsException<IsNullOrWhiteSpaceException>(() => assistant.AddUser(userNameWhiteSpace));
            Assert.ThrowsException<IsNullOrWhiteSpaceException>(() => assistant.AddUser(userNameNull));
        }

        [TestMethod]
        public void AddUser_ThrowException_WhenLoggedUserIsNull()
        {
            //ARRANGE
            var assistant = new RSAssistantRole(MockUofW.Object);

            //ASSERT
            Assert.ThrowsException<LoggedException>(() => assistant.AddUser(null));
        }

        [TestMethod]
        public void AddUser_NewUser_Test()
        {
            //ARRANGE
            var newUser = new UserTO { Id = 0, Name = "Enrique", IsArchived = false, Company = "Company 01", Role = UserRole.Assistant, Email = "user@gmail.com" };

            MockUserRepository.Setup(x => x.Add(It.IsAny<UserTO>())); //.Returns(newUser);
            var mockUofW = new Mock<IRSUnitOfWork>();
            mockUofW.Setup(x => x.UserRepository).Returns(MockUserRepository.Object);

            var assistant = new RSAssistantRole(mockUofW.Object);

            //ASSERT
            Assert.IsTrue(assistant.AddUser(newUser));
        }

        [TestMethod]
        public void AddUser_UserRepositoryIsCalledOnce_WhenAValidUserIsProvidedAndAddInDB()
        {
            //ARRANGE
            MockUserRepository.Setup(x => x.Add(It.IsAny<UserTO>()));
            MockUofW.Setup(x => x.UserRepository).Returns(MockUserRepository.Object);

            var ass = new RSAssistantRole(MockUofW.Object);
            var newUser = new UserTO { Id = 0, Name = "New User" };

            //ACT
            ass.AddUser(newUser);
            MockUserRepository.Verify(x => x.Add(It.IsAny<UserTO>()), Times.Once);
        }
    }
}