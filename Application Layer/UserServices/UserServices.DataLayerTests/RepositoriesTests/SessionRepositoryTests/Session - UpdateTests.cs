using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class Session_UpdateTests
    {
        [TestMethod]
        public void Should_Contain_2Students_After_Adding_User()
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

                var AddedCourse = courseRepository.Add(SQLCourse);
                context.SaveChanges();

                var SQLSession = new SessionTO()
                {
                    Attendees = new List<UserTO>()
                    {
                        AddedAttendee
                    },

                    Course = AddedCourse,
                    Teacher = AddedTeacher,
                };

                var AddedSession = sessionRepository.Add(SQLSession);
                context.SaveChanges();

                Assert.AreEqual(1, sessionRepository.GetStudents(AddedSession).Count());

                AddedSession.Attendees.Add(AddedAttendee2);
                sessionRepository.Update(AddedSession);
                context.SaveChanges();

                Assert.AreEqual(2, sessionRepository.GetStudents(AddedSession).Count());
            }
        }

        [TestMethod]
        public void Should_Return_One_User_After_Removing_User()
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

                var AddedCourse = courseRepository.Add(SQLCourse);
                context.SaveChanges();

                var SQLSession = new SessionTO()
                {
                    Attendees = new List<UserTO>()
                    {
                        AddedAttendee, AddedAttendee2
                    },

                    Course = AddedCourse,
                    Teacher = AddedTeacher,
                };

                var AddedSession = sessionRepository.Add(SQLSession);
                context.SaveChanges();
                Assert.AreEqual(2, sessionRepository.GetStudents(AddedSession).Count());

                AddedSession.Attendees.Remove(AddedAttendee2);
                sessionRepository.Update(AddedSession);
                context.SaveChanges();
                Assert.AreEqual(1, sessionRepository.GetStudents(AddedSession).Count());
            }
        }

        [TestMethod]
        public void Modifying_Teacher()
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
                    Role = UserRole.Teacher
                };

                var AddedTeacher = userRepository.Add(Teacher);
                var AddedTeacher2 = userRepository.Add(Isabelle);
                var AddedAttendee = userRepository.Add(Michou);

                context.SaveChanges();

                var SQLCourse = new CourseTO()
                {
                    Name = "SQL"
                };

                var AddedCourse = courseRepository.Add(SQLCourse);
                context.SaveChanges();

                var SQLSession = new SessionTO()
                {
                    Attendees = new List<UserTO>()
                    {
                        AddedAttendee,
                    },

                    Course = AddedCourse,
                    Teacher = AddedTeacher,
                };

                var AddedSession = sessionRepository.Add(SQLSession);
                context.SaveChanges();
                Assert.AreEqual("Christian", AddedSession.Teacher.Name);

                AddedSession.Teacher = AddedTeacher2;
                AddedSession = sessionRepository.Update(AddedSession);
                context.SaveChanges();
                Assert.AreEqual("Isabelle Balkany", AddedSession.Teacher.Name);
            }
        }

        [TestMethod]
        public void Modifying_Course()
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

                context.SaveChanges();

                var SQLCourse = new CourseTO()
                {
                    Name = "SQL"
                };

                var MVCCourse = new CourseTO() { Name = "MVC" };

                var AddedCourse = courseRepository.Add(SQLCourse);
                var AddedCourse2 = courseRepository.Add(MVCCourse);
                context.SaveChanges();

                var SQLSession = new SessionTO()
                {
                    Attendees = new List<UserTO>()
                    {
                        AddedAttendee,
                    },

                    Course = AddedCourse,
                    Teacher = AddedTeacher,
                };

                var AddedSession = sessionRepository.Add(SQLSession);
                context.SaveChanges();
                Assert.AreEqual("SQL", AddedSession.Course.Name);

                AddedSession.Course = AddedCourse2;
                AddedSession = sessionRepository.Update(AddedSession);
                context.SaveChanges();
                Assert.AreEqual(1, sessionRepository.GetStudents(AddedSession).Count());
            }
        }
    }
}