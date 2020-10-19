using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentorship.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace Mentorship.Controllers
{
    public class ClearDB : Controller
    {
        public IActionResult Index()
        {
            using (CoursesContext context = new CoursesContext())
            {
                foreach (var u in context.courses)
                {
                    context.courses.Remove(u);
                }
                context.SaveChanges();
           

            return Content("0");
            }
        }
    }
}