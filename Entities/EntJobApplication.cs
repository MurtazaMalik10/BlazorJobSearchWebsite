namespace Entities
{
    public class EntJobApplication
    {
        public int JobId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
        public string? CVFile { get; set; } // Property for CV file in base64 string format
        // Other properties related to a job application
    }
}
