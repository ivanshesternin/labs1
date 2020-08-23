using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using StudentsTesting1.DataAccess;
using StudentsTesting1.Logic.Accounts;
using System.ComponentModel.DataAnnotations;

namespace StudentsTesting1.Pages
{
    public class IndexModel : PageModel
    {

        [Required(ErrorMessage = "Не вказано логін!")]
        public string login { get; set; }

        [Required(ErrorMessage = "Не введений пароль!")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string error = "";
        public IActionResult OnGet()
        {
            return RedirectToPage("Account/login");
        }
    }
}
