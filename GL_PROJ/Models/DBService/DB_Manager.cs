using GL_PROJ.Data;
using GL_PROJ.Models.DbContextModels;
using Microsoft.EntityFrameworkCore;
using GL_PROJ.Models;
using GL_PROJ.Models.DTO;
using System.Linq;

namespace GL_PROJ.Models.DBService
{
    public class DB_Manager: IDB
    {
        private readonly AppDbContext _db;
        public DB_Manager(AppDbContext db)
        {
            _db = db;
        }
  
        /*next three methods work with DTO models*/
        public GroupDTO[] GetGroupsByUserId(uint user_id)
        {
           var groupIDsbyUserID = _db.UserGroupRelations.Where(relation => relation.UserId == user_id).Select(relation => relation.GroupId);
           return _db.Groups.Where(group => groupIDsbyUserID.Contains(group.Id)).
                Select(group => new GroupDTO { Id = group.Id, Description = group.Description, Name = group.Name }).ToArray();

        }

        public UserDTO[] GetUsersByGroupId(uint group_id)
        {
            var usersIDbyGroupID = _db.UserGroupRelations.Where(relation => relation.GroupId == group_id).Select(relation => relation.UserId);
            return _db.Users.Where(user => usersIDbyGroupID.Contains(user.Id)).Select(user => new UserDTO { Id = user.Id, Description = user.Description, Name = user.Name }).ToArray();
        }
        /*ordered by date ascending*/
        public MessageDTO[] GetMessagesByGroupId(uint group_id)
        {
            return _db.Messages.Where(message => message.GroupId == group_id).
                Select(message => new MessageDTO { MessageId = message.Id, GroupId = message.GroupId, Data = message.Data, 
                    Date = message.Date, Type = message.Type, UserId = message.UserId})
                .OrderBy(message => message.Date).ToArray();
        }
        /*checking methods*/
        public bool CheckIfInGroup(uint user_id, uint group_id)
        {
            var userGroupRelation = _db.UserGroupRelations.FirstOrDefault(relation => relation.UserId == user_id && group_id == relation.GroupId);
            return userGroupRelation != null;
        }
        public uint LeaveGroup(uint user_id, uint group_id)
        {
            if (!CheckIfInGroup(user_id, group_id))
            {
                return 1;
            }
            _db.UserGroupRelations.Remove(_db.UserGroupRelations.SingleOrDefault(relation => relation.UserId == user_id && relation.GroupId == group_id));
            _db.SaveChanges();
            return 0;
        }
        /*for implementing, high-priority*/
        public bool CheckIfGroupAdmin(uint user_id, uint group_id)
        {
            throw new NotImplementedException();
        }

        public uint CreateUser(string username, string password, string description)
        {
            throw new NotImplementedException();
        }

        public uint CreateGroup(uint user_id, string name, string description)
        {
            throw new NotImplementedException();
        }

        public uint CreateMessage(uint user_id, uint group_id, string data, uint type)
        {
            throw new NotImplementedException();
        }

        public uint JoinGroup(uint user_id, uint group_id)
        {
            throw new NotImplementedException();
        }
    }
}
