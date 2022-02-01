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

        List<Group> GroupsByUID(string id) { return null; }

        List<Message> MessagesByUID(string id) { return null; }
    }
    public class DB_Manager
    {

    }
}
