using GL_PROJ.Models.DbContextModels;
using GL_PROJ.Models.DTO;

namespace GL_PROJ.Models.DBService
{
    public interface IDB
    {
        void CreateUser(User user) { }

        //void DeleteUser(User user) { }

        void CreateGroup(Group group) { }

        //void DeleteGroup(Group group) { }

        void LeaveGroup(Group group, string UID) { }

        void JoinGroup(Group group, string UID) { }

        void WriteMessage(Message message) { }
        void CreateUserAsync(User user) { }

        //void DeleteUser(User user) { }

        void CreateGroupAsync(Group group) { }

        //void DeleteGroup(Group group) { }

        void LeaveGroupAsync(Group group, string UID) { }

        void JoinGroupAsync(Group group, string UID) { }

        void WriteMessageAsync(Message message) { }

        public List<Group> GroupsByUID(string UID) { return null; }

        public List<Message> MessagesByGID(string GID) { return null; }
    }
}
