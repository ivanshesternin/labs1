using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.Logic.Results;
using StudentsTesting1.Logic.Exams;
using StudentsTesting1.Logic.Users;
using StudentsTesting1.Logic.Groups;
using StudentsTesting1.IoC;
using StudentsTesting1.DataAccess;
using StudentsTesting1.Logic.Questions;
using StudentsTesting1.Logic.Subjects;

namespace StudentsTesting1.Controllers
{
    public class StudentController
    {
        private Student student { get; set; }
        private IoCContainer IoC { get; set; } = new IoCContainer();
        private IDBAccess dbAccess { get; set; }
        private ResultAccess resultAccess { get; set; }
        private SubjectAccess subjectAccess { get; set; }
        private ExamAccess examAccess { get; set; }

        public StudentController(Student Student)
        {
            IoC.RegisterObject<IGroup, Group>();
            IoC.RegisterObject<IExam, Exam>();
            IoC.RegisterObject<IDBAccess, DBAccess>();
            dbAccess = new DBAccess();
            resultAccess = new ResultAccess(dbAccess);
            examAccess = new ExamAccess(dbAccess);
            subjectAccess = new SubjectAccess(dbAccess);
            student = Student;
        }

        // Constructor for test purposes
        public StudentController(ResultAccess ResultAccess, Student Student)
        {
            IoC.RegisterObject<IGroup, Group>();
            IoC.RegisterObject<IExam, Exam>();
            IoC.RegisterObject<IDBAccess, DBAccess>();
            student = Student;
        }

        public List<Subject> GetSubjectsOfStudent()
        {
            return subjectAccess.GetSubjectsOfStudent(student.studentID);
        }

        public List<Exam> GetExamsOfStudent()
        {
            return examAccess.GetExamsOfStudent(student.studentID);
        }

        public Result GetResultsOfStudent(int examID)
        {
            return resultAccess.GetResultOfStudent(student.studentID, examID);
        }
        public void PassExam(Exam exam, List<Question> questions, List<string> answers)
        {
            List<AnsweredQuestion> answeredQuestions = new List<AnsweredQuestion>();
            for (int i = 0; i < questions.Count; i++)
            {
                AnsweredQuestion answeredQuestion = new AnsweredQuestion(questions[i] as Question, answers[i]);
                answeredQuestions.Add(answeredQuestion);
            }
            List<object> param = new List<object>();
            param.Add(exam);
            param.Add(this.student);
            param.Add(answeredQuestions);
            resultAccess.InsertResultToDB(IoC.ResolveObject(typeof(Result), param) as Result, exam.id, student.studentID);
        }

        public Result CheckResult(Exam exam)
        {
            return resultAccess.GetResultOfStudent(student.studentID, exam.id);
        }
    }
}
