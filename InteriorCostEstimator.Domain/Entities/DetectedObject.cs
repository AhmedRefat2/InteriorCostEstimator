using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Domain.Entities
{
    public class DetectedObject
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;// bed, sofa...

        // Foreign Key
        public Guid ProjectId { get; set; }

        // Navigation
        public Project Project { get; set; }

        public List<MatchedProduct> MatchedProducts { get; set; } = new();
    }
}
