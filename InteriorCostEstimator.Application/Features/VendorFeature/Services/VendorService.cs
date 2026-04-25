using InteriorCostEstimator.Application.Features.ProfileFeature.DTOs;
using InteriorCostEstimator.Application.Features.VendorFeature.DTOs;
using InteriorCostEstimator.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace InteriorCostEstimator.Application.Features.VendorFeature.Services
{
    public class VendorService : IVendorService
    {
        private readonly AppDbContext _context;

        public VendorService(AppDbContext context)
        {
            _context = context;
        }



        public async Task<ProfileDto?> GetProfileAsync(
           string userId)
        {
            return await _context.Vendors
                .Include(v => v.User)
                .Include(v => v.Products)
                .Where(v => v.UserId == userId)
                .Select(v => new ProfileDto
                {
                    UserName = v.User.FullName,
                    VendorName = v.CompanyName,
                    Email = v.User.Email!,
                    Location = v.Location,
                    LogoImageUrl = v.LogoImageUrl,
                    PhoneNumber = v.Phone,
                    Bio = v.Bio,
                    TotalProducts = v.Products.Count()
                })
                .FirstOrDefaultAsync();
        }



        public async Task UpdateProfileAsync(
           string userId,
           UpdateProfileRequest request)
        {
            var vendor = await _context.Vendors
                 .FirstOrDefaultAsync(
                     x => x.UserId == userId);

            if (vendor is null)
                throw new Exception("Vendor not found");


            vendor.CompanyName = request.VendorName;
            vendor.Location = request.Location;
            vendor.Phone = request.PhoneNumber;
            vendor.Bio = request.Bio;
            vendor.LogoImageUrl = request.LogoImageUrl;

            _context.Vendors.Update(vendor);
            await _context.SaveChangesAsync();
        }



        public async Task<IEnumerable<VendorListDto>>
           GetAllVendorsAsync()
        {
            return await _context.Vendors
               .Include(v => v.Products)
               .Select(v => new VendorListDto
               {
                   VendorName = v.CompanyName,
                   TotalProducts = v.Products.Count()
               })
               .ToListAsync();
        }

    }
}
