using System;
using System.Collections.Generic;
using StudentsTesting1.Logic.Groups;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentsTesting1.Pages.Admin
{
    public class AddGroupModel : PageModel
    {
        Logic.Users.Admin admin = new Logic.Users.Admin("", "", "");
        [BindProperty]
        public List<Group> groups { get; set; }
        [BindProperty]
        public string groupTitle { get; set; }
        [BindProperty]
        public string studentId { get; set; }
        [BindProperty]
        public string error { get; set; }
        public void OnGet()
        {
            groups = admin.GetGroups();
            foreach(Group group in groups)
            {
                group.SetSubjects(admin.GetSubjectsOfGroup(group));
            }
        }

        public void OnPostRegisterGroup()
        {
            groups = admin.GetGroups();
            foreach (Group group in groups)
            {
                if (group.title == groupTitle)
                {
                    error = "Така група вже існує!";
                    return;
                }
            }

            admin.CreateGroup(groupTitle);
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