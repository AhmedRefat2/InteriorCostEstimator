using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProfileFeature.DTOs
{
    public class ProfileDto
    {
        public string UserName { get; set; } = string.Empty;

        public string VendorName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string LogoImageUrl { get; set; } = string.Empty;

        public int TotalProducts { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;
    }
}
