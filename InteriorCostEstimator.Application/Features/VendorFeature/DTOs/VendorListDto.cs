using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.VendorFeature.DTOs
{
    public class VendorListDto
    {
        public string VendorName { get; set; } = string.Empty;

        public int TotalProducts { get; set; }
    }
}
