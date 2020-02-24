using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineServices.Common.RegistrationServices.Enumerations;
using OnlineServices.Common.RegistrationServices.Interfaces;
using OnlineServices.Common.RegistrationServices.TransferObject;
using RegistrationServices.BusinessLayer.UseCase.Attendee;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrationServices.BusinessLayerTests.UseCases.AttendeeTests
{
    [TestClass]
    public class Attendee_GetSessionByUserByDateTests
    {
        [TestMethod]
        public void GetSessionByUserByDate_Successful()
        {
            var user = new UserTO { Company = "CaBossDur", Email = "machin@bidule.ouaip", Id = 3, IsActivated = true, Name = "Marcel", Role = UserRole.Attendee, };
            var user2 = new UserTO { Company = "CaBossDur2", Email = "truc@bidule.ouaip", Id = 2, IsActivated = true, Name = "Jean-Luc", Role = UserRole.Attendee, };
            var user3 = new UserTO { Company = "CaBossDur3", Email = "poulette@bidule.ouaip", Id = 1, IsActivated = true, Name = "Clara", Role = UserRole.Attendee, };

            var sessionDay = new SessionDayTO { Id = 1, Date = new DateTime(20 / 02 / 2020), PresenceType = SessionPresenceType.AfternoonOnly };
            var sessionDay2 = new SessionDayTO { Id = 2, Date = new DateTime(21 / 02 / 2020), PresenceType = SessionPresenceType.MorningAfternoon };
            var sessionDay3 = new SessionDayTO { Id = 3, Date = new DateTime(22 / 02 / 2020), PresenceType = SessionPresenceType.OnceADay };

            var sessions = new List<SessionTO> 
            {
               new SessionTO(){Attendees = new List<UserTO>(){user, user2}, Id = 1},
               new SessionTO(){},
               new SessionTO(){},
            };


            var mockUnitOfWork = new Mock<IRSUnitOfWork>();
            mockUnitOfWork.Setup(u => u.UserRepository.GetById(It.IsAny<int>())).Returns(user);
            mockUnitOfWork.Setup(u => u.SessionRepository.GetByUser(It.IsAny<UserTO>())).Returns(sessions);
            var sut = new RSAttendeeRole(mockUnitOfWork.Object);


            //ACT
            //var result = sut.GetIdByMail("machin@bidule.ouaip");

            ////ASSERT
            //mockUnitOfWork.Verify(u => u.UserRepository.GetAll(), Times.Once);
            //Assert.AreEqual(3, result);
        }
    }
}
