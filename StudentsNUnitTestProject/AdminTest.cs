using StudentsTesting1.Logic.Groups;
using StudentsTesting1.Logic.Subjects;
using NUnit.Framework;
using StudentsTesting1.IoC;
using StudentsTesting1.Logic.Users;
using StudentsTesting1.DataAccess;
using Moq;

namespace StudentsNUnitTestProject
{
    class AdminTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AdminCreateTeacherTest()
        {
            //Arrange
            bool isTeacherCreated = false;

            var teacherAccess = new Mock<TeacherAccess>(new DBAccess());
            teacherAccess.Setup(t => t.InsertTeacherToDB(It.IsAny<Teacher>())).Callback(() => isTeacherCreated = true);
            GroupAccess groupAccess = new GroupAccess(new DBAccess());
            SubjectAccess subjectAccess = new SubjectAccess(new DBAccess());

            Admin admin = new Admin("Admin", "Adminov", "adminId", teacherAccess.Object, groupAccess, subjectAccess);

            //Act
            admin.CreateTeacher("Petro", "Petrov", "ID","petrov","abc123");

            //Assert
            Assert.IsTrue(isTeacherCreated);
        }

        [Test]
        public void AdminCreateSubjectTest()
        {
            //Arrange
            bool isSubjectCreated = false;

            var subjectAccess = new Mock<SubjectAccess>(new DBAccess());
            subjectAccess.Setup(t => t.InsertSubjectToDB(It.IsAny<Subject>(), It.IsAny<string>())).Callback(() => isSubjectCreated = true);
            GroupAccess groupAccess = new GroupAccess(new DBAccess());
            TeacherAccess teacherAccess = new TeacherAccess(new DBAccess());

            Admin admin = new Admin("Admin", "Adminov", "adminId", teacherAccess, groupAccess, subjectAccess.Object);
            Teacher teacher = new Teacher("Petro", "Petrov", "ID","petrov");

            //Act
            admin.CreateSubject("Subject", "ID");

            //Assert
            Assert.IsTrue(isSubjectCreated);
        }

        [Test]
        public void AdminCreateGroupTest()
        {
            //Arrange
            bool isGroupCreated = false;

            var groupAccess = new Mock<GroupAccess>(new DBAccess());
            groupAccess.Setup(t => t.InsertGroupToDB(It.IsAny<Group>())).Callback(() => isGroupCreated = true);
            SubjectAccess subjectAccess = new SubjectAccess(new DBAccess());
            TeacherAccess teacherAccess = new TeacherAccess(new DBAccess());

            Admin admin = new Admin("Admin", "Adminov", "adminId", teacherAccess, groupAccess.Object, subjectAccess);

            //Act
            admin.CreateGroup("TEST");

            //Assert
            Assert.IsTrue(isGroupCreated);
        }

        [Test]
        public void AdminCreateStudentTest()
        {
            //Arrange
            bool isStudentCreated = false;

            var groupMock = new Mock<Group>("TEST");
            groupMock.Setup(t => t.AddStudent(It.IsAny<Student>())).Callback(() => isStudentCreated = true);
            SubjectAccess subjectAccess = new SubjectAccess(new DBAccess());
            TeacherAccess teacherAccess = new TeacherAccess(new DBAccess());
            GroupAccess groupAccess = new GroupAccess(new DBAccess());

            Admin admin = new Admin("Admin", "Adminov", "adminId", teacherAccess, groupAccess, subjectAccess);

            //Act
            admin.CreateStudent("Ivan", "Ivanov", "Studak", "Zachotka", "Group", "ivanov", "ivanov");

            //Assert
            Assert.IsTrue(isStudentCreated);
        }

        [Test]
        public void AdminAssignSubjectToGroupTest()
        {
            //Arrange
            bool isSubjectAssigned = false;

            var groupMock = new Mock<Group>("TEST");
            groupMock.Setup(t => t.AssignSubject(It.IsAny<Subject>())).Callback(() => isSubjectAssigned = true);
            SubjectAccess subjectAccess = new SubjectAccess(new DBAccess());
            TeacherAccess teacherAccess = new TeacherAccess(new DBAccess());
            GroupAccess groupAccess = new GroupAccess(new DBAccess());

            Admin admin = new Admin("Admin", "Adminov", "adminId", teacherAccess, groupAccess, subjectAccess);
            Subject subject = new Subject("Subject");

            //Act
            admin.AssignSubjectToGroup(subject, groupMock.Object);

            //Assert
            Assert.IsTrue(isSubjectAssigned);
        }
    }
}
