using GL_PROJ.Models.DbContextModels;
using GL_PROJ.Models.DTO;

namespace GL_PROJ.Models.DBService
{
    public interface IDB
    {     
        GroupDTO[] GetGroupsByUserId(uint user_id);
        UserDTO[] GetUsersByGroupId(uint group_id);
        MessageDTO[] GetMessagesByGroupId(uint group_id);
        uint LeaveGroup(uint user_id, uint group_id);
        bool CheckIfInGroup(uint user_id, uint group_id);

        bool CheckIfGroupAdmin(uint user_id, uint group_id);

        uint CreateUser(string username, string password, string description);

        uint CreateGroup(uint user_id, string name, string description);
        uint CreateMessage(uint user_id, uint group_id, string data, uint type);

        uint JoinGroup(uint user_id, uint group_id);

    }
}
