using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.Logic.Users;

namespace StudentsTesting1.DataAccess
{
    public class TeacherAccess
    {
        private IDBAccess dbaccess;
        public TeacherAccess(IDBAccess iDBAccess)
        {
            dbaccess = iDBAccess;
        }
        public virtual void InsertTeacherToDB(Teacher teacher)
        {
            string command = "INSERT INTO TEACHERS(\"FIRST_NAME\",\"LAST_NAME\",\"TEACHERID\") VALUES " +
                "(\"" + teacher.firstName + "\", \"" + teacher.lastName + "\", \"" + teacher.teacherID + "\");";
            dbaccess.SQLExecute(command);
        }

        public List<Teacher> GetTeachersFromDB()
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM TEACHERS JOIN ACCOUNTS ON TEACHERS.ID = ACCOUNTS.USER_ID WHERE ACCOUNTS.ROLE = \"teacher\"");
            List<Teacher> teachers = new List<Teacher>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    teachers.Add(new Teacher(Convert.ToString(dataTable.Rows[i].ItemArray[1]), Convert.ToString(dataTable.Rows[i].ItemArray[2]), 
                        Convert.ToString(dataTable.Rows[i].ItemArray[3]), Convert.ToString(dataTable.Rows[i].ItemArray[5])));
                }
            }
            return teachers;
        }

        public Teacher GetTeacherByID(int id)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM TEACHERS JOIN ACCOUNTS ON TEACHERS.ID = ACCOUNTS.USER_ID WHERE ACCOUNTS.ROLE = \"teacher\" AND TEACHERS.ID = " + id + ";");
            if (dataTable.Rows.Count > 0)
            {
                Teacher teacher = new Teacher(Convert.ToString(dataTable.Rows[0].ItemArray[1]), Convert.ToString(dataTable.Rows[0].ItemArray[2]), 
                    Convert.ToString(dataTable.Rows[0].ItemArray[3]), Convert.ToString(dataTable.Rows[0].ItemArray[5]));
                return teacher;
            }
            return null;
        }
    }
}
