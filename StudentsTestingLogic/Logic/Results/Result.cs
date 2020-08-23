using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.Logic.Exams;
using StudentsTesting1.Logic.Users;
using StudentsTesting1.Logic.Questions;

namespace StudentsTesting1.Logic.Results
{
    public class Result : IResult
    {
        public Exam exam { get; protected set; }
        public Student student { get; protected set; }
        public List<AnsweredQuestion> answeredQuestions { get; private set; }
        public int score { get; protected set; }

        public Result(Exam Exam, Student Student, List<AnsweredQuestion> AnsweredQuestions)
        {
            exam = Exam;
            student = Student;
            answeredQuestions = AnsweredQuestions;
            score = 0;
            foreach(AnsweredQuestion question in answeredQuestions)
            {
                if (question.correctAnswer == question.actualAnswer)
                {
                    score++;
                }
            }
        }

        public Result(Student Student, int Score, List<AnsweredQuestion> AnsweredQuestions)
        {
            student = Student;
            score = Score;
            answeredQuestions = AnsweredQuestions;
        }
    }
}
