using GL_PROJ.Models.DbContextModels;
namespace GL_PROJ.Data

{
    interface IDB
    {
        bool CreateUser(User user) { return false; }

        //void DeleteUser(User user) { }

        void CreateGroup(Group group) { }

        //void DeleteGroup(Group group) { }

        bool LeaveGroup(Group group, string UID) { return false; }

        bool JoinGroup(Group group, string UID) { return false; }

        void WriteMessage(Message message) { }

        public List<Group> GroupsByUID(string UID) { return null; }

        public List<Message> MessagesByGID(string GID) { return null; }
    }
    public class DB_Manager : IDB
    {
        private readonly AppDbContext _db;
        public DB_Manager(AppDbContext db) 
        {
            _db = db;
        }

        private bool ContainsUser(User user)
        {
            var tmp_user = _db.Users.FirstOrDefault(u => u.Name == user.Name);
            return tmp_user != null;
        }
        //find method doesn't work
        private bool UserInGroup(int UID, int GID)
        {
            var ugr = _db.UserGroupRelations.SingleOrDefault(ugr => ugr.UserId == UID && ugr.GroupId == GID);
            return ugr != null;
        }
        public bool CreateUser(User user) 
        {
            bool res = false;
            if (!ContainsUser(user)) 
            {
                _db.Users.Add(user);
                res = true;
            }
            _db.SaveChanges();
            return res;
        }
        public void CreateGroup(Group group) 
        {
            _db.Groups.Add(group);
            _db.SaveChanges();
        }
        //doesn't work
        public bool LeaveGroup(Group group, string UID)
        {
            int id;
            if (int.TryParse(UID, out id))
            {
                if (!UserInGroup(id, group.Id))
                    return false;

                _db.UserGroupRelations.Remove(
                    _db.UserGroupRelations.SingleOrDefault(ugr=>ugr.UserId==id&&ugr.GroupId==group.Id));
                _db.SaveChanges();
            }
            return true;
        }

        public bool JoinGroup(Group group, string UID)
        {
            int id;
            if (int.TryParse(UID, out id))
            {
                if (UserInGroup(id, group.Id))
                    return false;
                User tmp = _db.Users.Find(id);
                UserGroupRelation ugr = new UserGroupRelation { Group = group, GroupId = group.Id,
                 Privilege = "user", User = tmp, UserId = id };
                _db.UserGroupRelations.Add(ugr);
                _db.SaveChanges();
            }
            return true;
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

        public List<Message> MessagesByGID(string GID)
        {
            
            return _db.Messages.Where(m=>m.GroupId.ToString()==GID).ToList();
        }

    }
}
