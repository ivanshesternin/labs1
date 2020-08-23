using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsTesting1.Logic.Questions
{
    public class AnsweredQuestion : Question, IQuestion
    {
        public string actualAnswer { get; private set; }

        public AnsweredQuestion(string Question, string CorrectAnswer, List<string> FalseAnswers, string ActualAnswer) : base(Question, CorrectAnswer, FalseAnswers)
        {
            actualAnswer = ActualAnswer;
        }

        public AnsweredQuestion(Question Q, string answer)
        {
            question = Q.question;
            correctAnswer = Q.correctAnswer;
            falseAnswers = Q.falseAnswers;
            actualAnswer = answer;
        }
    }
}
