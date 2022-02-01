using GL_PROJ.Models.DbContextModels;
namespace GL_PROJ.Data

{
    interface IDB
    {
        void CreateUser(User user) { }

        //void DeleteUser(User user) { }

        void CreateGroup(Group group) { }

        //void DeleteGroup(Group group) { }

        void LeaveGroup(Group group, string UID) { }

        void WriteMessage(Message message) { }

        public List<Group> GroupsByUID(string UID) { return null; }

        public List<Message> MessagesByUID(string UID) { return null; }
    }
    public class DB_Manager : IDB
    {
        private readonly AppDbContext _db;
        public DB_Manager(AppDbContext db) 
        {
            _db = db;
        }
        public void CreateUser(User user) 
        { 
            _db.Users.Add(user);
            _db.SaveChanges();
        }
        public void CreateGroup(Group group) 
        {
            _db.Groups.Add(group);
            _db.SaveChanges();
        }
        public void LeaveGroup(Group group, string UID) 
        {
            UserGroupRelation ugr = _db.UserGroupRelations.Find(UID,group.Id);
            _db.UserGroupRelations.Remove(ugr);
            _db.SaveChanges();
        }
        public void WriteMessage(Message message) 
        {
            _db.Messages.Add(message);
            _db.SaveChanges();
        }

        public List<Group> GroupByUID(string UID) 
        {
            int[] groud_ids = _db.UserGroupRelations.Where(r => r.UserId.ToString() == UID).Select(rel => rel.GroupId).ToArray();
            return _db.Groups.Where(g => groud_ids.Contains(g.Id)).ToList();
        }

        public List<Message> MessagesByUID(string UID)
        {
            
            return _db.Messages.Where(m=>m.UserId.ToString()==UID).ToList();
        }

    }
}
