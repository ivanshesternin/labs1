using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.Logic.Exams;
using StudentsTesting1.Logic.Subjects;
using StudentsTesting1.Logic.Questions;
using StudentsTesting1.Logic.Groups;
using StudentsTesting1.Logic.Results;
using StudentsTesting1.IoC;

namespace StudentsTesting1.Logic.Users
{
    public class Teacher : User
    {
        public string teacherID { get; private set; }
        public string login { get; private set; }

        public Teacher(string FirstName, string LastName, string ID, string Login) : base(FirstName, LastName)
        {
            teacherID = ID;
            login = Login;
        }
    }
}
