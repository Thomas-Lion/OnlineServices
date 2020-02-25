﻿using Microsoft.EntityFrameworkCore;
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
    public class User_UpdateTests
    {
        [TestMethod]
        public void UpdateUser_NameChanged()
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
            AddedUser1.Name = "You lost the game";
            userRepository.Update(AddedUser1);
            RSCxt.SaveChanges();
            //assert
            Assert.AreEqual(3, userRepository.GetAll().Count());
            Assert.AreEqual("You lost the game", userRepository.GetById(AddedUser1.Id).Name);
        }

        [TestMethod]
        public void UpdateUser_StatusChanged()
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
            AddedUser1.IsArchived = true;
            userRepository.Update(AddedUser1);
            RSCxt.SaveChanges();
            //assert
            Assert.AreEqual(2, userRepository.GetAll().Count());
            Assert.AreEqual(true, userRepository.GetById(AddedUser1.Id).IsArchived);
        }

        [TestMethod]
        public void UpdateUser_RoleChanged()
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
            AddedUser1.Role = UserRole.Assistant;
            userRepository.Update(AddedUser1);
            RSCxt.SaveChanges();
            //assert
            Assert.AreEqual(3, userRepository.GetAll().Count());
            Assert.AreEqual(UserRole.Assistant, userRepository.GetById(AddedUser1.Id).Role);
        }

        [TestMethod]
        public void UpdateUser_AddCompanyWithUpdate()
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
            AddedUser1.Company = "LE BUS MAGIQUE";
            userRepository.Update(AddedUser1);
            RSCxt.SaveChanges();
            //assert
            Assert.AreEqual(3, userRepository.GetAll().Count());
            Assert.AreEqual("LE BUS MAGIQUE", userRepository.GetById(AddedUser1.Id).Company);
        }

        [TestMethod]
        public void UpdateUser_ShouldThrowExceptionWhenInvalid()
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

            RSCxt.SaveChanges();
            //act & assert
            Assert.AreEqual(2, userRepository.GetAll().Count());
            Assert.ThrowsException<ArgumentException>(() => userRepository.Update(John));
        }
    }
}