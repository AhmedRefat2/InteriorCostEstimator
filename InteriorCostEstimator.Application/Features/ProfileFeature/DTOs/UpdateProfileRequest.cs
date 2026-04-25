using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProfileFeature.DTOs
{
    public class UpdateProfileRequest
    {
        public string VendorName { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public string LogoImageUrl { get; set; } = string.Empty;
    }
}
