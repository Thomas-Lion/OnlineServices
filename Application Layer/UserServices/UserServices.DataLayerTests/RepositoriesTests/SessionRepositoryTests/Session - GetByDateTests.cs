using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineServices.Common.RegistrationServices.Enumerations;
using OnlineServices.Common.RegistrationServices.Interfaces;
using OnlineServices.Common.RegistrationServices.TransferObject;
using RegistrationServices.DataLayer;
using RegistrationServices.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RegistrationServices.DataLayerTests.RepositoriesTests.SessionRepositoryTests
{
    [TestClass]
    public class Session_GetByDateTests
    {
        [TestMethod]
        public void Should_Return_0_Sessions()
        {
            var options = new DbContextOptionsBuilder<RegistrationContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;

            using (var context = new RegistrationContext(options))
            {
                IRSUserRepository userRepository = new UserRepository(context);
                IRSSessionRepository sessionRepository = new SessionRepository(context);
                IRSCourseRepository courseRepository = new CourseRepository(context);

                var Teacher = new UserTO()
                {
                    //Id = 420,
                    Name = "Christian",
                    Email = "gyssels@fartmail.com",
                    Role = UserRole.Teacher
                };

                var Michou = new UserTO()
                {
                    //Id = 45,
                    Name = "Michou Miraisin",
                    Email = "michou@superbg.caca",
                    Role = UserRole.Attendee
                };

                var Isabelle = new UserTO()
                {
                    Name = "Isabelle Balkany",
                    Email = "isa@rendlargent.gouv",
                    Role = UserRole.Attendee
                };

                var AddedTeacher = userRepository.Add(Teacher);
                var AddedAttendee = userRepository.Add(Michou);
                var AddedAttendee2 = userRepository.Add(Isabelle);
                context.SaveChanges();

                var SQLCourse = new CourseTO()
                {
                    Name = "SQL"
                };

                var MVCCourse = new CourseTO()
                {
                    Name = "MVC"
                };

                var AddedCourse = courseRepository.Add(SQLCourse);
                var AddedCourse2 = courseRepository.Add(MVCCourse);
                context.SaveChanges();

                var SQLSession = new SessionTO()
                {
                    Attendees = new List<UserTO>()
                    {
                        Michou
                    },

                    Course = AddedCourse,
                    Teacher = Teacher,

                    SessionDays = new List<SessionDayTO>()
                    {
                        new SessionDayTO()
                        {
                             Date = new DateTime(2020, 02, 20),
                              PresenceType = SessionPresenceType.MorningAfternoon
                        },

                        new SessionDayTO()
                        {
                            Date = new DateTime(2020,02,21),
                            PresenceType = SessionPresenceType.MorningAfternoon
                        }
                    }
                };

                var MVCSession = new SessionTO()
                {
                    Attendees = new List<UserTO>()
                    {
                        Isabelle
                    },

                    Course = AddedCourse,
                    Teacher = Teacher,

                    SessionDays = new List<SessionDayTO>()
                    {
                        new SessionDayTO()
                        {
                             Date = new DateTime(2020, 03, 20),
                              PresenceType = SessionPresenceType.MorningAfternoon
                        },

                        new SessionDayTO()
                        {
                            Date = new DateTime(2020,03,21),
                            PresenceType = SessionPresenceType.MorningAfternoon
                        }
                    }
                };

                var AddedSession = sessionRepository.Add(SQLSession);
                context.SaveChanges();

                Assert.AreEqual(0, sessionRepository.GetSessionsByDate(new DateTime(2021, 05, 11)).Count());
            }
        }

        [TestMethod]
        public void Should_Return_1_Session()
        {
            var options = new DbContextOptionsBuilder<RegistrationContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;

            using (var context = new RegistrationContext(options))
            {
                IRSUserRepository userRepository = new UserRepository(context);
                IRSSessionRepository sessionRepository = new SessionRepository(context);
                IRSCourseRepository courseRepository = new CourseRepository(context);

                var Teacher = new UserTO()
                {
                    //Id = 420,
                    Name = "Christian",
                    Email = "gyssels@fartmail.com",
                    Role = UserRole.Teacher
                };

                var Michou = new UserTO()
                {
                    //Id = 45,
                    Name = "Michou Miraisin",
                    Email = "michou@superbg.caca",
                    Role = UserRole.Attendee
                };

                var Isabelle = new UserTO()
                {
                    Name = "Isabelle Balkany",
                    Email = "isa@rendlargent.gouv",
                    Role = UserRole.Attendee
                };

                var AddedTeacher = userRepository.Add(Teacher);
                var AddedAttendee = userRepository.Add(Michou);
                var AddedAttendee2 = userRepository.Add(Isabelle);
                context.SaveChanges();

                var SQLCourse = new CourseTO()
                {
                    Name = "SQL"
                };

                var MVCCourse = new CourseTO()
                {
                    Name = "MVC"
                };

                var AddedCourse = courseRepository.Add(SQLCourse);
                var AddedCourse2 = courseRepository.Add(MVCCourse);
                context.SaveChanges();

                var SQLSession = new SessionTO()
                {
                    Attendees = new List<UserTO>()
                    {
                        Michou
                    },

                    Course = AddedCourse,
                    Teacher = Teacher,

                    SessionDays = new List<SessionDayTO>()
                    {
                        new SessionDayTO()
                        {
                             Date = new DateTime(2020, 02, 20),
                              PresenceType = SessionPresenceType.MorningAfternoon
                        },

                        new SessionDayTO()
                        {
                            Date = new DateTime(2020,02,21),
                            PresenceType = SessionPresenceType.MorningAfternoon
                        }
                    }
                };

                var MVCSession = new SessionTO()
                {
                    Attendees = new List<UserTO>()
                    {
                        Isabelle
                    },

                    Course = AddedCourse,
                    Teacher = Teacher,

                    SessionDays = new List<SessionDayTO>()
                    {
                        new SessionDayTO()
                        {
                             Date = new DateTime(2020, 02, 20),
                              PresenceType = SessionPresenceType.MorningAfternoon
                        },

                        new SessionDayTO()
                        {
                            Date = new DateTime(2020,03,21),
                            PresenceType = SessionPresenceType.MorningAfternoon
                        }
                    }
                };

                var AddedSession = sessionRepository.Add(SQLSession);
                context.SaveChanges();

                Assert.AreEqual(0, sessionRepository.GetSessionsByDate(new DateTime(2020, 02, 21)).Count());
                Assert.AreEqual(0, sessionRepository.GetSessionsByDate(new DateTime(2020, 03, 21)).Count());
            }
        }

        [TestMethod]
        public void Should_Return_2_Session()
        {
            var options = new DbContextOptionsBuilder<RegistrationContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;

            using (var context = new RegistrationContext(options))
            {
                IRSUserRepository userRepository = new UserRepository(context);
                IRSSessionRepository sessionRepository = new SessionRepository(context);
                IRSCourseRepository courseRepository = new CourseRepository(context);

                var Teacher = new UserTO()
                {
                    //Id = 420,
                    Name = "Christian",
                    Email = "gyssels@fartmail.com",
                    Role = UserRole.Teacher
                };

                var Michou = new UserTO()
                {
                    //Id = 45,
                    Name = "Michou Miraisin",
                    Email = "michou@superbg.caca",
                    Role = UserRole.Attendee
                };

                var Isabelle = new UserTO()
                {
                    Name = "Isabelle Balkany",
                    Email = "isa@rendlargent.gouv",
                    Role = UserRole.Attendee
                };

                var AddedTeacher = userRepository.Add(Teacher);
                var AddedAttendee = userRepository.Add(Michou);
                var AddedAttendee2 = userRepository.Add(Isabelle);
                context.SaveChanges();

                var SQLCourse = new CourseTO()
                {
                    Name = "SQL"
                };

                var MVCCourse = new CourseTO()
                {
                    Name = "MVC"
                };

                var AddedCourse = courseRepository.Add(SQLCourse);
                var AddedCourse2 = courseRepository.Add(MVCCourse);
                context.SaveChanges();

                var SQLSession = new SessionTO()
                {
                    Attendees = new List<UserTO>()
                    {
                        Michou
                    },

                    Course = AddedCourse,
                    Teacher = Teacher,

                    SessionDays = new List<SessionDayTO>()
                    {
                        new SessionDayTO()
                        {
                             Date = new DateTime(2020, 02, 20),
                              PresenceType = SessionPresenceType.MorningAfternoon
                        },

                        new SessionDayTO()
                        {
                            Date = new DateTime(2020,02,21),
                            PresenceType = SessionPresenceType.MorningAfternoon
                        }
                    }
                };

                var MVCSession = new SessionTO()
                {
                    Attendees = new List<UserTO>()
                    {
                        Isabelle
                    },

                    Course = AddedCourse,
                    Teacher = Teacher,

                    SessionDays = new List<SessionDayTO>()
                    {
                        new SessionDayTO()
                        {
                             Date = new DateTime(2020, 03, 20),
                              PresenceType = SessionPresenceType.MorningAfternoon
                        },

                        new SessionDayTO()
                        {
                            Date = new DateTime(2020,03,21),
                            PresenceType = SessionPresenceType.MorningAfternoon
                        }
                    }
                };

                var AddedSession = sessionRepository.Add(SQLSession);
                context.SaveChanges();

                Assert.AreEqual(0, sessionRepository.GetSessionsByDate(new DateTime(2020, 02, 20)).Count());
            }
        }
    }
}