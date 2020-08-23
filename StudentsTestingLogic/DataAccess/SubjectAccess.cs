using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.Logic.Subjects;
using System.Data;

namespace StudentsTesting1.DataAccess
{
    public class SubjectAccess
    {
        private IDBAccess dbaccess;

        public SubjectAccess(IDBAccess iDBAccess)
        {
            dbaccess = iDBAccess;
        }
        public virtual void InsertSubjectToDB(Subject subject, string teacherID)
        {
            string command = "INSERT INTO SUBJECTS(\"TITLE\",\"TEACHER_ID\") VALUES " +
                "(\"" + subject.subjectTitle + "\", (SELECT ID AS TEACHER_ID FROM TEACHERS WHERE TEACHERID = \"" + teacherID + "\"));";
            dbaccess.SQLExecute(command);
        }

        public List<Subject> GetSubjectsOfTeacher(string teacherID)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM SUBJECTS JOIN TEACHERS ON SUBJECTS.TEACHER_ID = TEACHERS.ID WHERE TEACHERS.TEACHERID = \"" + teacherID + "\";");
            List<Subject> subjects = new List<Subject>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    subjects.Add(new Subject(Convert.ToInt32(dataTable.Rows[i].ItemArray[0]),Convert.ToString(dataTable.Rows[i].ItemArray[1]), ""));
                }
            }
            return subjects;
        }
        public List<Subject> GetSubjectsOfGroup(string groupTitle)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM SUBJECTS JOIN GROUPSTOSUBJECTS ON GROUPSTOSUBJECTS.SUBJECT_ID = SUBJECTS.ID JOIN GROUPS ON GROUPSTOSUBJECTS.GROUP_ID = GROUPS.ID WHERE GROUPS.TITLE = \"" + groupTitle + "\";");
            List<Subject> subjects = new List<Subject>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    subjects.Add(new Subject(Convert.ToString(dataTable.Rows[i].ItemArray[1])));
                }
            }
            return subjects;
        }

        public List<Subject> GetSubjectsOfStudent(string studentID)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM SUBJECTS JOIN GROUPSTOSUBJECTS ON GROUPSTOSUBJECTS.SUBJECT_ID = SUBJECTS.ID JOIN GROUPS ON GROUPSTOSUBJECTS.GROUP_ID = GROUPS.ID JOIN STUDENTS ON STUDENTS.GROUP_ID = GROUPS.ID WHERE STUDENTS.STUDENTID = \"" + studentID + "\";");
            List<Subject> subjects = new List<Subject>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    subjects.Add(new Subject(Convert.ToString(dataTable.Rows[i].ItemArray[1])));
                }
            }
            return subjects;
        }

        public Subject GetSubjectByID(int id)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM SUBJECTS WHERE ID = " + id + ";");
            if (dataTable.Rows.Count > 0)
            {
                Subject subject = new Subject(Convert.ToString(dataTable.Rows[0].ItemArray[1]));
                return subject;
            }
            Subject emptySubject = new Subject("Subject not found");
            return emptySubject;
        }

        public List<Subject> GetAllSubjects()
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM SUBJECTS JOIN TEACHERS ON SUBJECTS.TEACHER_ID = TEACHERS.ID");
            List<Subject> subjects = new List<Subject>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    subjects.Add(new Subject(Convert.ToInt32(dataTable.Rows[i].ItemArray[0]),Convert.ToString(dataTable.Rows[i].ItemArray[1]),
                        Convert.ToString(dataTable.Rows[i].ItemArray[4]) + " " + Convert.ToString(Convert.ToString(dataTable.Rows[i].ItemArray[5]))));
                }
            }
            return subjects;
        }
    }
}
