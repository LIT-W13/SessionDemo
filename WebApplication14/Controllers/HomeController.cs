using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //int? number = HttpContext.Session.GetInt32("number");

            //if (!number.HasValue)
            //{
            //    number = 0;
            //}

            //number++;

            //HttpContext.Session.SetInt32("number", number.Value);

            //return View(new HomepageViewMoel
            //{
            //    Number = number.Value
            //});

            HomepageViewModel vm = HttpContext.Session.Get<HomepageViewModel>("hvm");
            if(vm == null)
            {
                vm = new HomepageViewModel { Number = 0 };
            }

            vm.Number++;
            HttpContext.Session.Set("hvm", vm);
            return View(vm);
        }

    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }
    }
}
