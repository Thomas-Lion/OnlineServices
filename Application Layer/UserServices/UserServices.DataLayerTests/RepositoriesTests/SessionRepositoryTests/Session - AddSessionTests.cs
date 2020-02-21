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
    public class Session_AddSessionTests
    {
        [TestMethod]
        public void Should_Insert_Session_when_valid()
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

                var AddedTeacher = userRepository.Add(Teacher);
                var AddedAttendee = userRepository.Add(Michou);
                context.SaveChanges();

                var SQLCourse = new CourseTO()
                {
                    //Id = 28,
                    Name = "SQL"
                };

                var AddedCourse = courseRepository.Add(SQLCourse);
                context.SaveChanges();

                var SQLSession = new SessionTO()
                {
                    //Id = 1,
                    Attendees = new List<UserTO>()
                {
                    AddedAttendee
                },
                    Course = AddedCourse,
                    Teacher = AddedTeacher,
                };

                //By Amb
                //var mySession = new SessionTO()
                //{
                //    Attendees = new List<UserTO> { new UserTO { Name = "AAA", Email = "a@gmail.com", Role = UserRole.Attendee, IsActivated = false } },
                //};
                //By Amb

                var AddedSession = sessionRepository.Add(SQLSession);
                context.SaveChanges();
                Assert.AreEqual(2, userRepository.GetAll().Count());

                Assert.AreEqual(1, sessionRepository.GetAll().Count());
            }
        }

        [TestMethod]
        public void Should_Throw_Exception_When_Course_IsArchived()
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

                var AddedTeacher = userRepository.Add(Teacher);
                var AddedAttendee = userRepository.Add(Michou);
                context.SaveChanges();

                var SQLCourse = new CourseTO()
                {
                    //Id = 28,
                    Name = "SQL",
                    IsArchived = true
                };

                var AddedCourse = courseRepository.Add(SQLCourse);
                context.SaveChanges();

                var SQLSession = new SessionTO()
                {
                    //Id = 1,
                    Attendees = new List<UserTO>()
                        {
                            AddedAttendee
                        },
                    Course = AddedCourse,
                    Teacher = AddedTeacher,
                };

                Assert.ThrowsException<ArgumentException>(() => sessionRepository.Add(SQLSession));
            }
        }

        [TestMethod]
        public void Should_Insert_Session_Without_Teacher_And_Users()
        {
            var options = new DbContextOptionsBuilder<RegistrationContext>()
               .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
               .Options;

            using (var context = new RegistrationContext(options))
            {
                IRSSessionRepository sessionRepository = new SessionRepository(context);
                IRSCourseRepository courseRepository = new CourseRepository(context);

                var hugeCourse = new CourseTO()
                {
                    Name = "Huge COurse"
                };

                var addedCourse = courseRepository.Add(hugeCourse);

                var hugeSession = new SessionTO()
                {
                    Course = addedCourse
                };

                var addedSession = sessionRepository.Add(hugeSession);

                context.SaveChanges();

                Assert.AreEqual(1, sessionRepository.GetAll().Count());
            }
        }
    }
}