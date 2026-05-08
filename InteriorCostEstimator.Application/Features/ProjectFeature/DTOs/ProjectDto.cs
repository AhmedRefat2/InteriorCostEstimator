using InteriorCostEstimator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProjectFeature.DTOs
{
    public class ProjectDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
        public string Detection_Image_Url { get; set; }

        public string RoomType { get; set; } // Bedroom, LivingRoom...

        public ProjectStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public decimal CustomerBudget { get; set; }

        public decimal Processing_Time { get; set; }
        //// Foreign Key
        //public string UserId { get; set; }

        //// Navigation
        //public ApplicationUser User { get; set; }

        public List<AiDetectedItemDto> DetectedObjects { get; set; } = new();

        public Proposal? Proposal { get; set; }
    }
}
