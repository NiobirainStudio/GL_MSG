using System;
namespace GL_PROJ.Models.DBService
{
    public class InputValidator
    {
        public InputValidator()
        {
        }
        //TODO add regex validation
        public bool PasswordValid(string passwd)
        {
            return true;
        }
        //TODO add regex validation
        public bool UsernameValid(string username)
        {
            return true;
        }
        //TODO
        public bool MessageTypeValid(uint type)
        {
            return true;
        }
    }
}
