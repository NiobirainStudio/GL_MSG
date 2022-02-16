using GL_PROJ.Data;
using GL_PROJ.Models.DbContextModels;
using GL_PROJ.Models.DBService;
using GL_PROJ.Models.DTO;
using Microsoft.AspNetCore.SignalR;

namespace GL_PROJ.AppConfig
{
    public class MainHub : Hub
    {
        public readonly AppDbContext _db;
        //private readonly IDB _dbManager;

        /*
        public async Task Ptt2(string data, string userId)
        {
            await Clients.User(userId).SendAsync("ptt2", data);
        }

        
        public async Task MessagePike()
        {
            await Clients.User.SendAsync()
        }
        
        public async Task BroadcastChartData(string data, string connectionId)
                => await Clients.Client(connectionId).SendAsync("broadcastchartdata", data);

        public async Task BroadcastToGroups(string data) => await Clients.Group("boiz").SendAsync("Global", data);
        */

        public MainHub(AppDbContext db)
        {
            _db = db;
        }

        public void AddToGroups(int user_id)
        {
                
           int[] groups = (from ugr in _db.UserGroupRelations
                            where ugr.UserId == user_id
                            select ugr.GroupId).ToArray();

            foreach (int group_id in groups)
                Groups.AddToGroupAsync(Context.ConnectionId, group_id.ToString());
        }

        public void PostMessage(int user_id, string data, int type, int group_id)
        {
            // 1. Get user ID from session

            // 2. Check if user is in the group

            // 3. Post message if all is good
            _db.Messages.Add(new Message { Data = data, Type = type, GroupId = group_id, UserId = 1, Date = DateTime.Now });
            _db.SaveChanges();

            SendMessages(user_id, data);
        }

        private void SendMessages(int user_id, string data)
        {
            int[] groups = (from ugr in _db.UserGroupRelations
                            where ugr.UserId == user_id
                            select ugr.GroupId).ToArray();

            foreach (int group_id in groups)
                Clients.Group(group_id.ToString()).SendAsync("GroupMessages", data);
        }
    }
}