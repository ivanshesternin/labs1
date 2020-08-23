using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using StudentsTesting1.DataAccess;
using StudentsTesting1.Logic.Users;
using NUnit.Framework;
using StudentsTesting1.Logic.Groups;
using StudentsTesting1.Logic.Accounts;
using System.Security.Cryptography;

namespace StudentsNUnitTestProject.Database_Integration
{
    class AccountAccessTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void RegisterStudentAndGetHimBack()
        {
            //Arrange
            Student expextedStudent = new Student("Name", "Surname", "StudentID", "RecordBook", "TEST", "login");
            AccountAccess accountAccess = new AccountAccess(new TestDBAccess());

            //Act
            accountAccess.RegisterStudent("password", expextedStudent);
            Student actualStudent = GetStudentWithMaxID();

            //Assert
            Assert.AreEqual(expextedStudent.firstName, actualStudent.firstName);
            Assert.AreEqual(expextedStudent.lastName, actualStudent.lastName);
            Assert.AreEqual(expextedStudent.studentID, actualStudent.studentID);
            Assert.AreEqual(expextedStudent.recordBook, actualStudent.recordBook);
            Assert.AreEqual(expextedStudent.groupTitle, actualStudent.groupTitle);
            Assert.AreEqual(expextedStudent.login, actualStudent.login);
        }

        [Test]
        public void RegisterTeacherAndGetHimBack()
        {
            //Arrange
            Teacher expectedTeacher = new Teacher("Name", "Surname", "TeacherID", "teacherLogin");
            AccountAccess accountAccess = new AccountAccess(new TestDBAccess());

            //Act
            accountAccess.RegisterTeacher(expectedTeacher.login, "password", expectedTeacher);
            Teacher actualTeacher = GetTeacherWithMaxID();

            //Assert
            Assert.AreEqual(expectedTeacher.firstName, actualTeacher.firstName);
            Assert.AreEqual(expectedTeacher.lastName, actualTeacher.lastName);
            Assert.AreEqual(expectedTeacher.teacherID, actualTeacher.teacherID);
            Assert.AreEqual(expectedTeacher.login, actualTeacher.login);
        }

        [Test]
        public void TestTryToLogin()
        {
            // Arrange
            Student expextedStudent = new Student("Name", "Surname", "StudentID", "RecordBook", "TEST", "login1");
            string expectedLogin = "login1";
            string expectedRole = "student";
            AccountAccess accountAccess = new AccountAccess(new TestDBAccess());
            accountAccess.RegisterStudent("password1", expextedStudent);

            //Act
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.UTF8.GetBytes("password1");
            string passwordHash = Encoding.UTF8.GetString(sha1.ComputeHash(data));
            Account actualAccount = accountAccess.TryToLogin(expectedLogin, passwordHash);

            //Assert
            Assert.AreEqual(expectedLogin, actualAccount.login);
            Assert.AreEqual(expectedRole, actualAccount.role);
        }
        
        [Test]
        public void TestGetUserID()
        {
            //Arrange
            Student expextedStudent = new Student("Name", "Surname", "StudentID", "RecordBook", "TEST", "login2");
            AccountAccess accountAccess = new AccountAccess(new TestDBAccess());
            accountAccess.RegisterStudent("password2", expextedStudent);
            int accountID = GetMaxAccountID("student");
            int expectedID = GetMaxIDOfStudent();

            //Act
            int actualID = accountAccess.GetUserId(accountID, "student");

            //Assert
            Assert.AreEqual(expectedID, actualID);
        }

        // Method for test purposes
        public Student GetStudentWithMaxID()
        {
            TestDBAccess dBAccess = new TestDBAccess();
            DataTable dataTable = dBAccess.SQLGetTableData("SELECT * FROM STUDENTS JOIN ACCOUNTS ON ACCOUNTS.USER_ID = STUDENTS.ID " +
                "JOIN GROUPS ON STUDENTS.GROUP_ID = GROUPS.ID WHERE ROLE = \"student\" AND STUDENTS.ID = (SELECT MAX(ID) FROM STUDENTS);");
            if (dataTable.Rows.Count > 0)
            {
                Student student = new Student(Convert.ToString(dataTable.Rows[0].ItemArray[1]), Convert.ToString(dataTable.Rows[0].ItemArray[2]),
                        Convert.ToString(dataTable.Rows[0].ItemArray[3]), Convert.ToString(dataTable.Rows[0].ItemArray[4]), Convert.ToString(dataTable.Rows[0].ItemArray[12]),
                        Convert.ToString(dataTable.Rows[0].ItemArray[7]));
                return student;
            }
            return null;
        }
        // Method for test purposes
        public Teacher GetTeacherWithMaxID()
        {
            TestDBAccess dbaccess = new TestDBAccess();
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM TEACHERS JOIN ACCOUNTS ON TEACHERS.ID = ACCOUNTS.USER_ID WHERE ACCOUNTS.ROLE = \"teacher\" AND TEACHERS.ID = (SELECT MAX(ID) FROM TEACHERS);");
            if (dataTable.Rows.Count > 0)
            {
                Teacher teacher = new Teacher(Convert.ToString(dataTable.Rows[0].ItemArray[1]), Convert.ToString(dataTable.Rows[0].ItemArray[2]),
                    Convert.ToString(dataTable.Rows[0].ItemArray[3]), Convert.ToString(dataTable.Rows[0].ItemArray[5]));
                return teacher;
            }
            return null;
        }
        // Method for test purposes
        public int GetMaxIDOfStudent()
        {
            TestDBAccess dBAccess = new TestDBAccess();
            DataTable dataTable = dBAccess.SQLGetTableData("SELECT MAX(ID) FROM STUDENTS;");
            if (dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
            }
            return 0;
        }
        // Method for test purposes
        public int GetMaxAccountID(string role)
        {
            TestDBAccess dBAccess = new TestDBAccess();
            DataTable dataTable = dBAccess.SQLGetTableData("SELECT MAX(ID) FROM ACCOUNTS WHERE ROLE = \"" + role + "\";");
            if (dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
            }
            return 0;
        }

        // Method for test purposes
        public int CountStudentsInGroup(string title)
        {
            TestDBAccess dBAccess = new TestDBAccess();
            DataTable dataTable = dBAccess.SQLGetTableData("SELECT COUNT(*) FROM STUDENTS JOIN GROUPS ON STUDENTS.GROUP_ID = GROUPS.ID WHERE GROUPS.TITLE = \"" + title + "\";");
            if (dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
            }
            return 0;
        }
    }
}
