using GL_PROJ.Models.DbContextModels;
using GL_PROJ.Models.DTO;

namespace GL_PROJ.Models.DBService
{
    public interface IDB
    {
        //------------------------------//
        // Utility
        //------------------------------//
        // Retrieves all groups for this user
        //------------------------------//
        // Priority - High
        //------------------------------//
        GroupDTO[] GetGroupsByUserId(uint user_id);

        //------------------------------//
        // Retrieves all users of a group
        //------------------------------//
        // Priority - High
        //------------------------------//
        UserDTO[] GetUsersByGroupId(uint group_id);

        //------------------------------//
        // Retrieves all messages of a group (sort by date)
        //------------------------------//
        // Priority - High
        //------------------------------//
        MessageDTO[] GetMessagesByGroupId(uint group_id);

        //------------------------------//
        // Used on socket group joining
        //------------------------------//
        // Priority - High
        //------------------------------//
        int[] GetGroupIdsByUserId(uint user_id);

        //------------------------------//
        // Checking functions
        //------------------------------//

        //------------------------------//
        // Priority - High
        //------------------------------//
        bool CheckIfGroupAdmin(uint user_id, uint group_id);

        //------------------------------//
        // Priority - High
        //------------------------------//
        bool CheckIfInGroup(uint user_id, uint group_id);



        //------------------------------//
        // Users
        //------------------------------//
        // Creating user by username & password (description is optional) & (check if username is not taken)
        //------------------------------//
        // Priority - High
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //  1 - Username is null
        //  2 - Username is already taken
        //  3 - Invalid username (too small/large, includes certain chars...)
        //  4 - Password is null
        //  5 - Invalid password (too small/large, includes certain chars...)
        //------------------------------//
        uint CreateUser(string username, string password, string description);

        //------------------------------//
        // Delete user (check if user is target_user)
        //------------------------------//
        // Priority - Low
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //------------------------------//
        uint DeleteUser(uint user_id);

        //------------------------------//
        // Edit user (check if username is not taken)
        //------------------------------//
        // Priority - Low
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //  1 - Username is already taken
        //  2 - Invalid username
        //------------------------------//
        uint EditUser(uint user_id, string username, string description);



        //------------------------------//
        // Groups
        //------------------------------//
        // Creating group by name (description is optional) & (check if group name is not taken)
        //------------------------------//
        // Priority - High
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //  1 - Name is already taken
        //------------------------------//
        uint CreateGroup(uint user_id, string name, string description);

        //------------------------------//
        // Delete group (check if user is admin)
        //------------------------------//
        // Priority - Low
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //  1 - You don't have permission to delete this group
        //------------------------------//
        uint DeleteGroup(uint user_id, uint group_id);

        //------------------------------//
        // Edit group (check if user is admin)
        //------------------------------//
        // Priority - Low
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //  1 - You don't have permission to edit this group
        //  2 - Name is already taken
        //  3 - Invalid name
        //------------------------------//
        uint EditGroup(uint user_id, uint group_id, string name, string description);



        //------------------------------//
        // Messages
        //------------------------------//
        // Creating message by user_id, group_id, data and type (check if user is in the group)
        //------------------------------//
        // Priority - High
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //  1 - You are not a member of this group
        //  2 - Data is null
        //  3 - Invalid type
        //------------------------------//
        uint CreateMessage(uint user_id, uint group_id, string data, uint type);

        //------------------------------//
        // Delete message (check if user is author or admin)
        //------------------------------//
        // Priority - Low
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //  1 - You can only delete your own messages | not admin
        //------------------------------//
        uint DeleteMessage(uint user_id, uint message_id);

        //------------------------------//
        // Edit message (check if user is author & if data is not null)
        //------------------------------//
        // Priority - Low
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //  1 - You can only edit your own messages
        //  2 - Data is null
        //------------------------------//
        uint EditMessage(uint user_id, uint message_id, string data);



        //------------------------------//
        // User-Group Relations
        //------------------------------//
        // Joining a group by group_id (check if user is not already in group)
        //------------------------------//
        // Priority - High
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //  1 - You are already a member of this group
        //------------------------------//
        uint JoinGroup(uint user_id, uint group_id);

        //------------------------------//
        // Leave group (check if is in group)
        //------------------------------//
        // Priority - High
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //  1 - You are not a member of this group
        //------------------------------//
        uint LeaveGroup(uint user_id, uint group_id);

        //------------------------------//
        // Edit user privilege (check if user_id is admin & user_id != target_id)
        //------------------------------//
        // Priority - Low
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //  1 - You are not a member of this group
        //  2 - Target user is not a member of this group
        //  3 - You cannot edit your own privileges
        //  4 - You don't have permission
        //------------------------------//
        uint EditPrivilege(uint user_id, uint target_user_id, uint group_id, uint new_privilege);
    }
}
