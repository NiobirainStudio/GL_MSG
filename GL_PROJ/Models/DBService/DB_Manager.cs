using GL_PROJ.Data;
using GL_PROJ.Models.DbContextModels;
using Microsoft.EntityFrameworkCore;
using GL_PROJ.Models;
using GL_PROJ.Models.DTO;
using System.Linq;
using System;

namespace GL_PROJ.Models.DBService
{
    public class DB_Manager: IDB
    {
        private readonly AppDbContext _db;
        private InputValidator IV;
        public DB_Manager(AppDbContext db)
        {
            _db = db;
            IV = new InputValidator();
        }

     /*   private MessageDTO LastGroupMessage(uint GID)
        {
            var msg_list = GetMessagesByGroupId(GID).ToList();
            msg_list.OrderBy(m => m.Date);
            return msg_list[msg_list.Count - 1];
        }*/
        private bool ContainsUserByUserID(int UID)
        {
                var userbyID = _db.Users.SingleOrDefault(u => u.Id == UID);
                return userbyID != null;
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
        public GroupDTO[] GetGroupsByUserId(uint user_id) /*работает*/
        {
            var groupIDsbyUserID = GetGroupIdsByUserId(user_id);
            var groupMessages = _db.Messages.Where(message => groupIDsbyUserID.Contains(message.GroupId)).
                Select(message => new MessageDTO
                {
                    MessageId = message.Id,
                    GroupId = message.GroupId,
                    Data = message.Data,
                    Date = message.Date,
                    Type = message.Type,
                    UserId = message.UserId
                }).OrderBy(message => message.Date).ToList();

            MessageDTO lastMessage = groupMessages[groupMessages.Count - 1];
            return _db.Groups.Where(group => groupIDsbyUserID.Contains(group.Id)).
                 Select(group => new GroupDTO
                 {
                     Id = group.Id,
                     Description = group.Description,
                     Name = group.Name,
                     LastMessage = lastMessage
                 }).ToArray();

        }

        public UserDTO[] GetUsersByGroupId(uint group_id) /*работает*/
        {
            var usersIDbyGroupID = _db.UserGroupRelations.Where(relation => relation.GroupId == group_id).Select(relation => relation.UserId);
            return _db.Users.Where(user => usersIDbyGroupID.Contains(user.Id)).Select(user => new UserDTO { Id = user.Id, Description = user.Description, Name = user.Name }).ToArray();
        }
        /*ordered by date ascending*/
        public MessageDTO[] GetMessagesByGroupId(uint group_id) /*работает*/
        {
            return _db.Messages.Where(message => message.GroupId == group_id).
                Select(message => new MessageDTO { MessageId = message.Id, GroupId = message.GroupId, Data = message.Data, 
                    Date = message.Date, Type = message.Type, UserId = message.UserId})
                .OrderBy(message => message.Date).ToArray();
        }
        /*checking methods*/
        //return true if in group?
        public bool CheckIfInGroup(uint user_id, uint group_id)
        {
            var UGR = _db.UserGroupRelations.Find(user_id, group_id);
            return UGR != null;
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
        public int[] GetGroupIdsByUserId(uint user_id) /*этот метод пришлось сделать интом, а то нормально не конвертируется в uint*/
        {
            return _db.UserGroupRelations.Where(relation => relation.UserId == user_id).Select(relation => relation.GroupId).ToArray();
        }
        /*for implementing, high-priority*/
        public bool CheckIfGroupAdmin(uint user_id, uint group_id)
        {
            var UGR = _db.UserGroupRelations.Find(user_id, group_id);
            return UGR.Privilege == "admin";
        }

        public uint CreateUser(string username, string password, string description)
        {
            uint res = 0;
            if (username == null || username == "")
                res = 1;
            else if (_db.Users.SingleOrDefault(u => u.Name == username) != null)
                res = 2;
            else if (!IV.UsernameValid(username))
                res = 3;
            else if (password == null || password == "")
                res = 4;
            else if (!IV.PasswordValid(password))
                res = 5;
            if (res == 0)
            {
                _db.Users.Add(new User {
                    Name = username,
                    Password = password,
                    Description = description
                });
                _db.SaveChanges();
            }
            return res;
        }

        public uint CreateGroup(uint user_id, string name, string description)
        {
            uint res = 0;
            if (GroupByName(name) != null)
                res = 1;
            else if (name == null || name == "")
                res = 2;
            if(res == 0)
            {
                _db.Groups.Add(new Group
                {
                    Name = name,
                    Description = description
                });
                _db.SaveChanges();
                int groupid = GroupByName(name).Id;
                JoinGroup(user_id, (uint)groupid);
                MakeAdmin((int)user_id, groupid);
            }
            return res;

        }
        //TODO add invalid type check
        public uint CreateMessage(uint user_id, uint group_id, string data, uint type)
        {
            uint res = 0;
            if (!CheckIfInGroup(user_id, group_id))
                res = 1;
            else if (data == null || data == "")
                res = 2;
            else if (IV.MessageTypeValid(type))
                res = 3;
            if(res == 0)
            {
                _db.Messages.Add(new Message
                {
                    Data = data,
                    Date = DateTime.Now,
                    Type = (int)type,
                    UserId = (int)user_id,
                    GroupId = (int)group_id
                });
                _db.SaveChanges();
            }
            return res;
        }

        public uint JoinGroup(uint user_id, uint group_id)
        {
            uint res = 0;
            if (CheckIfInGroup(user_id,group_id))
                res = 1;
            else
            {
                _db.UserGroupRelations.Add(new UserGroupRelation
                {
                    UserId = (int)user_id,
                    GroupId = (int)group_id,
                    Privilege = "user"
                });
                _db.SaveChanges();
            }
            return res;
        }

        /*low-priority-methods*/
        public uint DeleteUser(uint user_id)
        {
            throw new NotImplementedException();
        }

        public uint EditUser(uint user_id, string username, string description)
        {
            throw new NotImplementedException();
        }

        public uint DeleteGroup(uint user_id, uint group_id)
        {
            throw new NotImplementedException();
        }

        public uint EditGroup(uint user_id, uint group_id, string name, string description)
        {
            throw new NotImplementedException();
        }

        public uint DeleteMessage(uint user_id, uint message_id)
        {
            throw new NotImplementedException();
        }

        public uint EditMessage(uint user_id, uint message_id, string data)
        {
            throw new NotImplementedException();
        }

        public uint EditPrivilege(uint user_id, uint target_user_id, uint group_id, uint new_privilege)
        {
            throw new NotImplementedException();
        }
    }
}
