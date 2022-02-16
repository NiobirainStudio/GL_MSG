using GL_PROJ.Data;
using GL_PROJ.Models.DbContextModels;
using Microsoft.EntityFrameworkCore;
using GL_PROJ.Models;
using GL_PROJ.Models.DTO;
using System.Linq;
using System;

namespace GL_PROJ.Models.DBService
{
    public class DB_Manager : IDB
    {
        private readonly AppDbContext _db;
        private InputValidator IV;
        public DB_Manager(AppDbContext db)
        {
            _db = db;
            IV = new InputValidator();
        }

        private bool ContainsUserByUserID(int UID)
        {
            var userbyID = _db.Users.SingleOrDefault(u => u.Id == UID);
            return userbyID != null;
        }

        private User GetUserByUserId(int user_id)
        {
            return _db.Users.Find(user_id);
        }

        //questionable solution
        private void MakeAdmin(int UID, int GID)
        {
            var UGR = _db.UserGroupRelations.Find(UID, GID);
            if (UGR != null)
            {
                UGR.Privilege = "admin";
                _db.SaveChanges();
            }

        }

        private Group GroupByName(string name)
        {
            return _db.Groups.FirstOrDefault(g => g.Name == name);
        }

        /*next three methods work with DTO models*/
        public GroupDTO[] GetGroupsByUserId(int user_id) /*работает*/
        {
            var data = (from rel in _db.UserGroupRelations
                        join grp in _db.Groups
                        on rel.GroupId equals grp.Id
                        where rel.UserId == user_id
                        select new GroupDTO
                        {
                            Id = grp.Id,
                            Description = grp.Description,
                            Name = grp.Name,
                            LastMessage = null
                        }).ToArray();

            foreach(var grp in data)
                grp.LastMessage = (from msg in _db.Messages
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
                                   }).FirstOrDefault();

            return data;
        }

        public UserDTO[] GetUsersByGroupId(int group_id) /*работает*/
        {
            var usersIDbyGroupID = _db.UserGroupRelations.Where(relation => relation.GroupId == group_id).Select(relation => relation.UserId);
            return _db.Users.Where(user => usersIDbyGroupID.Contains(user.Id)).Select(user => new UserDTO { Id = user.Id, Description = user.Description, Name = user.Name }).ToArray();
        }

        /*ordered by date ascending*/
        public MessageDTO[] GetMessagesByGroupId(int group_id) /*работает*/
        {
            return _db.Messages.Where(message => message.GroupId == group_id).
                Select(message => new MessageDTO
                {
                    MessageId = message.Id,
                    GroupId = message.GroupId,
                    Data = message.Data,
                    Date = message.Date,
                    Type = message.Type,
                    UserId = message.UserId
                })
                .OrderBy(message => message.Date).ToArray();
        }
        /*checking methods*/
        //return true if in group?
        public bool CheckIfInGroup(int user_id, int group_id)
        {
            var UGR = _db.UserGroupRelations.Find(user_id, group_id);
            return UGR != null;
        }
        public int LeaveGroup(int user_id, int group_id)
        {
            if (!CheckIfInGroup(user_id, group_id))
                return 1;

            _db.UserGroupRelations.Remove(_db.UserGroupRelations.SingleOrDefault(relation => relation.UserId == user_id && relation.GroupId == group_id));
            _db.SaveChanges();
            return 0;
        }
        public int[] GetGroupIdsByUserId(int user_id) /*этот метод пришлось сделать интом, а то нормально не конвертируется в int*/
        {
            return _db.UserGroupRelations.Where(relation => relation.UserId == user_id).Select(relation => relation.GroupId).ToArray();
        }
        /*for implementing, high-priority*/
        public bool CheckIfGroupAdmin(int user_id, int group_id)
        {
            var UGR = _db.UserGroupRelations.Find(user_id, group_id);
            return UGR.Privilege == "admin";
        }

        public int CreateUser(string username, string password, string description)
        {
            if (username == null || username == "")
                return 0;
            if (_db.Users.SingleOrDefault(u => u.Name == username) != null)
                return 2;
            if (!IV.UsernameValid(username))
                return 3;
            if (password == null || password == "")
                return 4;
            if (!IV.PasswordValid(password))
                return 5;


            _db.Users.Add(new User
            {
                Name = username,
                Password = password,
                Description = description
            });
            _db.SaveChanges();
            
            return 0;
        }

        public int CreateGroup(int user_id, string name, string description)
        {
            if (name == "") 
                return 1;
            if (GroupByName(name) != null)
                return 2;


            _db.Groups.Add(new Group
            {
                Name = name,
                Description = description
            });
            _db.SaveChanges();

            int groupid = GroupByName(name).Id;
            JoinGroup(user_id, groupid);
            MakeAdmin(user_id, groupid);
            CreateMessage(user_id, groupid, $"{GetUserByUserId(user_id).Name} created the group {name}", 1);

            return 0;

        }
        //TODO add invalid type check
        public int CreateMessage(int user_id, int group_id, string data, int type)
        {
            if (!CheckIfInGroup(user_id, group_id))
                return 1;
            else if (data == null || data == "")
                return 2;
            else if (!IV.MessageTypeValid(type))
                return 3;


            _db.Messages.Add(new Message
            {
                Data = data,
                Date = DateTime.Now,
                Type = (int)type,
                UserId = (int)user_id,
                GroupId = (int)group_id
            });

            _db.SaveChanges();
            return 0;
        }

        public int JoinGroup(int user_id, int group_id)
        {
            if (CheckIfInGroup(user_id, group_id))
                return 1;


            _db.UserGroupRelations.Add(new UserGroupRelation
            {
                UserId = (int)user_id,
                GroupId = (int)group_id,
                Privilege = "user"
            });
            _db.SaveChanges();

            return 0;
        }

        /*low-priority-methods*/
        public int DeleteUser(int user_id)
        {
            throw new NotImplementedException();
        }

        public int EditUser(int user_id, string username, string description)
        {
            throw new NotImplementedException();
        }

        public int DeleteGroup(int user_id, int group_id)
        {
            throw new NotImplementedException();
        }

        public int EditGroup(int user_id, int group_id, string name, string description)
        {
            throw new NotImplementedException();
        }

        //Not tested
        public int DeleteMessage(int user_id, int message_id)
        {
            var msg = _db.Messages.Find(message_id);
            var user = _db.Users.Find(user_id);
            var group = _db.Groups.Find(msg.GroupId);
            var ugr = _db.UserGroupRelations.Find(user.Id, group.Id);
            if (msg == null || user == null || group == null || ugr == null||msg.UserId!=user_id||ugr.Privilege!="admin")
                return 1;
            else
            {
                _db.Messages.Remove(msg);
                _db.SaveChanges();
                return 0;
            }
        }
        //Not Tested
        public int EditMessage(int user_id, int message_id, string data)
        {
            var msg = _db.Messages.Find(message_id);
            var user = _db.Users.Find(user_id);
            var group = _db.Groups.Find(msg.GroupId);
            var ugr = _db.UserGroupRelations.Find(user.Id, group.Id);
            if (msg == null || user == null || group == null || ugr == null || msg.UserId != user_id)
                return 1;
            if (data == null || data == "")
                return 2;
            msg.Data = data;
            _db.SaveChanges();
                return 0;
        }
        //TODO adapt for int privilleges
        public int EditPrivilege(int user_id, int target_user_id, int group_id, int new_privilege)
        {
            var user = _db.Users.Find(user_id);
            var target_user = _db.Users.Find(target_user_id);
            var group = _db.Groups.Find(group_id);
            return 0;
        }
    }
}
