using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.Logic.Exams;
using StudentsTesting1.Logic.Groups;

namespace StudentsTesting1.Logic.Subjects
{
    public class Subject : ISubject
    {
        public int id { get; private set; }
        public string subjectTitle { get; private set; }
        public string teacherName { get; private set; }

        public Subject(string SubjectTitle)
        {
            subjectTitle = SubjectTitle;
        }
        public Subject(int ID, string SubjectTitle, string TeacherName)
        {
            id = ID;
            subjectTitle = SubjectTitle;
            teacherName = TeacherName;
        }
    }
}
