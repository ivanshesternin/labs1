using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using StudentsTesting1.Logic.Questions;
using StudentsTesting1.Logic.Exams;
using System.Data.Common;

namespace StudentsTesting1.DataAccess
{
    public class QuestionAccess
    {
        private IDBAccess dbaccess;

        public QuestionAccess(IDBAccess iDBAccess)
        {
            dbaccess = iDBAccess;
        }
        public void InsertQuestionToDB(Question question)
        {
            string command = "INSERT INTO QUESTIONS(\"QUESTION\",\"CORRECTANSWER\",\"EXAM_ID\") VALUES " +
                "(\"" + question.question + "\", \"" + question.correctAnswer + "\", (SELECT MAX(ID) FROM EXAMS));";
            dbaccess.SQLExecute(command);
            foreach (string falseAnswer in question.falseAnswers)
            {
                string insertFalseAnswer = "INSERT INTO QUESTIONFALSEANSWERS(\"FALSEANSWER\",\"QUESTION_ID\") VALUES " +
                "(\"" + falseAnswer + "\", (SELECT MAX(ID) FROM QUESTIONS));";
                dbaccess.SQLExecute(insertFalseAnswer);
            }
        }

        public void InsertAnsweredToDB(AnsweredQuestion question)
        {
            string command = "INSERT INTO ANSWEREDQUESTIONS(\"QUESTION\",\"ACTUALANSWER\",\"RESULT_ID\") VALUES " +
                "(" + question.id + ", \"" + question.actualAnswer + "\", (SELECT MAX(ID) FROM RESULTS));";
            dbaccess.SQLExecute(command);
        }

        public List<Question> GetQuestionsOfExam(int examId)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM QUESTIONS JOIN EXAMS ON QUESTIONS.EXAM_ID = EXAMS.ID WHERE EXAMS.ID = \"" + examId + "\";");
            List<Question> questions = new List<Question>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataTable falseAnswersTable = dbaccess.SQLGetTableData("SELECT * FROM QUESTIONFALSEANSWERS JOIN QUESTIONS ON QUESTIONFALSEANSWERS.QUESTION_ID = QUESTIONS.ID WHERE QUESTIONS.ID = " + dataTable.Rows[i].ItemArray[0] + ";");
                    List<string> falseAnswers = new List<string>();
                    if (falseAnswersTable.Rows.Count > 0)
                    {
                        for (int j = 0; i < falseAnswersTable.Rows.Count; i++)
                        {
                            falseAnswers.Add(Convert.ToString(falseAnswersTable.Rows[j].ItemArray[1]));
                        }
                    }
                    questions.Add(new Question(Convert.ToString(dataTable.Rows[i].ItemArray[1]), Convert.ToString(dataTable.Rows[i].ItemArray[2]), falseAnswers));
                }
            }
            return questions;
        }

        public List<AnsweredQuestion> GetAnsweredOfResult(int resultId)
        {
            DataTable dataTable = dbaccess.SQLGetTableData("SELECT * FROM QUESTIONS JOIN ANSWEREDQUESTIONS ON ANSWEREDQUESTIONS.QUESTION_ID = QUESTIONS.ID JOIN RESULTS ON RESULTS.ID = ANSWEREDQUESTIONS.RESULT_ID WHERE RESULTS.ID = " + resultId + ";");
            List<AnsweredQuestion> answeredQuestions = new List<AnsweredQuestion>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataTable falseAnswersTable = dbaccess.SQLGetTableData("SELECT * FROM QUESTIONFALSEANSWERS JOIN QUESTIONS ON QUESTIONFALSEANSWERS.QUESTION_ID = QUESTIONS.ID WHERE QUESTIONS.ID = " + dataTable.Rows[i].ItemArray[1] + ";");
                    List<string> falseAnswers = new List<string>();
                    if (falseAnswersTable.Rows.Count > 0)
                    {
                        for (int j = 0; i < falseAnswersTable.Rows.Count; i++)
                        {
                            falseAnswers.Add(Convert.ToString(falseAnswersTable.Rows[j].ItemArray[1]));
                        }
                    }
                    answeredQuestions.Add(new AnsweredQuestion(Convert.ToString(dataTable.Rows[i].ItemArray[1]), 
                        Convert.ToString(dataTable.Rows[i].ItemArray[2]), falseAnswers, Convert.ToString(dataTable.Rows[i].ItemArray[3])));
                }
            }
            return answeredQuestions;
        }
    }
}
