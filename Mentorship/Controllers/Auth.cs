
using Mentorship.Models.Context;
using Mentorship.Models.MainFunctions;
using Mentorship.Models.SecondaryFunctions;
using Mentorship.Models.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Mentorship.Controllers
{
    public class Auth : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
        public IActionResult logIn(string email, string pass)
        {
            AuthModel model = new AuthModel()
            {
                Danger = "User " + email + " wasn't found",
            };
            using (AccountContext context = new AccountContext())
            {
                var db = context.accounts;
                foreach (var u in db)
                {
                    if (u.Email.Equals(email) && u.Password.Equals(pass))
                    {
                        Accounts account = new Accounts() { Name = u.Name, Surname = u.Surname, Email = email, Pass = pass };

                        Response.Cookies.Append("name", u.Name);
                        Response.Cookies.Append("surname", u.Surname);
                        Response.Cookies.Append("email", email);
                        Response.Cookies.Append("pass", pass);

                        return View("myAccount", account);
                    }
                }
            }
            return View("Index", model);
        }
        public IActionResult regIn(string name, string surname, string email, string pass)
        {
            AuthModel model = new AuthModel()
            {
                RegDanger = "User " + email + " currently exists",
            };

            using (AccountContext context = new AccountContext())
            {
                var db = context.accounts;
                foreach (var u in db)
                {
                    if (u.Email.Equals(email))
                    {
                        return View("Index", model);
                    }
                }
            }

            using (AccountContext context = new AccountContext())
            {
                Account ac = new Account() { Name = name, Surname = surname, Email = email, Password = pass };
                context.accounts.Add(ac);
                context.SaveChanges();
            }

            Response.Cookies.Append("name", name);
            Response.Cookies.Append("surname", surname);
            Response.Cookies.Append("email", email);
            Response.Cookies.Append("pass", pass);

            Accounts account = new Accounts()
            {
                Name = name,
                Surname = surname,
                Email = email,
                Pass = pass
            };

            // return Content(HttpContext.Request.Cookies["email"]);
            return View("myAccount", account);
        }
        public IActionResult myAccountPage()
        {
            string name = HttpContext.Request.Cookies["name"];
            string surname = HttpContext.Request.Cookies["surname"];
            string email = HttpContext.Request.Cookies["email"];
            string pass = HttpContext.Request.Cookies["pass"];

            Accounts acc = new Accounts() { Name = name, Surname = surname, Email = email, Pass = pass };
            return View("myAccount", acc);

        }

        [HttpPost]
        public IActionResult setImg(IFormFile file)
        {
            string[] path = HttpContext.Request.Cookies["email"].Split('@');
            string email = path[0] + "-" + path[1] + ".png";

            using (FileStream stream = new FileStream("wwwroot/user/" + email, FileMode.OpenOrCreate))
            {
                file.CopyTo(stream);
            }

            Accounts account = new Accounts()
            {
                Name = HttpContext.Request.Cookies["name"],
                Surname = HttpContext.Request.Cookies["surname"],
                Email = HttpContext.Request.Cookies["email"],
                Pass = HttpContext.Request.Cookies["pass"]
            };

            return View("myAccount", account);
        }
    }
}