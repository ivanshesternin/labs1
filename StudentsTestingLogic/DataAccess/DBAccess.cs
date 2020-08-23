using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace StudentsTesting1.DataAccess
{
    public class DBAccess : IDBAccess
    {
        private static DBAccess Instance { get; set; }
        private static string filename = "Database/StudentsTesting.db";
        private static SQLiteConnection connection = new SQLiteConnection("Data Source=" + filename + ";Version=3;");

        public DBAccess() { }

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
            if(Instance == null)
            {
                Instance = new DBAccess();
            }
            return Instance;
        }
    }
}
