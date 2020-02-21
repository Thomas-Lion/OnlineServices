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
    public class User_GetUserByRoleTests
    {
        [TestMethod]
        public void GetUserByRole_CorrespondingResult()
        {
            var options = new DbContextOptionsBuilder<RegistrationContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;

            using var RSCxt = new RegistrationContext(options);
            IRSUserRepository userRepository = new UserRepository(RSCxt);

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

            Assert.AreEqual(2, userRepository.GetUserByRole(UserRole.Attendee).Count());
        }

        [TestMethod]
        public void GetUserByRole_NoResult()
        {
            var options = new DbContextOptionsBuilder<RegistrationContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;

            using var RSCxt = new RegistrationContext(options);
            IRSUserRepository userRepository = new UserRepository(RSCxt);

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

            Assert.AreEqual(0, userRepository.GetUserByRole(UserRole.Assistant).Count());
        }
    }
}
