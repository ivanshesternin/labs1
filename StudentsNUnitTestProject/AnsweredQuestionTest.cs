using System;
using System.Collections.Generic;
using NUnit.Framework;
using StudentsTesting1.Logic.Questions;

namespace StudentsNUnitTestProject
{
    class AnsweredQuestionTest
    {
        [Test]
        public void AnsweredQuestionConstructorTest()
        {
            //Arrange
            string expectedQuestion = "Question";
            string expectedAnswer = "Correct answer";
            List<string> falseAnswers = new List<String> { "Answer1", "Answer2" };

            //Act
            AnsweredQuestion answeredQuestion = new AnsweredQuestion(expectedQuestion, expectedAnswer, falseAnswers, expectedAnswer);

            //Assert
            Assert.AreEqual(expectedQuestion, answeredQuestion.question);
            Assert.AreEqual(expectedAnswer, answeredQuestion.correctAnswer);
            Assert.AreEqual(expectedAnswer, answeredQuestion.actualAnswer);
            Assert.AreEqual(falseAnswers[0], answeredQuestion.falseAnswers[0]);
            Assert.AreEqual(falseAnswers[1], answeredQuestion.falseAnswers[1]);
        }

        [Test]
        public void ResultAnotherConstructorTest()
        {
            //Arrange
            int expectedId = 1;
            string expectedQuestion = "Question";
            string expectedAnswer = "Correct answer";
            List<string> falseAnswers = new List<String> { "Answer1", "Answer2" };
            Question question = new Question(expectedId, expectedQuestion, expectedAnswer, falseAnswers);

            //Act
            AnsweredQuestion answeredQuestion = new AnsweredQuestion(question, expectedAnswer);

            //Assert
            Assert.AreEqual(expectedQuestion, answeredQuestion.question);
            Assert.AreEqual(expectedAnswer, answeredQuestion.correctAnswer);
            Assert.AreEqual(expectedAnswer, answeredQuestion.actualAnswer);
            Assert.AreEqual(falseAnswers[0], answeredQuestion.falseAnswers[0]);
            Assert.AreEqual(falseAnswers[1], answeredQuestion.falseAnswers[1]);
        }
    }
}
