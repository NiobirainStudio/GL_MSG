using GL_PROJ.Data;
using GL_PROJ.Models.DbContextModels;

namespace GL_PROJ.Models.DBService
{
    public class DB_Manager: IDB
    {
        private readonly AppDbContext _db;
        public DB_Manager(AppDbContext db)
        {
            _db = db;
        }
        /*a method that checks if a user is in the system by comparing the data passed to the method with records in the database*/
        private bool ContainsUser(User user) 
        {
            var tmp_user = _db.Users.FirstOrDefault(u => u.Name == user.Name);
            return tmp_user != null;
        }
        /*perhaps this technical method will be useful in some other situations*/
        private bool ContainsUserByUserID(string UID)
        {
            int id;
            if(int.TryParse(UID, out id))
            {
                var userbyID = _db.Users.SingleOrDefault(u => u.Id == id);
                return userbyID != null;
            }
            return false;
        }

        private bool UserInGroup(int UID, int GID)
        {
            var ugr = _db.UserGroupRelations.SingleOrDefault(ugr => ugr.UserId == UID && ugr.GroupId == GID);
            return ugr != null;
        }
        /*creating a user and checking if the user is in the database*/
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
        /*leaving the group, and checking that the user is in the group*/
        public bool LeaveGroup(Group group, string UID)
        {
            int id;
            if (int.TryParse(UID, out id))
            {
                if (!UserInGroup(id, group.Id))
                    return false;

                _db.UserGroupRelations.Remove(
                    _db.UserGroupRelations.SingleOrDefault(ugr => ugr.UserId == id && ugr.GroupId == group.Id));
                _db.SaveChanges();
            }
            return true;
        }
        /*method for a user to leave a group, including checking that the user is in the group*/
        public bool JoinGroup(Group group, string UID)
        {
            int id;
            if (int.TryParse(UID, out id))
            {
                if (UserInGroup(id, group.Id))
                    return false;
                User tmp = _db.Users.Find(id);
                UserGroupRelation ugr = new UserGroupRelation
                {
                    Group = group,
                    GroupId = group.Id,
                    Privilege = "user",
                    User = tmp,
                    UserId = id
                };
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
        /*in the method get the group ID for a specific userID, and then retrieve a list of groups from the "Groups" table according to this ID*/

        public List<Group> GroupByUID(string UID)
        {
            int[] groud_ids = _db.UserGroupRelations.Where(r => r.UserId.ToString() == UID).Select(rel => rel.GroupId).ToArray();
            return _db.Groups.Where(g => groud_ids.Contains(g.Id)).ToList();
        }
        /*method for retrieving a list of messages according to an incoming group ID*/
        public List<Message> MessagesByGID(string GID)
        {

            return _db.Messages.Where(m => m.GroupId.ToString() == GID).ToList();
        }
    }
}
