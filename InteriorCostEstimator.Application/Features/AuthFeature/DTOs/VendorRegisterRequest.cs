using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.AuthFeature.DTOs
{
    public class VendorRegisterRequest : RegisterRequest
    {
        public string? CompanyName { get; set; } = string.Empty;

        public string? Phone { get; set; } = string.Empty;

        public string? WhatsAppLink { get; set; } = string.Empty;

        public string? Location { get; set; } = string.Empty;

        public string? Bio { get; set; } = string.Empty;

        public string? LogoImageUrl { get; set; } = string.Empty;
    }
}
