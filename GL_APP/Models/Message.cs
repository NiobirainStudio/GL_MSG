namespace GL_APP.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime When { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
