using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsTesting1.Logic.Questions;

namespace StudentsTesting1.Logic.Exams
{
    public interface IExam
    {
        public void AddQuestion(IQuestion question);
    }
}
