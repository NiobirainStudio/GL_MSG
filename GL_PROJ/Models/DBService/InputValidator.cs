using System;
using System.Text.RegularExpressions;

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
            return Regex.IsMatch(passwd, "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$");
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
