using Microsoft.AspNetCore.Mvc;
using GL_PROJ.Models;
using GL_PROJ.Models.DbContextModels;
using System.Diagnostics;
using GL_PROJ.Data;
using GL_PROJ.Models.DTO;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using GL_PROJ.Models.DBService;
using System.Net.WebSockets;
using System.Net;
using Microsoft.AspNetCore.SignalR;
using GL_PROJ.AppConfig;
using GL_PROJ.TimerFeatures;

namespace GL_PROJ.Controllers
{
    public class HomeController : ControllerBase
    {
        // Database manager linking
        private readonly IDB _dbManager;
        private readonly AppDbContext _db;

        // Main hub linking
        private readonly IHubContext<MainHub> _hub;

        public HomeController(AppDbContext db, IDB dbManager, IHubContext<MainHub> hub)
        {
            _dbManager = dbManager;
            _db = db;
            _hub = hub;
        }


        /*
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
            if (user.Password != model.Password)
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
            model.UserId = userId;
            model.Groups = (from rel in _db.UserGroupRelations
                            join grp in _db.Groups
                            on rel.GroupId equals grp.Id
                            where rel.UserId == userId
                            select grp)
                            .ToArray();
            
            //return View("Main");
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
        [HttpPost]
        public List<MessageDTO> GetMessagesByGroupId(string userId, string groupId)
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
                Console.Out.WriteLine("Eh...");
                return null;
            }
        }
        //
        // AJAX REQUEST BLOCK END
        //
        //------------------------------------------------------------//
        */

        /*
        //------------------------------------------------------------//
        //
        // TESTING BLOCK BEGIN
        //

        
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }



        // Sign in
        [HttpPost]
        public JsonResult Login([FromBody] LogRegDTO lr)
        {
            var user = _db.Users.FirstOrDefault(u => u.Name == lr.UserName);

            // User doesn't exist error
            if (user == null)
            {
                return Json(new { code = 0 });
            }

            // Return the login page with an error
            if (user.Password != lr.Password)
            {
                return Json(new { code = 1 });
            }

            return Json(new { code = 2, session = "Session string, baby!" });
        }

        // Sign up


        // Get groups
        [HttpPost]
        public GroupDTO[] GetGroups([FromBody] string session)
        {
            return new GroupDTO[]
            {
                new GroupDTO { Id = 1, Name = "First ever!" }
            };
        }

        [HttpPost]
        public MessageDTO[] GetLastXMessages([FromBody] string session, [FromBody] int group_id)
        {
            return new MessageDTO[]
            {
                new MessageDTO { MessageId = 1, GroupId = 1, UserId = 1, Data = "Hi!", Date = DateTime.Now, Type = 1 }
            };
        }
        
        [HttpPost]
        public UserDTO[] GetUsersForGroup([FromBody] string session, [FromBody] int group_id)
        {
            return new UserDTO[] 
            { 
                new UserDTO { Id = 2, Name = "Dave", Description = "Tis description" },
                new UserDTO { Id = 3, Name = "Davey", Description = "Tis description2" }
            };
        }



        [HttpPost]
        public bool CheckSession([FromBody] string session)
        {
            return true;
        }
        



        //
        // ERROR RESPONSE BLOCK END
        //
        //------------------------------------------------------------//
        */
        
        public IActionResult GetUserGroups([FromBody] int user_id)
        {
            var result = new { groups = _dbManager.GetGroupsByUserId(user_id) };

            /*
             var data = new { groups = (from rel in _db.UserGroupRelations
                                       join grp in _db.Groups
                                       on rel.GroupId equals grp.Id
                                       where rel.UserId == user_id
                                       select new GroupDTO
                                       {
                                           Id = grp.Id,
                                           Description = grp.Description,
                                           Name = grp.Name,
                                           LastMessage = (from msg in _db.Messages
                                                          where msg.GroupId == grp.Id
                                                          orderby msg.Date descending
                                                          select new MessageDTO
                                                          {
                                                              MessageId = msg.Id,
                                                              GroupId = msg.GroupId,
                                                              Data = msg.Data,
                                                              Date = msg.Date,
                                                              Type = msg.Type,
                                                              UserId = msg.UserId
                                                          }
                                                         ).ToArray()[0]
                                       }).ToArray()
            };
             */

            return Ok(result);
        }


        public IActionResult GetLastGroupMessages([FromBody] int groupId)
        {
            //var result = new { messages = _dbManager.GetMessagesByGroupId(group_id) };

            var result = new { messages = (from msg in _db.Messages
                                           where msg.GroupId == groupId
                                           select new MessageDTO
                                           {
                                               MessageId = msg.Id,
                                               GroupId = msg.GroupId,
                                               Data = msg.Data,
                                               Date = msg.Date,
                                               Type = msg.Type,
                                               UserId = msg.UserId
                                           }).ToArray().TakeLast(5)
            };
            return Ok(result);
        }


        public class GXGM_DTO
        {
            public string session { get; set; }
            public int groupId { get; set; }
            public int lastId { get; set; }
        }
        public IActionResult GetXGroupMessages([FromBody] GXGM_DTO i)
        {
            
            //var result = new { messages = _dbManager.GetMessagesByGroupId(group_id) };

            var result = new { messages = (from msg in _db.Messages
                                           where msg.GroupId == i.groupId && msg.Id < i.lastId
                                           select new MessageDTO
                                           {
                                               MessageId = msg.Id,
                                               GroupId = msg.GroupId,
                                               Data = msg.Data,
                                               Date = msg.Date,
                                               Type = msg.Type,
                                               UserId = msg.UserId
                                           }).ToArray().TakeLast(5)
            };
            return Ok(result);
        }


        public IActionResult SignIn([FromBody] LogRegDTO lr)
        {
            // Find user by name
            var user = _db.Users.FirstOrDefault(u => u.Name == lr.UserName);

            // User doesn't exist error
            if (user == null)
            {
                return Ok(new { code = 0 });
            }

            // Return the login page with an error
            if (user.Password != lr.Password)
            {
                return Ok(new { code = 1 });
            }

            return Ok(new { code = 2, session = user.Id, id = user.Id });
        }
    }
}