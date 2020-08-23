using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsTesting1.Logic.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using StudentsTesting1.DataAccess;

namespace StudentsTesting1.Pages.Admin
{
    public class AddTeacherModel : PageModel
    {
        Logic.Users.Admin admin = new Logic.Users.Admin("", "", "");
        [BindProperty]
        public List<Teacher> teachers { get; set; }
        [BindProperty]
        public string firstName { get; set; }
        [BindProperty]
        public string lastName { get; set; }
        [BindProperty]
        public string id { get; set; }
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
            teachers = admin.GetTeachers();
        }

        public void OnPostRegisterTeacher()
        {
            foreach(Teacher teacher in teachers)
            {
                if (teacher.login == login)
                {
                    error = "Такий логін вже зайнятий! Придумайте інший логін!";
                    return;
                }
                if (teacher.teacherID == id)
                {
                    error = "Унікальний ідентифікатор вже використовується! Введіть інший ідентифікатор!";
                    return;
                }
            }
            if (password != repeatPassword)
            {
                error = "Пароль і повтор пароля не співпадають!";
                return;
            }

            admin.CreateTeacher(firstName, lastName, id, login, password);
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