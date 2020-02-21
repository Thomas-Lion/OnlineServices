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

namespace RegistrationServices.DataLayerTests.RepositoriesTests.UserRepositoryTests
{
    [TestClass]
    public class User_GetUserBySessionTests
    {
        [TestMethod]
        public void GetUserBySession_WhenValid()
        {
            //arrange
            var options = new DbContextOptionsBuilder<RegistrationContext>()
               .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
               .Options;

            using var RSCxt = new RegistrationContext(options);
            IRSUserRepository userRepository = new UserRepository(RSCxt);
            IRSCourseRepository courseRepository = new CourseRepository(RSCxt);
            IRSSessionRepository sessionRepository = new SessionRepository(RSCxt);

            var Teacher = new UserTO()
            {
                Name = "Max",
                Email = "Padawan@HighGround.OW",
                Role = UserRole.Teacher
            };
            var Jack = new UserTO()
            {
                Name = "Jack Jack",
                Email = "Jack@Kcaj.Niet",
                Role = UserRole.Attendee
            };
            var John = new UserTO()
            {
                Name = "John",
                Email = "John@JHON.Nee",
                Role = UserRole.Attendee
            };

            var AddedUser0 = userRepository.Add(Teacher);
            var AddedUser1 = userRepository.Add(Jack);
            var AddedUser2 = userRepository.Add(John);
            RSCxt.SaveChanges();

            var SQLCourse = new CourseTO()
            {
                Name = "SQL"
            };

            var AddedCourse = courseRepository.Add(SQLCourse);
            RSCxt.SaveChanges();

            var SQLSession = new SessionTO()
            {
                Attendees = new List<UserTO>()
                {
                    AddedUser1
                },
                Course = AddedCourse,
                Teacher = AddedUser0,
            };

            var AddedSession = sessionRepository.Add(SQLSession);
            RSCxt.SaveChanges();
            //act

            //assert
            Assert.AreEqual(3, userRepository.GetAll().Count());
            Assert.AreEqual(2, userRepository.GetUsersBySession(AddedSession).Count());
        }

        [TestMethod]
        public void GetUserBySession_NoValidSession()
        {
            var options = new DbContextOptionsBuilder<RegistrationContext>()
              .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
              .Options;

            using var RSCxt = new RegistrationContext(options);
            IRSUserRepository userRepository = new UserRepository(RSCxt);
            IRSCourseRepository courseRepository = new CourseRepository(RSCxt);
            IRSSessionRepository sessionRepository = new SessionRepository(RSCxt);

            var Teacher = new UserTO()
            {
                Name = "Max",
                Email = "Padawan@HighGround.OW",
                Role = UserRole.Teacher
            };
            var Jack = new UserTO()
            {
                Name = "Jack Jack",
                Email = "Jack@Kcaj.Niet",
                Role = UserRole.Attendee
            };
            var John = new UserTO()
            {
                Name = "John",
                Email = "John@JHON.Nee",
                Role = UserRole.Attendee
            };

            var AddedUser0 = userRepository.Add(Teacher);
            var AddedUser1 = userRepository.Add(Jack);
            var AddedUser2 = userRepository.Add(John);
            RSCxt.SaveChanges();

            var SQLCourse = new CourseTO()
            {
                Name = "SQL"
            };

            var AddedCourse = courseRepository.Add(SQLCourse);
            RSCxt.SaveChanges();

            var SQLSession = new SessionTO()
            {
                Attendees = new List<UserTO>() { },
                Course = AddedCourse,
                Teacher = null
            };

            var AddedSession = sessionRepository.Add(SQLSession);
            RSCxt.SaveChanges();

            Assert.ThrowsException<NullReferenceException>(() => userRepository.GetUsersBySession(AddedSession));
        }
    }
}