using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using StudentsTesting1.Controllers;
using StudentsTesting1.Logic.Users;
using System.Security.Claims;
using StudentsTesting1.DataAccess;
using StudentsTesting1.Logic.Subjects;
using StudentsTesting1.Logic.Exams;
using StudentsTesting1.Logic.Results;
using StudentsTesting1.Logic.Questions;

namespace StudentsTesting1.Pages.CommonUser
{
    public class SubjectsModel : PageModel
    {
        StudentController studentController { get; set; }
        [BindProperty]
        public List<Subject> subjects { get; set; } = new List<Subject>();
        [BindProperty]
        public List<Exam> exams { get; set; } = new List<Exam>();
        [BindProperty]
        public List<Result> results { get; set; } = new List<Result>();
        public void OnGet()
        {
            int userID = Convert.ToInt32(HttpContext.User.Claims.Where(claim => claim.Type == "ID").Select(c => c.Value).FirstOrDefault());
            AccountAccess accountAccess = new AccountAccess(new DBAccess());
            int id = accountAccess.GetUserId(userID, "student");
            StudentAccess studentAccess = new StudentAccess(new DBAccess());
            Logic.Users.Student student = studentAccess.GetStudentByID(id);
            studentController = new StudentController(student);
            subjects = studentController.GetSubjectsOfStudent();
            exams = studentController.GetExamsOfStudent();
            foreach (Exam exam in exams)
            {
                Result result = studentController.GetResultsOfStudent(exam.id);
                if(result == null)
                {
                    results.Add(new Result(exam, student, new List<AnsweredQuestion>()));
                }
            }

        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Account/Login");
        }
    }
}