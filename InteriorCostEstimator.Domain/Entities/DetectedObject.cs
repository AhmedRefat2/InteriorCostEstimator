using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Domain.Entities
{
    public class DetectedObject
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public decimal Confidence { get; set; }

        public string Crop_Url { get; set; }

        // Foreign Key
        public Guid ProjectId { get; set; }

        // Navigation
        public Project Project { get; set; }

        public List<MatchedProduct> MatchedProducts { get; set; } = new();
    }
}
