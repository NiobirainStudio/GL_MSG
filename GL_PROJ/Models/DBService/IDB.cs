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
        GroupDTO[] GetGroupsByUserId(int user_id);

        //------------------------------//
        // Retrieves all users of a group
        //------------------------------//
        // Priority - High
        //------------------------------//
        UserDTO[] GetUsersByGroupId(int group_id);

        //------------------------------//
        // Retrieves all messages of a group (sort by date)
        //------------------------------//
        // Priority - High
        //------------------------------//
        MessageDTO[] GetMessagesByGroupId(int group_id);

        //------------------------------//
        // Used on socket group joining
        //------------------------------//
        // Priority - High
        //------------------------------//
        int[] GetGroupIdsByUserId(int user_id);

        //------------------------------//
        // Checking functions
        //------------------------------//

        //------------------------------//
        // Priority - High
        //------------------------------//
        bool CheckIfGroupAdmin(int user_id, int group_id);

        //------------------------------//
        // Priority - High
        //------------------------------//
        bool CheckIfInGroup(int user_id, int group_id);
        


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
        int CreateUser(string username, string password, string description);

        //------------------------------//
        // Delete user (check if user is target_user)
        //------------------------------//
        // Priority - Low
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //------------------------------//
        int DeleteUser(int user_id);

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
        int EditUser(int user_id, string username, string description);



        //------------------------------//
        // Groups
        //------------------------------//
        // Creating group by name (description is optional) & (check if group name is not taken)
        //------------------------------//
        // Priority - High
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //  1 - Invalid name
        //  2 - Name is already taken
        //------------------------------//
        int CreateGroup(int user_id, string name, string description);

        //------------------------------//
        // Delete group (check if user is admin)
        //------------------------------//
        // Priority - Low
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //  1 - You don't have permission to delete this group
        //------------------------------//
        int DeleteGroup(int user_id, int group_id);

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
        int EditGroup(int user_id, int group_id, string name, string description);



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
        int CreateMessage(int user_id, int group_id, string data, int type);

        //------------------------------//
        // Delete message (check if user is author or admin)
        //------------------------------//
        // Priority - Low
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //  1 - You can only delete your own messages | not admin
        //------------------------------//
        int DeleteMessage(int user_id, int message_id);

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
        int EditMessage(int user_id, int message_id, string data);



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
        int JoinGroup(int user_id, int group_id);

        //------------------------------//
        // Leave group (check if is in group)
        //------------------------------//
        // Priority - High
        //------------------------------//
        // Return codes:
        //  0 - All is good
        //  1 - You are not a member of this group
        //------------------------------//
        int LeaveGroup(int user_id, int group_id);

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
        int EditPrivilege(int user_id, int target_user_id, int group_id, int new_privilege);
        
    }
}