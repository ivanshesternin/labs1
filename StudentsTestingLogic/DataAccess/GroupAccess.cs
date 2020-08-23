using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.Logic.Groups;
using System.Data;
using StudentsTesting1.Logic.Subjects;
using StudentsTesting1.Logic.Users;

namespace StudentsTesting1.DataAccess
{
    public class GroupAccess
    {
        private IDBAccess dbaccess;

        public GroupAccess(IDBAccess iDBAccess)
        {
            dbaccess = iDBAccess;
        }
        public virtual void InsertGroupToDB(Group group)
        {
            string command = "INSERT INTO GROUPS(\"TITLE\") VALUES (\"" + group.title + "\");";
            dbaccess.SQLExecute(command);
        }

        public virtual void AddSubjectToGroup(Group group, Subject subject)
        {
            string command = "INSERT INTO GROUPSTOSUBJECTS(\"GROUP_ID\", \"SUBJECT_ID\") VALUES ((SELECT ID FROM GROUPS WHERE GROUPS.TITLE = \"" + group.title + "\"), " + subject.id + ");";
            dbaccess.SQLExecute(command);
        }

        public List<Group> GetGroupsFromDB()
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM GROUPS");
            List<Group> groups = new List<Group>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    groups.Add(new Group(Convert.ToString(dataTable.Rows[i].ItemArray[1])));
                }
            }
            return groups;
        }

        public List<Group> GetGroupsOfSubject(int subjectId)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM GROUPS JOIN GROUPSTOSUBJECTS ON GROUPSTOSUBJECTS.GROUP_ID" +
                " = GROUPS.ID JOIN SUBJECTS ON SUBJECTS.ID = GROUPSTOSUBJECTS.SUBJECT_ID WHERE SUBJECTS.ID = \"" + subjectId + "\";");
            List<Group> groups = new List<Group>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    groups.Add(new Group(Convert.ToString(dataTable.Rows[i].ItemArray[1])));
                }
            }
            return groups;
        }

        public Group GetGroupByID(int id)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM GROUPS WHERE ID = " + id + ";");
            if (dataTable.Rows.Count > 0)
            {
                Group group = new Group(Convert.ToString(dataTable.Rows[0].ItemArray[1]));
                return group;
            }
            Group emptyGroup = new Group("Group not found");
            return emptyGroup;
        }
    }
}
