using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Domain.Entities
{
    public class MatchedProduct
    {
        public Guid Id { get; set; }

        // Foreign Keys
        public Guid ProjectId { get; set; }

        public Guid DetectedObjectId { get; set; }

        public Guid ProductId { get; set; }

        // Matching Info
        public double SimilarityScore { get; set; }

        public int Rank { get; set; } // 1 = best match

        // Navigation
        public Project Project { get; set; }

        public DetectedObject DetectedObject { get; set; }

        public Product Product { get; set; }
    }
}
