using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineServices.Common.Exceptions;
using System.Net.Mail;
using RegistrationServices.BusinessLayer;

namespace RegistrationServices.BusinessLayerTests
{
    [TestClass]
    public class UserTest
    {
        private User ass = new User { Id = 1, Name = "Assistant", IsArchived = false, Company = "Company 01", Role = UserRole.Assistant, Email = "Assistant@gmail.com" };
        private User att = new User { Id = 2, Name = "Attendee", IsArchived = false, Company = "Company 02", Role = UserRole.Attendee, Email = "Attendee@gmail.com" };
        private User tea = new User { Id = 3, Name = "Teacher", IsArchived = false, Company = "Company 03", Role = UserRole.Teacher, Email = "Teacher@gmail.com" };

        [TestMethod()]
        public void Role_Validation()
        {
            Assert.AreEqual(UserRole.Assistant, ass.Role);
            Assert.AreEqual(UserRole.Attendee, att.Role);
            Assert.AreEqual(UserRole.Teacher, tea.Role);
        }

        [TestMethod()]
        public void IsValid_ThrowsIsNullOrWhiteSpaceException_WhenNullNameIsProvided()
        {
            var us = new User { Name = null };
            Assert.ThrowsException<IsNullOrWhiteSpaceException>(() => us.IsValid());
        }

        [TestMethod]
        public void IsValid_ThrowsIsNullOrWhiteSpaceException_WhenWhiteSpaceNameIsProvided()
        {
            var us = new User { Name = " " };
            Assert.ThrowsException<IsNullOrWhiteSpaceException>(() => us.IsValid());
        }

        [TestMethod]
        public void IsValid_ThrowsIsNullOrWhiteSpaceException_WhenEmptyNameIsProvided()
        {
            var us = new User { Name = "" };
            Assert.ThrowsException<IsNullOrWhiteSpaceException>(() => us.IsValid());
        }

        [TestMethod]
        public void IsValid_Email()
        {
            User noValidEmailUser = new User { Id = 0, Name = "Teacher", IsArchived = false, Company = "Company 03", Role = UserRole.Teacher, Email = "Teacher@gmailcom" };

            Assert.IsTrue(ass.ValidateEmail(ass.Email));
            Assert.IsTrue(att.ValidateEmail(att.Email));
            Assert.IsTrue(tea.ValidateEmail(tea.Email));

            Assert.IsFalse(noValidEmailUser.ValidateEmail(noValidEmailUser.Email));
        }
    }
}