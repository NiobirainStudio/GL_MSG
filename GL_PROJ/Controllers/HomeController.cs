using Microsoft.AspNetCore.Mvc;
using GL_PROJ.Models;
using GL_PROJ.Models.DbContextModels;
using System.Diagnostics;
using GL_PROJ.Data;
using Microsoft.EntityFrameworkCore;
using GL_PROJ.Models.DTO;

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
        //
        // REGISTRATION PAGE BLOCK BEGIN
        //
        // This GET method returns the registration page in order to recieve user data
        [HttpGet]
        public IActionResult Registration()
        {
            // Return registration page
            var model = new RegistrationViewModel();
            return View(model);
        }

        // This POST method recieves user data from the registration page
        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            // Check for model validity
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Invalid input!";
                return View(model);
            }

            // Find user by name
            var user = _db.Users.FirstOrDefault(u => u.Name == model.UserName);

            // User already exists error
            if (user != null)
            {
                ViewBag.Error = "User already exists!";
                return View(model);
            }

            // Create if not found
            user = new User { Name = model.UserName, Password = model.Password };
            _db.Users.Add(user);
            _db.SaveChanges();

            // Redirect to the main method (GET)
            return RedirectToAction("Main", new { userId = user.Id, userName = user.Name });
        }
        //
        // REGISTRATION PAGE BLOCK END
        //
        //------------------------------------------------------------//





        //------------------------------------------------------------//
        //
        // LOGIN PAGE BLOCK BEGIN
        //
        // This GET method returns the login page in order to recieve user data
        [HttpGet]
        public IActionResult Login()
        {
            // Return login page
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
                ViewBag.Error = "Invalid input!";
                return View(model);
            }

            // Find user by name
            var user = _db.Users.FirstOrDefault(u => u.Name == model.UserName);

            // User doesn't exist error
            if (user == null)
            {
                ViewBag.Error = "User doesn't exist!";
                return View(model);
            }

            // Return the login page with an error
            if(user.Password != model.Password)
            {
                ViewBag.Error = "Incorrect password!";
                return View(model);
            }

            // Redirect to the main method (GET)
            return RedirectToAction("Main", new { userId = user.Id, userName = user.Name });
        }
        //
        // LOGIN PAGE BLOCK END
        //
        //------------------------------------------------------------//





        //------------------------------------------------------------//
        //
        // MAIN PAGE BLOCK BEGIN
        //
        // This GET method is called via login POST method
        // The metod returns the main page in order to display messages
        [HttpGet]
        public IActionResult Main(int userId, string userName)
        {
            var model = new MainViewModel();

            model.Groups = (from rel in _db.UserGroupRelations
                            join grp in _db.Groups
                            on rel.GroupId equals grp.Id
                            where rel.UserId == userId
                            select grp)
                            .ToList();

            return View(model);
        }
        //
        // MAIN PAGE BLOCK END
        //
        //------------------------------------------------------------//





        //------------------------------------------------------------//
        //
        // AJAX REQUEST BLOCK BEGIN
        //
        // This is an AJAX method for recieving group messages by using groupId (This is not a secure way of requesting data)
        public List<MessageDTO> GetMessagesByGroupId(string groupId)
        {
            try
            {
                var res = _db.Messages
                .Where(m => m.GroupId == int.Parse(groupId))
                .Select(me => new MessageDTO
                {
                    MessageId = me.Id,
                    Data = me.Data,
                    Date = me.Date,
                    Type = me.Type,
                    UserId = me.UserId,
                    UserName = me.User.Name,
                    GroupId = me.GroupId
                })
                .ToList();

                return res;
            }
            catch (Exception)
            {
                return null;
            }
        }
        //
        // AJAX REQUEST BLOCK END
        //
        //------------------------------------------------------------//





        //------------------------------------------------------------//
        //
        // ERROR RESPONSE BLOCK BEGIN
        //
        // Details unknown
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //
        // ERROR RESPONSE BLOCK END
        //
        //------------------------------------------------------------//
    }
}
