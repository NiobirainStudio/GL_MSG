using Microsoft.AspNetCore.Mvc;
using GL_PROJ.Models;
using System.Diagnostics;
using GL_PROJ.Data;
using Microsoft.EntityFrameworkCore;

namespace GL_PROJ.Controllers
{
    // This class defines the home controller
    public class HomeController : Controller
    {
        // Database linking
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        //------------------------------------------------------------//

        // Login page block
        // This GET method returns the login page in order to recieve user data
        [HttpGet]
        public IActionResult Login()
        {
            // Return Login page
            var model = new LoginViewModel();
            return View(model);
        }

        // This POST method recieves user data from the login page
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            // Check for model validity
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Invalid Input!";
                return View(model);
            }

            // Find user by name
            var user = _db.Users.FirstOrDefault(u => u.Name == model.UserName);

            // Create if not found
            if (user == null)
            {
                user = new User { Name = model.UserName, Password = model.Password };
                _db.Users.Add(user);
                _db.SaveChanges();
                
                // Redirect to the main method (GET)
                return RedirectToAction("Main", new { userId = user.Id, userName = user.Name });
            }

            // Return the login page with an error
            if(user.Password != model.Password)
            {
                ViewBag.Error = "Incorrect Password!";
                return View(model);
            }

            // Redirect to the main method (GET)
            return RedirectToAction("Main", new { userId = user.Id, userName = user.Name });
        }

        //------------------------------------------------------------//

        // Main page block
        // This GET method is called via login POST method
        // The metod returns the main page in order to display messages
        [HttpGet]
        public IActionResult Main(int userId, string userName)
        {
            var model = new MainViewModel();

            model.Messages = _db.Messages.Include(m => m.User)
                .OrderByDescending(m => m.When)
                .Take(10).ToArray();

            return View(model);
        }

        //------------------------------------------------------------//

        // Error response block
        // Details unknown
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
