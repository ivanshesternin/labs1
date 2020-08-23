using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using StudentsTesting1.Controllers;
using StudentsTesting1.DataAccess;
using StudentsTesting1.Logic.Exams;
using StudentsTesting1.Logic.Groups;
using StudentsTesting1.Logic.Subjects;
using StudentsTesting1.Logic.Users;

namespace StudentsTesting1.Pages.Teachers
{
    public class TSubjectsModel : PageModel
    {
        TeacherController teacherController { get; set; }
        [BindProperty]
        public List<Subject> subjects { get; set; }
        [BindProperty]
        public List<Group> groups { get; set; }
        [BindProperty]
        public List<Exam> exams { get; set; }
        [BindProperty]
        public int selectedSubject { get; set; }
        [BindProperty]
        public string examTitle { get; set; }
        [BindProperty]
        public int numberOfQuestions { get; set; }
        [BindProperty]
        public int attempts { get; set; }
        public string allGroups { get; set; } = "";
        public void OnGet()
        {
            int userID = Convert.ToInt32(HttpContext.User.Claims.Where(claim => claim.Type == "ID").Select(c => c.Value).FirstOrDefault());
            AccountAccess accountAccess = new AccountAccess(new DBAccess());
            int id = accountAccess.GetUserId(userID, "teacher");
            TeacherAccess teacherAccess = new TeacherAccess(new DBAccess());
            Teacher teacher = teacherAccess.GetTeacherByID(id);
            teacherController = new TeacherController(teacher);
            subjects = teacherController.GetSubjects();
            if (selectedSubject == 0)
            {
                groups = teacherController.GetGroups(subjects[0].id);
                exams = teacherController.GetExams(subjects[0].id);
            }
            else
            {
                groups = teacherController.GetGroups(selectedSubject);
                exams = teacherController.GetExams(selectedSubject);
            }
            if (groups.Count > 0)
            {
                foreach (Group group in groups)
                {
                    allGroups = allGroups + group.title + ", ";
                }
                allGroups = allGroups.Remove(allGroups.Length - 2);
            }
        }

        public void OnPostChangeSubject()
        {
            OnGet();
            return;
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Account/Login");
        }
    }
}