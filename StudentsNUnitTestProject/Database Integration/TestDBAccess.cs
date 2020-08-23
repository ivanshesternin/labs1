using System;
using System.Collections.Generic;
using System.Text;
using StudentsTesting1.DataAccess;
using System.Data.SQLite;
using System.Data;

namespace StudentsNUnitTestProject.Database_Integration
{
    class TestDBAccess : IDBAccess
    {

        private static DBAccess Instance { get; set; }
        private static string filename = "D:/Projects/C#/StudentsTesting1/StudentsNUnitTestProject/Database/TestDB.db";
        private static SQLiteConnection connection = new SQLiteConnection("Data Source=" + filename + ";Version=3;");

        public TestDBAccess() { }

        public void SQLExecute(string query)
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public DataTable SQLGetTableData(string query)
        {
            connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            connection.Close();
            return dataTable;
        }

        public IDBAccess GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DBAccess();
            }
            return Instance;
        }
    }
}
