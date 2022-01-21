using GL_APP.Data;
using GL_APP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GL_APP.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var userNames = _db.Users.Select(u => u.Name).ToArray();
            var str = string.Join("\n", userNames);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}