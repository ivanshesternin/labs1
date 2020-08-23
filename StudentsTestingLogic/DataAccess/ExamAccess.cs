using StudentsTesting1.Logic.Exams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using StudentsTesting1.Logic.Questions;
using StudentsTesting1.Logic.Users;

namespace StudentsTesting1.DataAccess
{
    public class ExamAccess
    {
        private IDBAccess dbaccess;
        private QuestionAccess questionAccess;

        public ExamAccess(IDBAccess iDBAccess)
        {
            dbaccess = iDBAccess;
            questionAccess = new QuestionAccess(dbaccess);
        }
        public virtual void InsertExamToDB(Exam exam, int subjectId)
        {
            string command = "INSERT INTO EXAMS(\"TITLE\",\"NUMBEROFQUESTIONS\",\"ATTEMPTS\",\"SUBJECT_ID\") VALUES " +
                "(\"" + exam.title + "\", " + exam.numberOfQuestions + ", " + exam.attempts + ", " +
                "(SELECT ID AS SUBJECT_ID FROM SUBJECTS WHERE SUBJECTS.ID = \"" + subjectId + "\"));";
            dbaccess.SQLExecute(command);
            foreach (Question q in exam.questions)
            {
                questionAccess.InsertQuestionToDB(q);
            }
        }

        public virtual void AssignExam(Exam exam, string studentId)
        {
            string command = "INSERT INTO EXAMSTOSTUDENTS(\"EXAM_ID\",\"STUDENT_ID\",\"ATTEMPTS\") VALUES " +
                "(" + exam.id + ", (SELECT ID FROM STUDENTS WHERE STUDENTID = \"" + studentId + "\"), " + exam.attempts + ");";
            dbaccess.SQLExecute(command);
        }

        public List<Exam> GetExamsOfSubject(int subjectID)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM EXAMS JOIN SUBJECTS ON EXAMS.SUBJECT_ID = SUBJECTS.ID WHERE SUBJECTS.ID = " + subjectID + ";");
            List<Exam> exams = new List<Exam>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    exams.Add(new Exam(Convert.ToInt32(dataTable.Rows[i].ItemArray[0]) ,Convert.ToString(dataTable.Rows[i].ItemArray[1]), 
                        Convert.ToInt32(dataTable.Rows[i].ItemArray[2]), Convert.ToInt32(dataTable.Rows[i].ItemArray[3]), 
                        Convert.ToInt32(dataTable.Rows[i].ItemArray[4])));
                    List<Question> questions = questionAccess.GetQuestionsOfExam(exams[i].id);
                    for (int j = 0; j < questions.Count; j++)
                    {
                        exams[i].AddQuestion(questions[j]);
                    }
                }
            }
            return exams;
        }

        public List<Exam> GetExamsOfStudent(string studentID)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM EXAMS JOIN EXAMSTOSTUDENTS ON EXAMSTOSTUDENTS.EXAM_ID" +
                " = EXAMS.ID JOIN STUDENTS ON STUDENTS.ID = EXAMSTOSTUDENTS.STUDENT_ID WHERE STUDENTS.STUDENTID = \"" + studentID + "\";"); 
            List<Exam> exams = new List<Exam>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    exams.Add(new Exam(Convert.ToInt32(dataTable.Rows[i].ItemArray[0]) ,Convert.ToString(dataTable.Rows[i].ItemArray[1]),
                        Convert.ToInt32(dataTable.Rows[i].ItemArray[2]), Convert.ToInt32(dataTable.Rows[i].ItemArray[3]), 
                        Convert.ToInt32(dataTable.Rows[i].ItemArray[4])));
                    List<Question> questions = questionAccess.GetQuestionsOfExam(exams[i].id);
                    for (int j = 0; j < questions.Count; j++)
                    {
                        exams[i].AddQuestion(questions[j]);
                    }
                }
            }
            return exams;
        }

        public Exam GetExamByID(int id)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM EXAMS WHERE ID = " + id + ";");
            if (dataTable.Rows.Count > 0)
            {
                Exam exam = new Exam(Convert.ToInt32(dataTable.Rows[0].ItemArray[0]), Convert.ToString(dataTable.Rows[0].ItemArray[1]),
                Convert.ToInt32(dataTable.Rows[0].ItemArray[2]), Convert.ToInt32(dataTable.Rows[0].ItemArray[3]),
                Convert.ToInt32(dataTable.Rows[0].ItemArray[4]));
                List<Question> questions = questionAccess.GetQuestionsOfExam(exam.id);
                for (int j = 0; j < questions.Count; j++)
                {
                    exam.AddQuestion(questions[j]);
                }
                return exam;
            }
            return null;
        }
    }
}