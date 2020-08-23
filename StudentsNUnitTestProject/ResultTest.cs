using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using StudentsTesting1.Logic.Exams;
using StudentsTesting1.Logic.Questions;
using StudentsTesting1.Logic.Results;
using StudentsTesting1.Logic.Users;

namespace StudentsNUnitTestProject
{
    class ResultTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ResultConstructorTest()
        {
            //Arrange
            int expectedScore = 1;

            Exam exam = new Exam("SomeExam", 1, 3, 1);
            Student student = new Student("Ivan", "Ivanov", "Studak", "Zachotka", "TEST", "ivanov");
            Question question = new Question("Some question", "Correct answer", new List<String> { "Answer1", "Answer2" });
            AnsweredQuestion answeredQuestion = new AnsweredQuestion(question, "Correct answer");
            List<AnsweredQuestion> answeredQuestions = new List<AnsweredQuestion> { answeredQuestion };

            //Act
            Result result = new Result(exam, student, answeredQuestions);

            //Assert
            Assert.AreEqual(expectedScore, result.score);
        }

        [Test]
        public void ResultAnotherConstructorTest()
        {
            //Arrange
            int expectedScore = 1;

            Exam exam = new Exam("SomeExam", 1, 3, 1);
            Student student = new Student("Ivan", "Ivanov", "Studak", "Zachotka", "TEST", "ivanov");
            Question question = new Question("Some question", "Correct answer", new List<String> { "Answer1", "Answer2" });
            AnsweredQuestion answeredQuestion = new AnsweredQuestion(question, "Correct answer");
            List<AnsweredQuestion> answeredQuestions = new List<AnsweredQuestion>();

            //Act
            Result result = new Result(student, expectedScore, answeredQuestions);

            //Assert
            Assert.AreEqual(answeredQuestion.correctAnswer, answeredQuestion.actualAnswer);
            Assert.AreEqual(expectedScore, result.score);
        }
    }
}
