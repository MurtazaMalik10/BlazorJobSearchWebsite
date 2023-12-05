using System;

namespace Entities
{
    public class EntUserProfile
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? TimeCreated { get; set; }
    }
}
