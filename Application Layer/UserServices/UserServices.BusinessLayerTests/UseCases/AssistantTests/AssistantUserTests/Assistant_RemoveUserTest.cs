﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using RegistrationServices.BusinessLayer.UseCase;
using Moq;
using RegistrationServices.BusinessLayer;
using OnlineServices.Common.RegistrationServices.Interfaces;
using OnlineServices.Common.RegistrationServices.TransferObject;
using RegistrationServices.BusinessLayer.UseCase.Assistant;

namespace RegistrationServices.BusinessLayerTests.UseCase.AssistantUserTests
{
    [TestClass]
    public class Assistant_RemoveUserTest
    {
        private Mock<IRSUnitOfWork> MockUofW = new Mock<IRSUnitOfWork>();
        private Mock<IRSUserRepository> MockUserRepository = new Mock<IRSUserRepository>();

        [TestMethod]
        public void RemoveUser_ThrowException_WhenUserIsNull()
        {
            //ARRANGE
            var assistant = new RSAssistantRole(MockUofW.Object);

            //ASSERT
            Assert.ThrowsException<ArgumentNullException>(() => assistant.RemoveUser(null));
        }

        [TestMethod]
        public void RemoveUser_ThrowException_WhenUserIdIsZero()
        {
            //ARRANGE
            var userIdZero = new UserTO { Id = 0, Name = "User Name" };
            var assistant = new RSAssistantRole(MockUofW.Object);

            //ASSERT
            Assert.ThrowsException<Exception>(() => assistant.RemoveUser(userIdZero));
        }

        [TestMethod]
        public void RemoveUser_ReturnsTrue_WhenUserIsProvidedAndRemovedFromDB_Test()
        {
            //ARRANGE
            MockUserRepository.Setup(x => x.Remove(It.IsAny<UserTO>()));
            MockUofW.Setup(x => x.UserRepository).Returns(MockUserRepository.Object);

            var assistant = new RSAssistantRole(MockUofW.Object);
            var userToRemove = new UserTO { Id = 1, Name = "User Name", IsArchived = false };

            //ASSERT
            Assert.IsTrue(assistant.RemoveUser(userToRemove));
        }

        [TestMethod]
        public void RemoveUser_UserRepositoryIsCalledOnce_WhenAValidUserIsProvidedAndRemovedFromDB()
        {
            //ARRANGE
            MockUserRepository.Setup(x => x.Remove(It.IsAny<UserTO>()));
            MockUofW.Setup(x => x.UserRepository).Returns(MockUserRepository.Object);

            var ass = new RSAssistantRole(MockUofW.Object);
            var userToRemoveOnce = new UserTO { Id = 1, Name = "User Name" };

            //ACT
            ass.RemoveUser(userToRemoveOnce);
            MockUserRepository.Verify(x => x.Remove(It.IsAny<UserTO>()), Times.Once);
        }
    }
}