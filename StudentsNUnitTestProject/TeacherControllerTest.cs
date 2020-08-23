using System;
using System.Collections.Generic;
using NUnit.Framework;
using StudentsTesting1.Logic.Users;
using StudentsTesting1.Logic.Results;
using StudentsTesting1.Logic.Exams;
using StudentsTesting1.Logic.Questions;
using StudentsTesting1.Logic.Subjects;
using StudentsTesting1.DataAccess;
using StudentsTesting1.Controllers;
using Moq;
using StudentsTesting1.Logic.Groups;

namespace StudentsNUnitTestProject
{
    class TeacherControllerTest
    {
        [Test]
        public void TeacherControllerCreateExamTest()
        {
            //Arrange
            var examAccess = new Mock<ExamAccess>(new DBAccess());
            examAccess.Setup(t => t.InsertExamToDB(It.IsAny<Exam>(), It.IsAny<int>()));

            Teacher teacher = new Teacher("Petro", "Petrov", "ID", "petrov");
            ResultAccess resultAccess = new ResultAccess(new DBAccess());
            StudentAccess studentAccess = new StudentAccess(new DBAccess());
            TeacherController teacherController = new TeacherController(teacher, resultAccess, studentAccess, examAccess.Object);

            Question question1 = new Question("Some question1", "Correct answer1", new List<String> { "Answer11", "Answer12" });
            Question question2 = new Question("Some question2", "Correct answer2", new List<String> { "Answer21", "Answer22" });
            List<Question> questions = new List<Question> { question1, question2 };
            Subject subject = new Subject("Subject");

            //Act
            bool isExamCreated = teacherController.CreateExam("SomeExam", 1, 3, questions, subject);

            //Assert
            Assert.IsTrue(isExamCreated);
        }

        [Test]
        public void TeacherControllerAssignExamToGroupTest()
        {
            //Arrange
            bool isExamAssigned = false;

            var group = new Mock<Group>("Test");
            group.Setup(t => t.AssignExam(It.IsAny<Exam>())).Callback(() => isExamAssigned = true);

            Teacher teacher = new Teacher("Petro", "Petrov", "ID", "petrov");
            TeacherController teacherController = new TeacherController(teacher);
            Exam exam = new Exam("SomeExam", 1, 3, 1);

            //Act
            teacherController.AssignExamToGroup(exam, group.Object);

            //Assert
            Assert.IsTrue(isExamAssigned);
        }

        [Test]
        public void TeacherControllerCheckResultsTest()
        {
            //Arrange
            Student student1 = new Student("Ivan", "Ivanov", "Studak1", "Zachotka1", "TEST", "ivanov");
            Student student2 = new Student("Katya", "Katina", "Studak2", "Zachotka2", "TEST", "katerina");
            Student student3 = new Student("Denis", "Denisov", "Studak3", "Zachotka3", "TEST", "denis");

            Result IvanovResult1 = new Result(student1, 2, new List<AnsweredQuestion>());
            Result IvanovResult2 = new Result(student1, 1, new List<AnsweredQuestion>());
            Result KatyaResult1 = new Result(student2, 1, new List<AnsweredQuestion>());
            Result KatyaResult2 = new Result(student2, 2, new List<AnsweredQuestion>());

            var resultAccess = new Mock<ResultAccess>(new DBAccess());
            resultAccess.Setup(t => t.GetResultsOfGroup(It.IsAny<string>(), It.IsAny<int>())).Returns(new List<Result> { IvanovResult1, IvanovResult2,
            KatyaResult1, KatyaResult2});
            var studentAccess = new Mock<StudentAccess>(new DBAccess());
            studentAccess.Setup(t => t.GetStudentsFromGroup(It.IsAny<string>())).Returns(new List<Student> { student1, student2, student3 });
            ExamAccess examAccess = new ExamAccess(new DBAccess());

            Teacher teacher = new Teacher("Petro", "Petrov", "ID", "petrov");
            TeacherController teacherController = new TeacherController(teacher, resultAccess.Object, studentAccess.Object, examAccess);
            Exam exam = new Exam("SomeExam", 1, 3, 1);
            Group group = new Group("Test");

            //Act
            List<Result> results = teacherController.CheckResults(group, exam);

            //Assert
            Assert.AreEqual(3, results.Count);
            Assert.AreSame(IvanovResult1, results[0]);
            Assert.AreSame(KatyaResult2, results[1]);
            Assert.AreEqual(0, results[2].score);
        }
    }
}
