using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsTesting1.Logic.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using StudentsTesting1.DataAccess;
using StudentsTesting1.Logic.Subjects;
using StudentsTesting1.Logic.Groups;

namespace StudentsTesting1.Pages.Admin
{
    public class AddSubjectModel : PageModel
    {
        Logic.Users.Admin admin = new Logic.Users.Admin("", "", "");
        [BindProperty]
        public List<Teacher> teachers { get; set; }
        [BindProperty]
        public List<Subject> subjects { get; set; }
        [BindProperty]
        public List<Group> groups { get; set; }
        [BindProperty]
        public string group { get; set; }
        [BindProperty]
        public string subjectTitle { get; set; }
        [BindProperty]
        public int subjectId { get; set; }
        [BindProperty]
        public string teacherID { get; set; }

        public void OnGet()
        {
            teachers = admin.GetTeachers();
            subjects = admin.GetSubjects();
            groups = admin.GetGroups();
        }

        public void OnPostCreateSubject()
        {
            admin.CreateSubject(subjectTitle, teacherID);
            OnGet();
            return;
        }

        public void OnPostAssignSubject()
        {
            subjects = admin.GetSubjects();
            groups = admin.GetGroups();
            foreach (Subject subject in subjects)
            {
                if (subject.id == subjectId) 
                {
                    foreach (Group group in groups)
                    {
                        if(group.title == this.group)
                        {
                            admin.AssignSubjectToGroup(subject, group);
                            OnGet();
                            return;
                        }
                    }
                }
            }
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