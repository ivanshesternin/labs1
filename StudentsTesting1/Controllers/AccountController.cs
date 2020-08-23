using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using StudentsTesting1.Pages;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentsTesting1.DataAccess;
using StudentsTesting1.IoC;
using StudentsTesting1.Logic.Accounts;
using Microsoft.AspNetCore.Http;

namespace StudentsTesting1.Controllers
{
    public class AccountController : Controller
    {
        private IDBAccess dBAccess { get; set; }
        private AccountAccess accountAccess { get; set; }
        private IoCContainer IoC = new IoCContainer();

        public AccountController(IDBAccess dBAccess)
        {
            IoC.RegisterObject<IDBAccess, DBAccess>();
            accountAccess = new AccountAccess(dBAccess);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<ClaimsIdentity> Login(LoginModel model, HttpContext context)
        {
            ClaimsIdentity identity = null;
            if (ModelState.IsValid)
            {
                var sha1 = new SHA1CryptoServiceProvider();
                var data = Encoding.UTF8.GetBytes(model.Password);
                Account account = accountAccess.TryToLogin(model.login, Encoding.UTF8.GetString(sha1.ComputeHash(data)));
                if (account != null)
                {
                    identity = await Authenticate(account, context);
                }
            }
            return identity;
        }

        private async Task<ClaimsIdentity> Authenticate(Account account, HttpContext context)
        {
            var claims = new List<Claim>
            {
                new Claim("ID", account.ID.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, account.role),
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            return id;
        }
        public async Task<IActionResult> Logout(HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Account/Login");
        }
    }
}
