using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.Logic.Groups;
using StudentsTesting1.Logic.Users;
using System.Data;

namespace StudentsTesting1.DataAccess
{
    public class StudentAccess
    {
        private IDBAccess dbaccess;

        public StudentAccess(IDBAccess iDBAccess)
        {
            dbaccess = iDBAccess;
        }
        public void InsertStudentToDB(Student student, string groupTitle)
        {
            string command = "INSERT INTO STUDENTS(\"FIRST_NAME\",\"LAST_NAME\",\"STUDENTID\",\"RECORD_BOOK\",\"GROUP_ID\") VALUES " +
                "(\"" + student.firstName + "\", \"" + student.lastName + "\", \"" + student.studentID + "\", \"" + student.recordBook + "\", " +
                "(SELECT ID AS GROUP_ID FROM GROUPS WHERE TITLE = \"" + groupTitle + "\"));";
            dbaccess.SQLExecute(command);
        }

        public virtual List<Student> GetStudentsFromGroup(string groupTitle)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM STUDENTS JOIN ACCOUNTS ON ACCOUNTS.USER_ID = STUDENTS.ID" +
                "JOIN GROUPS ON STUDENTS.GROUP_ID = GROUPS.ID WHERE ROLE = \"student\" AND GROUPS.TITLE = \"" + groupTitle + "\";");
            List<Student> students = new List<Student>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    students.Add(new Student(Convert.ToString(dataTable.Rows[i].ItemArray[1]), Convert.ToString(dataTable.Rows[i].ItemArray[2]),
                        Convert.ToString(dataTable.Rows[i].ItemArray[3]), Convert.ToString(dataTable.Rows[i].ItemArray[4]), Convert.ToString(dataTable.Rows[i].ItemArray[12]),
                        Convert.ToString(dataTable.Rows[i].ItemArray[7])));
                }
            }
            return students;
        }

        public Student GetStudentByID(int id)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM STUDENTS JOIN ACCOUNTS ON ACCOUNTS.USER_ID = STUDENTS.ID " +
                "JOIN GROUPS ON STUDENTS.GROUP_ID = GROUPS.ID WHERE ROLE = \"student\" AND STUDENTS.ID = " + id + ";");
            if (dataTable.Rows.Count > 0)
            {
                Student student = new Student(Convert.ToString(dataTable.Rows[0].ItemArray[1]), Convert.ToString(dataTable.Rows[0].ItemArray[2]),
                        Convert.ToString(dataTable.Rows[0].ItemArray[3]), Convert.ToString(dataTable.Rows[0].ItemArray[4]), Convert.ToString(dataTable.Rows[0].ItemArray[12]),
                        Convert.ToString(dataTable.Rows[0].ItemArray[7]));
                return student;
            }
            return null;
        }

        public List<Student> GetAllStudents()
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM STUDENTS JOIN ACCOUNTS ON ACCOUNTS.USER_ID = STUDENTS.ID JOIN GROUPS ON GROUPS.ID = STUDENTS.GROUP_ID WHERE ROLE = \"student\"");
            List<Student> students = new List<Student>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    students.Add(new Student(Convert.ToString(dataTable.Rows[i].ItemArray[1]), Convert.ToString(dataTable.Rows[i].ItemArray[2]),
                        Convert.ToString(dataTable.Rows[i].ItemArray[3]), Convert.ToString(dataTable.Rows[i].ItemArray[4]), Convert.ToString(dataTable.Rows[i].ItemArray[12]),
                        Convert.ToString(dataTable.Rows[i].ItemArray[7])));
                }
            }
            return students;
        }
    }
}
