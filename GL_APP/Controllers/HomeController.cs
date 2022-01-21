using GL_APP.Data;
using GL_APP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public IActionResult Index()
        {
            var messages = _db.Messages.Include(m => m.User);
            return View(messages);
        }

        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(string name, string password)
        {
            var user = _db.Users.FirstOrDefault(u => u.Password == password);
            if (user == null)
            {
                user = new User { Name = name, Password = password };
                _db.Users.Add(user);
                _db.SaveChanges();
            }

            return RedirectToAction("Discuss", new { userId = user.Id, userName = user.Name });
        }

        public IActionResult Discuss(int userId, string userName)
        {
            ViewBag.Messages = _db.Messages.Include(m => m.User)
                .OrderByDescending(m => m.When)
                .Take(10).ToArray();

            var model = new Message { UserName = userName, UserId = userId };
            return View(model);
        }

        [HttpPost]
        public IActionResult Discuss(Message message)
        {
            message.When = DateTime.Now;
            _db.Messages.Add(message);
            _db.SaveChanges();

            ViewBag.Messages = _db.Messages.Include(m => m.User)
                .OrderByDescending(m => m.When)
                .Take(10).ToArray();

            return View(message);
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