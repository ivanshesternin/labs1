using NUnit.Framework;
using StudentsTesting1.Logic.Users;

namespace StudentsNUnitTestProject
{
    class TeacherTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TeacherConstructorTest()
        {
            //Arrange
            string firstNameExpected = "Petro";
            string lastNameExpected = "Petrov";
            string teacherIdExpected = "ID";
            string loginExpected = "petrov";

            //Act
            Teacher teacher = new Teacher(firstNameExpected, lastNameExpected, teacherIdExpected, loginExpected);

            //Assert
            Assert.AreEqual(firstNameExpected, teacher.firstName);
            Assert.AreEqual(lastNameExpected, teacher.lastName);
            Assert.AreEqual(teacherIdExpected, teacher.teacherID);
            Assert.AreEqual(loginExpected, teacher.login);
        }
    }
}
