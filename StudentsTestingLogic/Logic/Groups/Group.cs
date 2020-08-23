using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.Logic.Users;
using StudentsTesting1.Logic.Exams;
using StudentsTesting1.DataAccess;
using StudentsTesting1.IoC;
using StudentsTesting1.Logic.Subjects;

namespace StudentsTesting1.Logic.Groups
{
    public class Group : IGroup
    {
        public string title { get; private set; }
        public List<Subject> subjects { get; private set; }
        public List<Student> students { get; private set; } = new List<Student>();
        private IDBAccess dbAccess = new DBAccess().GetInstance();
        private ExamAccess examAccess;
        private GroupAccess groupAccess;

        public Group(string Title)
        {
            title = Title;
            examAccess = new ExamAccess(dbAccess);
            groupAccess = new GroupAccess(dbAccess);
        }

        // Constructor for unit-tests
        public Group(string Title, ExamAccess ExamAccess, GroupAccess GroupAccess)
        {
            title = Title;
            examAccess = ExamAccess;
            groupAccess = GroupAccess;
        }

        public virtual void AssignExam(IExam exam)
        {
            foreach (Student student in students)
            {
                examAccess.AssignExam(exam as Exam, student.studentID);
            }
        }

        public virtual void AddStudent(Student student)
        {
            students.Add(student);
        }

        public virtual void AssignSubject(Subject subject)
        {
            groupAccess.AddSubjectToGroup(this, subject);
        }

        public void SetSubjects(List<Subject> Subjects)
        {
            subjects = Subjects;
        }

        public string StringOfSubjects()
        {
            string str = "";
            if (subjects != null && subjects.Count > 0)
            {
                foreach (Subject s in subjects)
                {
                    str = str + s.subjectTitle + ", ";
                }
                str = str.Remove(str.Length - 2);
            }
            return str;
        }
    }
}
