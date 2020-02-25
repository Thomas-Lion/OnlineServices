﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using RegistrationServices.BusinessLayer.UseCase.Assistant;
using Moq;
using OnlineServices.Common.RegistrationServices.Interfaces;
using System.Linq;
using OnlineServices.Common.RegistrationServices.TransferObject;

namespace RegistrationServices.BusinessLayerTests.UseCase.AssistantTests
{
    [TestClass]
    public class Assistant_GetCoursesTest
    {
        Mock<IRSUnitOfWork> MockUofW = new Mock<IRSUnitOfWork>();
        Mock<IRSCourseRepository> MockCourseRepository = new Mock<IRSCourseRepository>();

        public static List<CourseTO> CourseList()
        {
            return new List<CourseTO>
            {
                new CourseTO { Id=1, Name="Course1"},
                new CourseTO { Id=2, Name="Course2"},
                new CourseTO { Id=3, Name="Course3"}
            };
        }

        public static List<CourseTO> EmptyCourseList()
        {
            return new List<CourseTO>
            {
                null,
            };
        }


        [TestMethod]
        public void Throw_ArgumentNullException_When_CoursesTO_IsNULL()
        {
            MockCourseRepository.Setup(x => x.GetAll()).Returns(EmptyCourseList);
            MockUofW.Setup(x => x.CourseRepository).Returns(MockCourseRepository.Object);

            var Assistante = new RSAssistantRole(MockUofW.Object);

            //ASSERT
            Assert.ThrowsException<ArgumentNullException>(() => Assistante.GetCourses());
        }

        [TestMethod]
        public void GetCourses_ReturnsAllCoursesFromDB()
        {
            //ARRANGE
            MockCourseRepository.Setup( x=>x.GetAll()).Returns(CourseList);
            MockUofW.Setup(x => x.CourseRepository).Returns(MockCourseRepository.Object);

            var ass = new RSAssistantRole(MockUofW.Object);

            //ACT
            var cours = ass.GetCourses();
            
            //ASSERT
            Assert.AreEqual(CourseList().Count, cours.Count );
            Assert.AreEqual(3, cours.Count );
        }

        [TestMethod]
        public void GetCourses_UserRepositoryIsCalledOnce()
        {
            //ARRANGE
            MockCourseRepository.Setup( x => x.GetAll()).Returns(CourseList);
            MockUofW.Setup( x => x.CourseRepository).Returns(MockCourseRepository.Object);

            var ass = new RSAssistantRole(MockUofW.Object);

            //ACT
            var coursAll = ass.GetCourses();

            //ASSERT
            MockCourseRepository.Verify(x=>x.GetAll(), Times.Once);

        }

        [TestMethod]
        public void GetCourse_NullReferenceException_WhenCourseIDisZero()
        {
            //ARRANGE
            int courId = 0;
            var Assistante = new RSAssistantRole(new Mock<IRSUnitOfWork>().Object);

            //ASSERT
            Assert.ThrowsException<NullReferenceException>(() => Assistante.GetUserById(courId));
        }

        [TestMethod]
        public void GetCourse_ReturnsCourseByIDFromDB()
        {
            //ARRANGE
            int courId = 1;
            MockCourseRepository.Setup(x => x.GetById(courId)).Returns(CourseList().FirstOrDefault(x=>x.Id == courId));
            MockUofW.Setup(x => x.CourseRepository).Returns(MockCourseRepository.Object);

            var ass = new RSAssistantRole(MockUofW.Object);

            //ACT
            var userById = ass.GetCourseById(courId);

            //ASSERT
            Assert.AreEqual(courId ,userById.Id);
        }

        [TestMethod]
        public void GetCourse_ReturnsNull_WhenCourseDoesNotExist()
        {
            //ARRANGE
            int courId = 10000;
            MockCourseRepository.Setup(x => x.GetById(courId)).Returns(CourseList().FirstOrDefault(x => x.Id == courId));
            MockUofW.Setup(x => x.CourseRepository).Returns(MockCourseRepository.Object);

            var ass = new RSAssistantRole(MockUofW.Object);

            //ACT
            var courById = ass.GetCourseById(courId);

            //ASSERT
            Assert.IsNull(courById);
        }
    }
}
