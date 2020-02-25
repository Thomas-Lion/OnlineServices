using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineServices.Common.Exceptions;
using OnlineServices.Common.FacilityServices.Interfaces;
using OnlineServices.Common.RegistrationServices.Interfaces;
using OnlineServices.Common.RegistrationServices.TransferObject;
using RegistrationServices.BusinessLayer.UseCase.Attendee;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrationServices.BusinessLayerTests.UseCases.AttendeeTests
{
    [TestClass]
    public class Attendee_GetIdByMailTests
    {
        [TestMethod]
        public void GetIdByMail_Successful()
        {
            var users = new List<UserTO>
            {
                new UserTO {Company = "CaBossDur", Email="machin@bidule.ouaip",Id=3,IsArchived=false,Name="Marcel",Role=UserRole.Attendee,},
                new UserTO {Company = "CaBossDur2", Email="mec@bidule.ouaip",Id=4,IsArchived=false,Name="Jean-Louis",Role=UserRole.Attendee,},
                new UserTO {Company = "CaBossDur3", Email="meuf@bidule.ouaip",Id=2,IsArchived=false,Name="Clara",Role=UserRole.Attendee,}
            };
            var mockUnitOfWork = new Mock<IRSUnitOfWork>();
            mockUnitOfWork.Setup(u => u.UserRepository.GetAll()).Returns(users);
            var sut = new RSAttendeeRole(mockUnitOfWork.Object);

            //ACT
            var result = sut.GetIdByMail("machin@bidule.ouaip");

            //ASSERT
            mockUnitOfWork.Verify(u => u.UserRepository.GetAll(), Times.Once);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void GetIdByMail_GiveIncorrectMail_ThrowException()
        {
            var users = new List<UserTO>
            {
                new UserTO {Company = "EvilCorp", Email="machin@bidule.ouaip",Id=3,IsArchived=false,Name="Marcel",Role=UserRole.Attendee,},
                new UserTO {Company = "GoodCorp", Email="mec@bidule.ouaip",Id=4,IsArchived=false,Name="Jean-Louis",Role=UserRole.Attendee,},
                new UserTO {Company = "BofCorp", Email="meuf@bidule.ouaip",Id=2,IsArchived=false,Name="Clara",Role=UserRole.Attendee,}
            };
            var mockUnitOfWork = new Mock<IRSUnitOfWork>();
            mockUnitOfWork.Setup(u => u.UserRepository.GetAll()).Returns(users);
            var sut = new RSAttendeeRole(mockUnitOfWork.Object);

            //ASSERT

            Assert.ThrowsException<LoggedException>(() => sut.GetIdByMail("machin@bidule.ouai"));
            Assert.ThrowsException<LoggedException>(() => sut.GetIdByMail(" "));
            Assert.ThrowsException<LoggedException>(() => sut.GetIdByMail(null));
        }
    }
}