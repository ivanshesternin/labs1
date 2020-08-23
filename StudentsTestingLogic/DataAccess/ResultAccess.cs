using StudentsTesting1.Logic.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.Logic.Questions;
using System.Data;
using StudentsTesting1.Logic.Exams;

namespace StudentsTesting1.DataAccess
{
    public class ResultAccess
    {
        private IDBAccess dbaccess;
        QuestionAccess questionAccess;
        StudentAccess studentAccess;

        public ResultAccess(IDBAccess iDBAccess)
        {
            dbaccess = iDBAccess;
            questionAccess = new QuestionAccess(dbaccess);
            studentAccess = new StudentAccess(dbaccess);
        }
        public virtual void InsertResultToDB(Result result, int examId, string studentID)
        {
            string command = "INSERT INTO RESULTS(\"EXAM_ID\",\"STUDENT_ID\",\"SCORE\") VALUES " +
                "(" + examId + ", (SELECT ID AS STUDENT_ID FROM STUDENTS WHERE STUDENTID = \"" + studentID + "\"), " + result.score + "));";
            dbaccess.SQLExecute(command);
            foreach (AnsweredQuestion q in result.answeredQuestions)
            {
                questionAccess.InsertAnsweredToDB(q);
            }
        }

        public virtual List<Result> GetResultsOfGroup(string groupTitle, int examId)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM RESULTS JOIN STUDENTS ON RESULTS.STUDENT_ID = STUDENTS.ID " +
                "JOIN GROUPS ON GROUPS.ID = STUDENTS.GROUP_ID WHERE GROUPS.ID = \"" + groupTitle + "\" AND RESULTS.EXAM_ID = " + examId + ";");
            List<Result> results = new List<Result>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    results.Add(new Result(studentAccess.GetStudentByID(Convert.ToInt32(dataTable.Rows[i].ItemArray[2])),
                        Convert.ToInt32(dataTable.Rows[i].ItemArray[3]), questionAccess.GetAnsweredOfResult(Convert.ToInt32(dataTable.Rows[i].ItemArray[0]))));
                }
            }
            return results;
        }

        public virtual Result GetResultOfStudent(string studentID, int examID)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM RESULTS JOIN STUDENTS ON RESULTS.STUDENT_ID" +
                " = STUDENTS.ID WHERE STUDENTS.STUDENTID = \"" + studentID + "\" AND RESULTS.EXAM_ID = " + examID + ";");
            List<Result> results = new List<Result>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    results.Add(new Result(studentAccess.GetStudentByID(Convert.ToInt32(dataTable.Rows[i].ItemArray[2])),
                Convert.ToInt32(dataTable.Rows[i].ItemArray[3]), questionAccess.GetAnsweredOfResult(Convert.ToInt32(dataTable.Rows[i].ItemArray[0]))));
                }
                results.OrderBy(r => r.score);
                Result result = results.Last();
            }
            return null;
        }

        public Result GetResultByID(int id)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM RESULTS WHERE ID = " + id + ";");
            if (dataTable.Rows.Count > 0)
            {
                Result result = new Result(studentAccess.GetStudentByID(Convert.ToInt32(dataTable.Rows[0].ItemArray[2])),
                Convert.ToInt32(dataTable.Rows[0].ItemArray[3]), questionAccess.GetAnsweredOfResult(Convert.ToInt32(dataTable.Rows[0].ItemArray[0])));
                return result;
            }
            return null;
        }
    }
}
