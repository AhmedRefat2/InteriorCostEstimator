using InteriorCostEstimator.Application.Features.ProfileFeature.DTOs;
using InteriorCostEstimator.Application.Features.VendorFeature.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.VendorFeature.Services
{
    public interface IVendorService
    {
        Task<ProfileDto?> GetProfileAsync(string userId);

        Task UpdateProfileAsync(
            string userId,
            UpdateProfileRequest request);

        Task<IEnumerable<VendorListDto>> GetAllVendorsAsync();
    }
}
