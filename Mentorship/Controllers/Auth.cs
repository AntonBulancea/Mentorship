using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mentorship.Models.Context;
using Mentorship.Models.MainFunctions;
using Mentorship.Models.SecondaryFunctions;
using Mentorship.Models.Tables;
using Microsoft.AspNetCore.Mvc;

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
                        using (StreamWriter writer = new StreamWriter("wwwroot/Account.txt"))
                        {
                            writer.Write(u.Name + "&" + u.Surname + "&" + email + "&" + pass);
                        }
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

            using (StreamWriter writer = new StreamWriter("wwwroot/Account.txt"))
            {
                writer.Write(name + "&" + surname + "&" + email + "&" + pass);
            }
            Accounts account = new Accounts() { Name = name, Surname = surname, Email = email, Pass = pass };
            return View("myAccount", account);
        }
        public IActionResult myAccountPage()
        {
            using (StreamReader reader = new StreamReader("wwwroot/Account.txt"))
            {
                string text = reader.ReadToEnd();

                string name = text.Split('&')[0];
                string surname = text.Split('&')[1];
                string email = text.Split('&')[2];
                string pass = text.Split('&')[3];

                Accounts acc = new Accounts() { Name = name, Surname = surname, Email = email, Pass = pass };

                return View("myAccount", acc);
            }
        }
    }
}