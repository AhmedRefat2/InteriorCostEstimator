using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Domain.Entities
{

    public enum ProjectStatus
    {
        Uploaded = 1,
        Processing = 2,
        Completed = 3
    }


    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string RoomType { get; set; } // Bedroom, LivingRoom...

        public ProjectStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key
        public string UserId { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }

        public List<DetectedObject> DetectedObjects { get; set; } = new();

        public List<MatchedProduct> MatchedProducts { get; set; } = new();

        public Proposal? Proposal { get; set; }
    }
}
