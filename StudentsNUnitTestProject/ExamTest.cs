using System;
using System.Collections.Generic;
using NUnit.Framework;
using StudentsTesting1.Logic.Exams;
using StudentsTesting1.Logic.Questions;

namespace StudentsNUnitTestProject
{
    class ExamTest
    {
        [Test]
        public void AddQuestionTest()
        {
            //Arrange
            Exam exam = new Exam("Exam", 1, 3, 1);

            Question question = new Question("Question", "Correct answer", new List<String> { "Answer1", "Answer2" });

            //Act
            exam.AddQuestion(question);

            //Assert
            Assert.AreSame(question, exam.questions[0]);
        }
    }
}
