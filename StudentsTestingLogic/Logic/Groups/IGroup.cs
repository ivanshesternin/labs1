using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.Logic.Users;
using StudentsTesting1.Logic.Exams;
using StudentsTesting1.Logic.Subjects;

namespace StudentsTesting1.Logic.Groups
{
    public interface IGroup
    {
        public void AssignExam(IExam exam);
        public void AddStudent(Student student);
        public void AssignSubject(Subject subject);
    }
}
