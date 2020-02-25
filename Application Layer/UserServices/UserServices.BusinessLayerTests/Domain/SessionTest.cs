using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using RegistrationServices.BusinessLayer;

namespace RegistrationServices.BusinessLayerTests
{
    [TestClass]
    public class SessionTest
    {
        private Course cou1 = new Course { Id = 1, Name = "Course01" };
        private Course cou2 = new Course { Id = 2, Name = "Course02" };
        private Course cou3 = new Course { Id = 3, Name = "Course03" };

        private User teacher = new User { Id = 1, Name = "User_Teacher", IsArchived = false, Company = "Company 01", Role = UserRole.Teacher, Email = "teacher@gmail.com" };

        private User attendee1 = new User { Id = 2, Name = "User_Attendee", IsArchived = true, Company = "Company 02", Role = UserRole.Attendee, Email = "student@gmail.com" };
        private User attendee2 = new User { Id = 3, Name = "User_Attendee3", IsArchived = true, Company = "Company 02", Role = UserRole.Attendee, Email = "student2@gmail.com" };

        private List<User> attendees = new List<User>();

        //[Ignore]
        //[TestMethod]
        //public void GetCoursesAll()
        //{
        //    var startDate = DateTime.Now;
        //    var endDate = startDate.AddDays(30);

        //    attendees.Add(attendee1);
        //    attendees.Add(attendee2);

        //    List<DateTime> dates = new List<DateTime>();
        //    dates.Add(startDate);
        //    dates.Add(endDate);

        //    var ses = new Session { Id = 1, Teacher = teacher, Course = cou1, Attendees = attendees, Dates = dates }; //StartDate = startDate, EndDate = endDate };

        //    //Course
        //    Assert.AreEqual(cou1.Name, ses.Course.Name);
        //}

        //[Ignore]
        //[TestMethod()]
        //public void GetCourse_by_Session()
        //{
        //    attendees.Add(attendee1);
        //    attendees.Add(attendee2);

        //    var startDate = DateTime.Now;
        //    var endDate = startDate.AddDays(30) ;

        //    List<DateTime> dates = new List<DateTime>();
        //    dates.Add(startDate);
        //    dates.Add(endDate);

        //    var ses = new Session { Id = 1, Teacher = teacher,  Course = cou1,  Attendees = attendees , Dates = dates };

        //    //Course
        //    Assert.AreEqual(cou1.Id, ses.Course.Id);
        //    Assert.AreEqual(cou1.Name, ses.Course.Name);

        //    //Teacher
        //    Assert.AreEqual(teacher.Id, ses.Teacher.Id);
        //    Assert.AreEqual(teacher.Name, ses.Teacher.Name);
        //    Assert.AreEqual(teacher.IsActivated, ses.Teacher.IsActivated);
        //    Assert.AreEqual(teacher.Company, ses.Teacher.Company);
        //    Assert.AreEqual(teacher.Role, ses.Teacher.Role);
        //    Assert.AreEqual(teacher.Email, ses.Teacher.Email);

        //    //Attendee
        //    Assert.AreEqual(attendee1.Id, ses.Attendees[0].Id);
        //    Assert.AreEqual(attendee1.Name, ses.Attendees[0].Name);
        //    Assert.AreEqual(attendee1.IsActivated, ses.Attendees[0].IsActivated);
        //    Assert.AreEqual(attendee1.Company, ses.Attendees[0].Company);
        //    Assert.AreEqual(attendee1.Role, ses.Attendees[0].Role);
        //    Assert.AreEqual(attendee1.Email, ses.Attendees[0].Email);

        //    //IsActivatedFalse
        //    Assert.IsFalse(attendee1.IsActivated);

        //    //IsActivatedTrue
        //    Assert.IsTrue(teacher.IsActivated);

        //}
    }
}