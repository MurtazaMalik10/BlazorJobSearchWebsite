using System;
using System.Collections.Generic;

namespace Entities
{
    public class EntJobs
    {
        public int JobId { get; set; }

        public string? CompanyID { get; set; }

        public string? JobTitle { get; set; }

        public string? Description { get; set; }

        public int? CatId { get; set; }

        public string? CatName { get; set; }

        public string? Tags { get; set; }

        public DateTime? TimeUploaded { get; set; }

        public DateTime? LastUpdate { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? Timestamp { get; set; }

        public string? TimePassed { get; set; }

        public string? Thumbnail { get; set; }
    }
}