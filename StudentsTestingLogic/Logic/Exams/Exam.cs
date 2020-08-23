using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.Logic.Questions;
using StudentsTesting1.Logic.Results;

namespace StudentsTesting1.Logic.Exams
{
    public class Exam : IExam
    {
        public int id { get; protected set; }
        public string title { get; private set; }
        public int numberOfQuestions { get; private set; }
        public int attempts { get; private set; }
        public int subjectId { get; private set; }
        public List<IQuestion> questions { get; private set; } = new List<IQuestion>();

        public Exam(string Title, int Number, int Attempts, int SubjectID)
        {
            title = Title;
            numberOfQuestions = Number;
            attempts = Attempts;
            subjectId = SubjectID;
        }

        public Exam(int ID, string Title, int Number, int Attempts, int SubjectID)
        {
            id = ID;
            title = Title;
            numberOfQuestions = Number;
            attempts = Attempts;
            subjectId = SubjectID;
        }

        public void AddQuestion(IQuestion question)
        {
            questions.Add(question);
        }
    }
}
