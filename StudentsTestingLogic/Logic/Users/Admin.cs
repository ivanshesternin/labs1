using System.Security.Cryptography;
using System.Text;
using StudentsTesting1.Logic.Groups;
using StudentsTesting1.Logic.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.IoC;
using StudentsTesting1.DataAccess;

namespace StudentsTesting1.Logic.Users
{
    public class Admin : User
    {
        public string adminID { get; private set; }
        private IoCContainer IoC { get; set; } = new IoCContainer(); 
        private IDBAccess dbAccess { get; set; }
        private TeacherAccess teacherAccess { get; set; }
        private SubjectAccess subjectAccess { get; set; }
        private GroupAccess groupAccess { get; set; }
        private AccountAccess accountAccess { get; set; }
        private StudentAccess studentAccess { get; set; }

        public Admin(string FirstName, string LastName, string ID) : base(FirstName, LastName)
        {
            adminID = ID;
            IoC.RegisterObject<IDBAccess, DBAccess>();
            IoC.RegisterObject<ISubject, Subject>();
            IoC.RegisterObject<IGroup, Group>();
            dbAccess = new DBAccess();
            teacherAccess = new TeacherAccess(dbAccess);
            groupAccess = new GroupAccess(dbAccess);
            subjectAccess = new SubjectAccess(dbAccess);
            accountAccess = new AccountAccess(dbAccess);
            studentAccess = new StudentAccess(dbAccess);
        }

        // Special constructor for tests
        public Admin(string FirstName, string LastName, string ID, TeacherAccess TeacherAccess, GroupAccess GroupAccess, SubjectAccess SubjectAccess) : base(FirstName, LastName)
        {
            adminID = ID;
            IoC.RegisterObject<ISubject, Subject>();
            IoC.RegisterObject<IGroup, Group>();
            teacherAccess = TeacherAccess;
            groupAccess = GroupAccess;
            subjectAccess = SubjectAccess;
        }
        public List<Teacher> GetTeachers()
        {
            return teacherAccess.GetTeachersFromDB();
        }

        public void CreateTeacher(string firstName, string lastName, string ID, string login, string password)
        {
            accountAccess.RegisterTeacher(login, password, new Teacher(firstName, lastName, ID, login));
        }

        public void CreateSubject(string title, string teacherId)
        {
            List<object> param = new List<object> { title };
            subjectAccess.InsertSubjectToDB(IoC.ResolveObject(typeof(ISubject), param) as Subject, teacherId);
        }

        public void CreateGroup(string title)
        {
            List<object> param = new List<object> { title };
            groupAccess.InsertGroupToDB(IoC.ResolveObject(typeof(IGroup), param) as Group);
        }

        public List<Subject> GetSubjectsOfGroup(Group group)
        {
            return subjectAccess.GetSubjectsOfGroup(group.title);
        }

        public void CreateStudent(string firstName, string lastName, string studentID, string recordBook, string groupTitle, string login, string password)
        {
            Student student = new Student(firstName, lastName, studentID, recordBook, groupTitle, login);
            accountAccess.RegisterStudent(password, student);
        }

        public void AssignSubjectToGroup(Subject subject, Group group)
        {
            group.AssignSubject(subject);
        }

        public List<Group> GetGroups()
        {
            return groupAccess.GetGroupsFromDB();
        }

        public List<Student> GetStudents()
        {
            return studentAccess.GetAllStudents();
        }

        public List<Subject> GetSubjects()
        {
            return subjectAccess.GetAllSubjects();
        }
    }
}
