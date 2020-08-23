using System;
using NUnit.Framework;
using StudentsTesting1.Logic.Users;

namespace StudentsNUnitTestProject
{
    class StudentTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void StudentConstructorTest()
        {
            //Arrange
            string firstNameExpected = "Ivan";
            string lastNameExpected = "Ivanov";
            string studentIdExpected = "Studak";
            string recordBookExpected = "Zachotka";
            string groupTitleExpected = "TEST";
            string loginExpected = "ivanov";

            //Act
            Student student = new Student(firstNameExpected, lastNameExpected, studentIdExpected, recordBookExpected, groupTitleExpected, loginExpected);

            //Assert
            Assert.AreEqual(firstNameExpected, student.firstName);
            Assert.AreEqual(lastNameExpected, student.lastName);
            Assert.AreEqual(studentIdExpected, student.studentID);
            Assert.AreEqual(recordBookExpected, student.recordBook);
            Assert.AreEqual(groupTitleExpected, student.groupTitle);
            Assert.AreEqual(loginExpected, student.login);
        }
    }
}
