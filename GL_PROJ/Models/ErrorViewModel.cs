namespace GL_PROJ.Models
{
    // This class defines the error model
    // Details unknown
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
