namespace GL_PROJ.Models.DTO
{
    public class GroupDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public MessageDTO LastMessage { get; set; }
    }
}
