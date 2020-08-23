using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsTesting1.DataAccess
{
    public interface IDBAccess
    {
        public abstract IDBAccess GetInstance();

        public void SQLExecute(string query) { }

        public DataTable SQLGetTableData(string query) { return new DataTable(); }
    }
}
