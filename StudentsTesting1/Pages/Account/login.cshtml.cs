using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text;
using StudentsTesting1.Logic.Users;
using StudentsTesting1.DataAccess;
using System.ComponentModel.DataAnnotations;
using StudentsTesting1.Logic.Accounts;
using StudentsTesting1.Controllers;
using System.Threading;

namespace StudentsTesting1.Pages
{
    public class LoginModel : PageModel
    {
        AccountAccess accountAccess = new AccountAccess(new DBAccess());

        [Required(ErrorMessage = "Не вказано логін!")]
        [BindProperty]
        public string login { get; set; }

        [Required(ErrorMessage = "Не введений пароль!")]
        [DataType(DataType.Password)]
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string error { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostLogin()
        {
            AccountController accountController = new AccountController(new DBAccess().GetInstance());
            ClaimsIdentity identity = await accountController.Login(this, HttpContext);
            if (identity == null)
            {
                error = "Неправильний логін або пароль!";
                return null;
            }
            string role = identity.Claims.Where(claim => claim.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();
            switch (role)
            {
                case "admin":
                    {
                       return RedirectToPage("/Admin/AddTeacher");
                    }
                case ("teacher"):
                    {
                        return RedirectToPage("/Teachers/TSubjects");
                    }
                case ("student"):
                    {
                        return RedirectToPage("/CommonUser/Subjects");
                    }
                default: return null;
            }
        }
    }
}