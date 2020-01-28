﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineServices.Common.RegistrationServices.Enumerations;
using OnlineServices.Common.RegistrationServices.TransferObject;
using RegistrationServices.DataLayer.Entities;
using RegistrationServices.DataLayer.Extensions;
using System;
using System.Collections.Generic;

namespace RegistrationServices.DataLayerTests
{
    public partial class SessionExtensionsTests
    {
        [TestMethod]
        public void Should_Have_Same_Id_As_EF()
        {
            #region TOInitialization

            UserTO student = new UserTO()
            {
                Id = 1,
                Name = "Jacky Fringant",
                Email = "jacky@supermail.com",
                IsActivated = true,
                Role = UserRole.Attendee,
            };

            UserTO teacher = new UserTO()
            {
                Id = 2,
                Name = "Johnny Begood",
                Email = "johnny@yolomail.com",
                IsActivated = true,
                Role = UserRole.Teacher
            };

            CourseTO sql = new CourseTO()
            {
                Id = 1,
                Name = "SQL"
            };

            SessionTO sessionTO = new SessionTO()
            {
                Id = 1,
                Teacher = teacher,
                Course = sql,
                SessionDays = new List<SessionDayTO>()
                {
                   new SessionDayTO(){Id = 1, Date = new DateTime(2020, 2, 3), PresenceType = SessionPresenceType.MorningAfternoon},
                   new SessionDayTO(){Id = 2, Date = new DateTime(2020, 2, 4), PresenceType = SessionPresenceType.MorningAfternoon},
                   new SessionDayTO(){Id = 3, Date = new DateTime(2020, 2, 5), PresenceType = SessionPresenceType.MorningAfternoon}
                },

                Attendees = new List<UserTO>()
                {
                    student,
                }
            };

            #endregion TOInitialization

            #region EFInitialization

            UserEF studentEF = new UserEF()
            {
                Id = 1,
                Name = "Jacky Fringant",
                Email = "jacky@supermail.com",
                IsActivated = true,
                Role = UserRole.Attendee,
            };

            UserEF teacherEF = new UserEF()
            {
                Id = 2,
                Name = "Johnny Begood",
                Email = "johnny@yolomail.com",
                IsActivated = true,
                Role = UserRole.Teacher
            };

            CourseEF sqlEF = new CourseEF()
            {
                Id = 1,
                Name = "SQL"
            };

            SessionEF sessionEF = new SessionEF()
            {
                Id = 1,
                Course = sqlEF,
                Dates = new List<SessionDayEF>()
                {
                    new SessionDayEF { Id=1, Date=new DateTime(2020, 01, 20), PresenceType = SessionPresenceType.MorningOnly},
                    new SessionDayEF { Id=2, Date=new DateTime(2020, 01, 21), PresenceType = SessionPresenceType.MorningOnly},
                    new SessionDayEF { Id=3, Date=new DateTime(2020, 01, 22), PresenceType = SessionPresenceType.MorningOnly},
                },
            };

            List<UserSessionEF> userSessions = new List<UserSessionEF>()
            {
                new UserSessionEF
                {
                    SessionId = sessionEF.Id,
                    Session = sessionEF,
                    UserId = studentEF.Id,
                    User = studentEF
                },

                new UserSessionEF
                {
                    SessionId = sessionEF.Id,
                    Session = sessionEF,
                    UserId = teacherEF.Id,
                    User = teacherEF
                }
            };

            sessionEF.UserSessions = userSessions;

            #endregion EFInitialization

            SessionTO sessionConverted = sessionEF.ToTransfertObject();

            Assert.AreEqual(sessionTO.Id, sessionConverted.Id);
        }

        [TestMethod]
        public void Should_Have_Same_Teacher_As_EF()
        {
            #region TOInitialization

            UserTO student = new UserTO()
            {
                Id = 1,
                Name = "Jacky Fringant",
                Email = "jacky@supermail.com",
                IsActivated = true,
                Role = UserRole.Attendee,
            };

            UserTO teacher = new UserTO()
            {
                Id = 2,
                Name = "Johnny Begood",
                Email = "johnny@yolomail.com",
                IsActivated = true,
                Role = UserRole.Teacher
            };

            CourseTO sql = new CourseTO()
            {
                Id = 1,
                Name = "SQL"
            };

            SessionTO sessionTO = new SessionTO()
            {
                Id = 1,
                Teacher = teacher,
                Course = sql,
                SessionDays = new List<SessionDayTO>()
                {
                   new SessionDayTO(){Id = 1, Date = new DateTime(2020, 2, 3), PresenceType = SessionPresenceType.MorningAfternoon},
                   new SessionDayTO(){Id = 2, Date = new DateTime(2020, 2, 4), PresenceType = SessionPresenceType.MorningAfternoon},
                   new SessionDayTO(){Id = 3, Date = new DateTime(2020, 2, 5), PresenceType = SessionPresenceType.MorningAfternoon}
                },

                Attendees = new List<UserTO>()
                {
                    student,
                }
            };

            #endregion TOInitialization

            #region EFInitialization

            UserEF studentEF = new UserEF()
            {
                Id = 1,
                Name = "Jacky Fringant",
                Email = "jacky@supermail.com",
                IsActivated = true,
                Role = UserRole.Attendee,
            };

            UserEF teacherEF = new UserEF()
            {
                Id = 2,
                Name = "Johnny Begood",
                Email = "johnny@yolomail.com",
                IsActivated = true,
                Role = UserRole.Teacher
            };

            CourseEF sqlEF = new CourseEF()
            {
                Id = 1,
                Name = "SQL"
            };

            SessionEF sessionEF = new SessionEF()
            {
                Id = 1,
                Course = sqlEF,
                Dates = new List<SessionDayEF>()
                {
                    new SessionDayEF { Id=1, Date=new DateTime(2020, 01, 20), PresenceType = SessionPresenceType.MorningOnly},
                    new SessionDayEF { Id=2, Date=new DateTime(2020, 01, 21), PresenceType = SessionPresenceType.MorningOnly},
                    new SessionDayEF { Id=3, Date=new DateTime(2020, 01, 22), PresenceType = SessionPresenceType.MorningOnly},
                },
            };

            List<UserSessionEF> userSessions = new List<UserSessionEF>()
            {
                new UserSessionEF
                {
                    SessionId = sessionEF.Id,
                    Session = sessionEF,
                    UserId = studentEF.Id,
                    User = studentEF
                },

                new UserSessionEF
                {
                    SessionId = sessionEF.Id,
                    Session = sessionEF,
                    UserId = teacherEF.Id,
                    User = teacherEF
                }
            };

            sessionEF.UserSessions = userSessions;

            #endregion EFInitialization

            SessionTO sessionConverted = sessionEF.ToTransfertObject();

            Assert.AreEqual(sessionTO.Teacher.Id, sessionConverted.Teacher.Id);
        }

        [TestMethod]
        public void Should_Have_Same_Course_As_EF()
        {
            #region TOInitialization

            UserTO student = new UserTO()
            {
                Id = 1,
                Name = "Jacky Fringant",
                Email = "jacky@supermail.com",
                IsActivated = true,
                Role = UserRole.Attendee,
            };

            UserTO teacher = new UserTO()
            {
                Id = 2,
                Name = "Johnny Begood",
                Email = "johnny@yolomail.com",
                IsActivated = true,
                Role = UserRole.Teacher
            };

            CourseTO sql = new CourseTO()
            {
                Id = 1,
                Name = "SQL"
            };

            SessionTO sessionTO = new SessionTO()
            {
                Id = 1,
                Teacher = teacher,
                Course = sql,
                SessionDays = new List<SessionDayTO>()
                {
                   new SessionDayTO(){Id = 1, Date = new DateTime(2020, 2, 3), PresenceType = SessionPresenceType.MorningAfternoon},
                   new SessionDayTO(){Id = 2, Date = new DateTime(2020, 2, 4), PresenceType = SessionPresenceType.MorningAfternoon},
                   new SessionDayTO(){Id = 3, Date = new DateTime(2020, 2, 5), PresenceType = SessionPresenceType.MorningAfternoon}
                },

                Attendees = new List<UserTO>()
                {
                    student,
                }
            };

            #endregion TOInitialization

            #region EFInitialization

            UserEF studentEF = new UserEF()
            {
                Id = 1,
                Name = "Jacky Fringant",
                Email = "jacky@supermail.com",
                IsActivated = true,
                Role = UserRole.Attendee,
            };

            UserEF teacherEF = new UserEF()
            {
                Id = 2,
                Name = "Johnny Begood",
                Email = "johnny@yolomail.com",
                IsActivated = true,
                Role = UserRole.Teacher
            };

            CourseEF sqlEF = new CourseEF()
            {
                Id = 1,
                Name = "SQL"
            };

            SessionEF sessionEF = new SessionEF()
            {
                Id = 1,
                Course = sqlEF,
                Dates = new List<SessionDayEF>()
                {
                    new SessionDayEF { Id=1, Date=new DateTime(2020, 01, 20), PresenceType = SessionPresenceType.MorningOnly},
                    new SessionDayEF { Id=2, Date=new DateTime(2020, 01, 21), PresenceType = SessionPresenceType.MorningOnly},
                    new SessionDayEF { Id=3, Date=new DateTime(2020, 01, 22), PresenceType = SessionPresenceType.MorningOnly},
                },
            };

            List<UserSessionEF> userSessions = new List<UserSessionEF>()
            {
                new UserSessionEF
                {
                    SessionId = sessionEF.Id,
                    Session = sessionEF,
                    UserId = studentEF.Id,
                    User = studentEF
                },

                new UserSessionEF
                {
                    SessionId = sessionEF.Id,
                    Session = sessionEF,
                    UserId = teacherEF.Id,
                    User = teacherEF
                }
            };

            sessionEF.UserSessions = userSessions;

            #endregion EFInitialization

            SessionTO sessionConverted = sessionEF.ToTransfertObject();

            Assert.AreEqual(sessionTO.Course.Id, sessionConverted.Course.Id);
        }

        [TestMethod]
        public void Should_Contain_One_Attendee()
        {
            #region TOInitialization

            UserTO student = new UserTO()
            {
                Id = 1,
                Name = "Jacky Fringant",
                Email = "jacky@supermail.com",
                IsActivated = true,
                Role = UserRole.Attendee,
            };

            UserTO teacher = new UserTO()
            {
                Id = 2,
                Name = "Johnny Begood",
                Email = "johnny@yolomail.com",
                IsActivated = true,
                Role = UserRole.Teacher
            };

            CourseTO sql = new CourseTO()
            {
                Id = 1,
                Name = "SQL"
            };

            SessionTO sessionTO = new SessionTO()
            {
                Id = 1,
                Teacher = teacher,
                Course = sql,
                SessionDays = new List<SessionDayTO>()
                {
                   new SessionDayTO(){Id = 1, Date = new DateTime(2020, 2, 3), PresenceType = SessionPresenceType.MorningAfternoon},
                   new SessionDayTO(){Id = 2, Date = new DateTime(2020, 2, 4), PresenceType = SessionPresenceType.MorningAfternoon},
                   new SessionDayTO(){Id = 3, Date = new DateTime(2020, 2, 5), PresenceType = SessionPresenceType.MorningAfternoon}
                },

                Attendees = new List<UserTO>()
                {
                    student,
                }
            };

            #endregion TOInitialization

            #region EFInitialization

            UserEF studentEF = new UserEF()
            {
                Id = 1,
                Name = "Jacky Fringant",
                Email = "jacky@supermail.com",
                IsActivated = true,
                Role = UserRole.Attendee,
            };

            UserEF teacherEF = new UserEF()
            {
                Id = 2,
                Name = "Johnny Begood",
                Email = "johnny@yolomail.com",
                IsActivated = true,
                Role = UserRole.Teacher
            };

            CourseEF sqlEF = new CourseEF()
            {
                Id = 1,
                Name = "SQL"
            };

            SessionEF sessionEF = new SessionEF()
            {
                Id = 1,
                Course = sqlEF,
                Dates = new List<SessionDayEF>()
                {
                    new SessionDayEF { Id=1, Date=new DateTime(2020, 01, 20), PresenceType = SessionPresenceType.MorningOnly},
                    new SessionDayEF { Id=2, Date=new DateTime(2020, 01, 21), PresenceType = SessionPresenceType.MorningOnly},
                    new SessionDayEF { Id=3, Date=new DateTime(2020, 01, 22), PresenceType = SessionPresenceType.MorningOnly},
                },
            };

            List<UserSessionEF> userSessions = new List<UserSessionEF>()
            {
                new UserSessionEF
                {
                    SessionId = sessionEF.Id,
                    Session = sessionEF,
                    UserId = studentEF.Id,
                    User = studentEF
                },
            };

            sessionEF.UserSessions = userSessions;

            #endregion EFInitialization

            SessionTO sessionConverted = sessionEF.ToTransfertObject();

            Assert.AreEqual(sessionTO.Attendees.Count, sessionConverted.Attendees.Count);
        }
    }
}