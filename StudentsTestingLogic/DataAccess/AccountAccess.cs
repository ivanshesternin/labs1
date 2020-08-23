using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.Logic.Users;
using StudentsTesting1.Logic.Accounts;
using System.Data;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace StudentsTesting1.DataAccess
{
    public class AccountAccess : DbContext
    {
        private IDBAccess dbaccess;
        private StudentAccess studentAccess;
        private TeacherAccess teacherAccess;

        public AccountAccess(IDBAccess iDBAccess)
        {
            dbaccess = iDBAccess;
            studentAccess = new StudentAccess(dbaccess);
            teacherAccess = new TeacherAccess(dbaccess);
        }
        public void RegisterStudent(string password, Student student)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.UTF8.GetBytes(password);
            string passwordHash = Encoding.UTF8.GetString(sha1.ComputeHash(data));
            studentAccess.InsertStudentToDB(student, student.groupTitle);
            string command = "INSERT INTO ACCOUNTS(\"LOGIN\",\"PASSWORD_HASH\", \"ROLE\", \"USER_ID\") VALUES " +
                "(\"" + student.login + "\", \"" + passwordHash + "\", \"student\", (SELECT MAX(ID) FROM STUDENTS));";
            dbaccess.SQLExecute(command);
        }

        public void RegisterTeacher(string login, string password, Teacher teacher)
        {
            teacherAccess.InsertTeacherToDB(teacher);
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.UTF8.GetBytes(password);
            string passwordHash = Encoding.UTF8.GetString(sha1.ComputeHash(data));
            string command = "INSERT INTO ACCOUNTS(\"LOGIN\",\"PASSWORD_HASH\", \"ROLE\", \"USER_ID\") VALUES " +
                "(\"" + login + "\", \"" + passwordHash + "\", \"teacher\", (SELECT MAX(ID) FROM TEACHERS));";
            dbaccess.SQLExecute(command);
        }
        public Account TryToLogin(string login, string passwordHash)
        {
            DataTable dataTable = dbaccess.SQLGetTableData
                ("SELECT * FROM ACCOUNTS WHERE LOGIN = \"" + login + "\" AND PASSWORD_HASH = \"" + passwordHash + "\";");
            if (dataTable.Rows.Count > 0)
            {
                string role = Convert.ToString(dataTable.Rows[0].ItemArray[3]);
                switch (role)
                {
                    case ("admin"):
                        {
                            return new Account(Convert.ToInt32(dataTable.Rows[0].ItemArray[0]), login, role);
                        }
                    case ("teacher"):
                        {
                            return new Account(Convert.ToInt32(dataTable.Rows[0].ItemArray[0]), login, role);
                        }
                    case ("student"):
                        {
                            return new Account(Convert.ToInt32(dataTable.Rows[0].ItemArray[0]), login, role);
                        }
                    default: return null;
                }
            }
            return null;
        }

        public int GetUserId(int accountId, string role)
        {
            DataTable dataTable = dbaccess.SQLGetTableData
                ("SELECT USER_ID FROM ACCOUNTS WHERE ID = " + accountId + " AND ROLE = \"" + role + "\";");
            if (dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
            }
            else return 0;
        }
    }
}
