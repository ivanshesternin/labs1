using System;
using System.Collections.Generic;
using NUnit.Framework;
using StudentsTesting1.Logic.Questions;

namespace StudentsNUnitTestProject
{
    class QuestionTest
    {
        [Test]
        public void QuestionConstructorTest()
        {
            //Arrange
            string expectedQuestion = "Question";
            string expectedAnswer = "Correct answer";
            List<string> falseAnswers = new List<String> { "Answer1", "Answer2" };

            //Act
            Question question = new Question(expectedQuestion, expectedAnswer, falseAnswers);

            //Assert
            Assert.AreEqual(expectedQuestion, question.question);
            Assert.AreEqual(expectedAnswer, question.correctAnswer);
            Assert.AreEqual(falseAnswers[0], question.falseAnswers[0]);
            Assert.AreEqual(falseAnswers[1], question.falseAnswers[1]);
        }

        [Test]
        public void QuestionAnotherConstructorTest()
        {
            //Arrange
            int expectedId = 1;
            string expectedQuestion = "Question";
            string expectedAnswer = "Correct answer";
            List<string> falseAnswers = new List<String> { "Answer1", "Answer2" };

            //Act
            Question question = new Question(expectedId,expectedQuestion, expectedAnswer, falseAnswers);

            //Assert
            Assert.AreEqual(expectedId, question.id);
            Assert.AreEqual(expectedQuestion, question.question);
            Assert.AreEqual(expectedAnswer, question.correctAnswer);
            Assert.AreEqual(falseAnswers[0], question.falseAnswers[0]);
            Assert.AreEqual(falseAnswers[1], question.falseAnswers[1]);
        }
    }
}
