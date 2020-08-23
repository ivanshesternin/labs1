using StudentsTesting1.Logic.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.IoC;
using StudentsTesting1.Logic.Groups;
using StudentsTesting1.Logic.Exams;
using StudentsTesting1.Logic.Results;
using StudentsTesting1.DataAccess;
using StudentsTesting1.Logic.Subjects;
using StudentsTesting1.Logic.Questions;

namespace StudentsTesting1.Controllers
{
    public class TeacherController
    {
        private Teacher teacher { get; set; }
        private IoCContainer IoC { get; set; } = new IoCContainer();
        private IDBAccess dbAccess { get; set; }
        private GroupAccess groupAccess { get; set; }
        private SubjectAccess subjectAccess { get; set; }
        private ResultAccess resultAccess { get; set; }
        private StudentAccess studentAccess { get; set; }
        private ExamAccess examAccess { get; set; }

        public TeacherController(Teacher Teacher)
        {
            IoC.RegisterObject<IGroup, Group>();
            IoC.RegisterObject<IExam, Exam>();
            IoC.RegisterObject<IResult, Result>();
            IoC.RegisterObject<IDBAccess, DBAccess>();
            dbAccess = new DBAccess();
            examAccess = new ExamAccess(dbAccess);
            resultAccess = new ResultAccess(dbAccess);
            studentAccess = new StudentAccess(dbAccess);
            groupAccess = new GroupAccess(dbAccess);
            subjectAccess = new SubjectAccess(dbAccess);
            teacher = Teacher;
        }

        // Constructor for test purpose
        public TeacherController(Teacher Teacher, ResultAccess ResultAccess, StudentAccess StudentAccess, ExamAccess ExamAccess)
        {
            IoC.RegisterObject<IGroup, Group>();
            IoC.RegisterObject<IExam, Exam>();
            IoC.RegisterObject<IResult, Result>();
            IoC.RegisterObject<IDBAccess, DBAccess>();
            examAccess = ExamAccess;
            resultAccess = ResultAccess;
            studentAccess = StudentAccess;
            teacher = Teacher;
        }
        public List<Exam> GetExams(int subjectID)
        {
            return examAccess.GetExamsOfSubject(subjectID);
        }
        public List<Group> GetGroups(int subjectID)
        {
            return groupAccess.GetGroupsOfSubject(subjectID);
        }

        public List<Subject> GetSubjects()
        {
            return subjectAccess.GetSubjectsOfTeacher(teacher.teacherID);
        }

        public bool CreateExam(string Title, int NumberOfQuestions, int Attempts, List<Question> Questions, Subject subject)
        {
            if (Questions.Count < NumberOfQuestions)
            {
                return false;
            }
            else
            {
                List<object> param = new List<object>();
                param.Add(Title);
                param.Add(NumberOfQuestions);
                param.Add(Attempts);
                Exam exam = IoC.ResolveObject(typeof(Exam), param) as Exam;
                for (int i = 0; i < Questions.Count; i++)
                {
                    exam.AddQuestion(Questions[i]);
                }
                examAccess.InsertExamToDB(exam, subject.id);
                return true;
            }
        }

        public void AssignExamToGroup(IExam exam, IGroup group)
        {
            group.AssignExam(exam);
        }

        public List<Result> CheckResults(Group group, Exam exam)
        {
            List<Result> allResults = resultAccess.GetResultsOfGroup(group.title, exam.id);
            List<Student> students = studentAccess.GetStudentsFromGroup(group.title);
            List<Result> bestResults = new List<Result>();
            foreach (Student student in students)
            {
                int score = 0;
                Result saved = null;
                foreach(Result result in allResults)
                {
                    if(result.student.studentID == student.studentID)
                    {
                        if (result.score > score)
                        {
                            score = result.score;
                            saved = result;
                        }
                    }
                }
                if (saved == null)
                {
                    bestResults.Add(new Result(student, 0, new List<AnsweredQuestion>()));
                }
                else
                {
                    bestResults.Add(saved);
                }

            }
            bestResults.OrderBy(r => r.student.lastName);
            return bestResults;
        }
    }
}
