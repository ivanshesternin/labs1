using System;
using System.Collections.Generic;
using NUnit.Framework;
using StudentsTesting1.Logic.Groups;
using StudentsTesting1.DataAccess;
using Moq;
using StudentsTesting1.Logic.Exams;
using StudentsTesting1.Logic.Users;
using StudentsTesting1.Logic.Subjects;

namespace StudentsNUnitTestProject
{
    class GroupTest
    {
        [Test]
        public void AssinExamTest()
        {
            //Arrange
            bool isExamAssigned = false;

            var examAccess = new Mock<ExamAccess>(new DBAccess());
            examAccess.Setup(t => t.AssignExam(It.IsAny<Exam>(),It.IsAny<string>())).Callback(() => isExamAssigned = true);
            GroupAccess groupAccess = new GroupAccess(new DBAccess());
            Exam exam = new Exam("Exam", 1, 3, 1);
            Student student = new Student("Ivan", "Ivanov", "Studak", "Zachotka", "TEST", "ivanov");

            Group group = new Group("TEST", examAccess.Object, groupAccess);
            group.AddStudent(student);

            //Act
            group.AssignExam(exam);

            //Assert
            Assert.IsTrue(isExamAssigned);
        }

        [Test]
        public void AddStudentTest()
        {
            //Arrange
            Student student = new Student("Ivan", "Ivanov", "Studak", "Zachotka", "TEST", "ivanov");

            Group group = new Group("TEST");

            //Act
            group.AddStudent(student);

            //Assert
            Assert.AreSame(student, group.students[0]);
        }

        [Test]
        public void AssinSubjectTest()
        {
            //Arrange
            bool isSubjectAssigned = false;

            var groupAccess = new Mock<GroupAccess>(new DBAccess());
            groupAccess.Setup(t => t.AddSubjectToGroup(It.IsAny<Group>(), It.IsAny<Subject>())).Callback(() => isSubjectAssigned = true);
            ExamAccess examAccess = new ExamAccess(new DBAccess());
            Subject subject = new Subject("Subject");

            Group group = new Group("TEST", examAccess, groupAccess.Object);

            //Act
            group.AssignSubject(subject);

            //Assert
            Assert.IsTrue(isSubjectAssigned);
        }
    }
}
