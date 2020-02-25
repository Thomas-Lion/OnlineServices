using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineServices.Common.Exceptions;
using OnlineServices.Common.RegistrationServices.Enumerations;
using OnlineServices.Common.RegistrationServices.Interfaces;
using OnlineServices.Common.RegistrationServices.TransferObject;
using RegistrationServices.BusinessLayer.UseCase.Attendee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegistrationServices.BusinessLayerTests.UseCases.AttendeeTests
{
    [TestClass]
    public class Attendee_GetSessionsByDateTests
    {
        [TestMethod]
        public void GetSessionsByDate_Successful()
        {
            var user = new UserTO { Company = "CaBossDur", Email = "machin@bidule.ouaip", Id = 3, IsArchived = false, Name = "Marcel", Role = UserRole.Attendee, };
            var user2 = new UserTO { Company = "CaBossDur2", Email = "truc@bidule.ouaip", Id = 2, IsArchived = false, Name = "Jean-Luc", Role = UserRole.Attendee, };
            var user3 = new UserTO { Company = "CaBossDur3", Email = "poulette@bidule.ouaip", Id = 1, IsArchived = false, Name = "Clara", Role = UserRole.Attendee, };

            var users = new List<UserTO>() { user, user2, user3 };

            var sessionDay = new SessionDayTO { Id = 1, Date = new DateTime(2020, 02, 20), PresenceType = SessionPresenceType.AfternoonOnly };
            var sessionDay2 = new SessionDayTO { Id = 2, Date = new DateTime(2020, 02, 21), PresenceType = SessionPresenceType.MorningAfternoon };
            var sessionDay3 = new SessionDayTO { Id = 3, Date = new DateTime(2020, 02, 22), PresenceType = SessionPresenceType.OnceADay };

            var sessions = new List<SessionTO>
            {
               new SessionTO(){Attendees = new List<UserTO>(){user, user2}, Id = 1,SessionDays= new List<SessionDayTO>(){sessionDay, sessionDay2}},
               new SessionTO(){Attendees = new List<UserTO>() {user2}, Id=2,SessionDays= new List<SessionDayTO>(){sessionDay3, sessionDay2} },
               new SessionTO(){Attendees = new List<UserTO>() {user2, user3}, Id=3, SessionDays= new List<SessionDayTO>(){sessionDay, sessionDay3} },
            };

            var mockUnitOfWork = new Mock<IRSUnitOfWork>();
            mockUnitOfWork.Setup(u => u.SessionRepository.GetSessionsByDate(It.IsAny<DateTime>())).Returns(sessions);
            var sut = new RSAttendeeRole(mockUnitOfWork.Object);

            //ACT
            var result = sut.GetSessionsByDate(new DateTime(2020, 02, 20));

            //ASSERT
            mockUnitOfWork.Verify(u => u.SessionRepository.GetSessionsByDate(new DateTime(2020, 02, 20)), Times.Once);
        }

        [TestMethod]
        public void GetSessionsByDate_InvalidDateProvided_ThrowException()
        {
            var user = new UserTO { Company = "CaBossDur", Email = "machin@bidule.ouaip", Id = 3, IsArchived = false, Name = "Marcel", Role = UserRole.Attendee, };
            var user2 = new UserTO { Company = "CaBossDur2", Email = "truc@bidule.ouaip", Id = 2, IsArchived = false, Name = "Jean-Luc", Role = UserRole.Attendee, };
            var user3 = new UserTO { Company = "CaBossDur3", Email = "poulette@bidule.ouaip", Id = 1, IsArchived = false, Name = "Clara", Role = UserRole.Attendee, };

            var users = new List<UserTO>() { user, user2, user3 };

            var sessionDay = new SessionDayTO { Id = 1, Date = new DateTime(2020, 02, 20), PresenceType = SessionPresenceType.AfternoonOnly };
            var sessionDay2 = new SessionDayTO { Id = 2, Date = new DateTime(2020, 02, 21), PresenceType = SessionPresenceType.MorningAfternoon };
            var sessionDay3 = new SessionDayTO { Id = 3, Date = new DateTime(2020, 02, 22), PresenceType = SessionPresenceType.OnceADay };

            var sessions = new List<SessionTO>
            {
               new SessionTO(){Attendees = new List<UserTO>(){user, user2}, Id = 1,SessionDays= new List<SessionDayTO>(){sessionDay, sessionDay2}},
               new SessionTO(){Attendees = new List<UserTO>() {user2}, Id=2,SessionDays= new List<SessionDayTO>(){sessionDay3, sessionDay2} },
               new SessionTO(){Attendees = new List<UserTO>() {user2, user3}, Id=3, SessionDays= new List<SessionDayTO>(){sessionDay, sessionDay3} },
            };

            var mockUnitOfWork = new Mock<IRSUnitOfWork>();
            mockUnitOfWork.Setup(u => u.SessionRepository.GetSessionsByDate(It.IsAny<DateTime>())).Returns(sessions);
            var sut = new RSAttendeeRole(mockUnitOfWork.Object);

            //ACT
            //ASSERT
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => sut.GetSessionsByDate(new DateTime(30, 30, 30)));
        }

        [TestMethod]
        public void GetSessionsByDate_NotExistingDateInDB_ThrowException()
        {
            var user = new UserTO { Company = "CaBossDur", Email = "machin@bidule.ouaip", Id = 3, IsArchived = false, Name = "Marcel", Role = UserRole.Attendee, };
            var user2 = new UserTO { Company = "CaBossDur2", Email = "truc@bidule.ouaip", Id = 2, IsArchived = false, Name = "Jean-Luc", Role = UserRole.Attendee, };
            var user3 = new UserTO { Company = "CaBossDur3", Email = "poulette@bidule.ouaip", Id = 1, IsArchived = false, Name = "Clara", Role = UserRole.Attendee, };

            var users = new List<UserTO>() { user, user2, user3 };

            var sessionDay = new SessionDayTO { Id = 1, Date = new DateTime(2020, 02, 20), PresenceType = SessionPresenceType.AfternoonOnly };
            var sessionDay2 = new SessionDayTO { Id = 2, Date = new DateTime(2020, 02, 21), PresenceType = SessionPresenceType.MorningAfternoon };
            var sessionDay3 = new SessionDayTO { Id = 3, Date = new DateTime(2020, 02, 22), PresenceType = SessionPresenceType.OnceADay };

            var sessions = new List<SessionTO>
            {
               new SessionTO(){Attendees = new List<UserTO>(){user, user2}, Id = 1,SessionDays= new List<SessionDayTO>(){sessionDay, sessionDay2}},
               new SessionTO(){Attendees = new List<UserTO>() {user2}, Id=2,SessionDays= new List<SessionDayTO>(){sessionDay3, sessionDay2} },
               new SessionTO(){Attendees = new List<UserTO>() {user2, user3}, Id=3, SessionDays= new List<SessionDayTO>(){sessionDay, sessionDay3} },
            };

            var mockUnitOfWork = new Mock<IRSUnitOfWork>();
            var sut = new RSAttendeeRole(mockUnitOfWork.Object);

            //ACT
            //ASSERT
            Assert.ThrowsException<LoggedException>(() => sut.GetSessionsByDate(new DateTime(2021, 03, 02)));
        }
    }
}