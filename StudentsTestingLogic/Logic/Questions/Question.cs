using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsTesting1.Logic.Questions
{
    public class Question : IQuestion
    {
        public int id { get; protected set; }
        public string question { get; protected set; }
        public string correctAnswer { get; protected set; }
        public List<string> falseAnswers { get; protected set; } = new List<string>();

        public Question(string Question, string CorrectAnswer, List<string> FalseAnswers)
        {
            question = Question;
            correctAnswer = CorrectAnswer;
            falseAnswers = FalseAnswers;
        }
        public Question(int ID, string Question, string CorrectAnswer, List<string> FalseAnswers)
        {
            id = ID;
            question = Question;
            correctAnswer = CorrectAnswer;
            falseAnswers = FalseAnswers;
        }
        public Question() { }
    }
}
