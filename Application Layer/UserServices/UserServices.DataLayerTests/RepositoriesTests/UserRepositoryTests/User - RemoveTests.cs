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
    public class User_RemoveTests
    {
        [TestMethod]
        public void RemoveUser_WhenValid()
        {
            //arrange
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
            //act
            userRepository.Remove(AddedUser0.Id);
            RSCxt.SaveChanges();
            //assert
            Assert.AreEqual(2, userRepository.GetAll().Count());
            Assert.IsFalse(userRepository.GetAll().Any(x => x.Id == 1));
        }
        [TestMethod]
        public void RemoveUser_WhenInvalidId()
        {
            //arrange
            var options = new DbContextOptionsBuilder<RegistrationContext>()
               .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
               .Options;

            using var RSCxt = new RegistrationContext(options);
            IRSUserRepository userRepository = new UserRepository(RSCxt);
            var John = new UserTO()
            {
                Name = "John",
                Email = "John@JHON.Nee",
                Role = UserRole.Attendee
            };
            userRepository.Add(John);
            RSCxt.SaveChanges();
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.Remove(15));

        }
        [TestMethod]
        public void RemoveUser_WhenInvalidUser()
        {
            //arrange
            var options = new DbContextOptionsBuilder<RegistrationContext>()
               .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
               .Options;

            using var RSCxt = new RegistrationContext(options);
            IRSUserRepository userRepository = new UserRepository(RSCxt);
            var John = new UserTO()
            {
                Name = "John",
                Email = "John@JHON.Nee",
                Role = UserRole.Attendee
            };
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.Remove(John));

        }
    }
}
