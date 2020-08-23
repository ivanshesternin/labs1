using System;
using System.Collections.Generic;
using NUnit.Framework;
using StudentsTesting1.Logic.Users;
using StudentsTesting1.Logic.Results;
using StudentsTesting1.Logic.Exams;
using StudentsTesting1.Logic.Questions;
using StudentsTesting1.DataAccess;
using Moq;
using StudentsTesting1.Controllers;

namespace StudentsNUnitTestProject
{
    class StudentControllerTest
    {
        [Test]
        public void StudentControllerPassExamTest()
        {
            //Arrange
            bool isResultRecorded = false;

            var resultAccess = new Mock<ResultAccess>(new DBAccess());
            resultAccess.Setup(t => t.InsertResultToDB(It.IsAny<Result>(), It.IsAny<int>(), It.IsAny<string>())).Callback(() => isResultRecorded = true);

            Student student = new Student("Ivan", "Ivanov", "Studak", "Zachotka", "TEST", "ivanov");
            StudentController studentController = new StudentController(resultAccess.Object, student);
            Exam exam = new Exam("SomeExam", 1, 3, 1);
            Question question1 = new Question("Some question1", "Correct answer1", new List<String> { "Answer11", "Answer12" });
            Question question2 = new Question("Some question2", "Correct answer2", new List<String> { "Answer21", "Answer22" });
            List<Question> questions = new List<Question> { question1, question2 };
            List<String> answers = new List<String> { "Correct answer1", "Correct answer2" };

            //Act
            studentController.PassExam(exam, questions, answers);

            //Assert
            Assert.IsTrue(isResultRecorded);
        }

        [Test]
        public void StudentControllerCheckResultTest()
        {
            //Arrange
            bool isResultChecked = false;

            var resultAccess = new Mock<ResultAccess>(new DBAccess());
            resultAccess.Setup(t => t.GetResultOfStudent(It.IsAny<string>(), It.IsAny<int>())).Callback(() => isResultChecked = true);

            Student student = new Student("Ivan", "Ivanov", "Studak", "Zachotka", "TEST", "ivanov");
            StudentController studentController = new StudentController(resultAccess.Object, student);
            Exam exam = new Exam("SomeExam", 1, 3, 1);

            //Act
            studentController.CheckResult(exam);

            //Assert
            Assert.IsTrue(isResultChecked);
        }
    }
}
