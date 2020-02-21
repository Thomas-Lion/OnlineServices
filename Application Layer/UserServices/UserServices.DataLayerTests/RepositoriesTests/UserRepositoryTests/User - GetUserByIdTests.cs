using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineServices.Common.RegistrationServices.Interfaces;
using OnlineServices.Common.RegistrationServices.TransferObject;
using RegistrationServices.DataLayer;
using RegistrationServices.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RegistrationServices.DataLayerTests.RepositoriesTests.UserRepositoryTests
{
    [TestClass]
    public class User_GetUserByIdTests
    {
        [TestMethod]
        public void GetUserById_ValidId()
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
            //act
            var AddedUser0 = userRepository.Add(Teacher);
            var AddedUser1 = userRepository.Add(Jack);
            var AddedUser2 = userRepository.Add(John);

            RSCxt.SaveChanges();
            //assert
            Assert.AreEqual("Jack Jack", userRepository.GetById(AddedUser1.Id).Name);
            Assert.AreEqual("Jack@Kcaj.Niet", userRepository.GetById(AddedUser1.Id).Email);
        }
        [TestMethod]
        public void GetById_ThrowException()
        {
            var options = new DbContextOptionsBuilder<RegistrationContext>()
               .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
               .Options;

            using var RSCxt = new RegistrationContext(options);
            IRSUserRepository userRepository = new UserRepository(RSCxt);

            Assert.ThrowsException<NullReferenceException>(()=> userRepository.GetById(666));
        }
    }
}
