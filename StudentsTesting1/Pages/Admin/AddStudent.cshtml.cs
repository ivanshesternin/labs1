using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsTesting1.Logic.Users;
using StudentsTesting1.Logic.Groups;

namespace StudentsTesting1.Pages.Admin
{
    public class AddStudentModel : PageModel
    {
        Logic.Users.Admin admin = new Logic.Users.Admin("", "", "");
        [BindProperty]
        public List<Student> students { get; set; }
        [BindProperty]
        public List<Group> groups { get; set; }
        [BindProperty]
        public string firstName { get; set; }
        [BindProperty]
        public string lastName { get; set; }
        [BindProperty]
        public string group { get; set; }
        [BindProperty]
        public string studentId { get; set; }
        [BindProperty]
        public string recordBook { get; set; }
        [BindProperty]
        public string login { get; set; }
        [BindProperty]
        public string password { get; set; }
        [BindProperty]
        public string repeatPassword { get; set; }

        [BindProperty]
        public string error { get; set; }
        public void OnGet()
        {
            students = admin.GetStudents();
            groups = admin.GetGroups();
        }

        public void OnPostRegisterStudent()
        {
            foreach (Student student in students)
            {
                if (student.login == login)
                {
                    error = "Такий логін вже зайнятий! Придумайте інший логін!";
                    OnGet();
                    return;
                }
                if (student.studentID == studentId)
                {
                    error = "Студент із таким студентським квитком вже є!";
                    OnGet();
                    return;
                }
                if (student.recordBook == recordBook)
                {
                    error = "Студент із такою заліковою книжкою вже є!";
                    OnGet();
                    return;
                }
            }
            if (password != repeatPassword)
            {
                error = "Пароль і повтор пароля не співпадають!";
                OnGet();
                return;
            }

            admin.CreateStudent(firstName, lastName, studentId, recordBook, group, login, password);
            error = "";
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