using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace InteriorCostEstimator.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        // Navigation
        public Vendor? Vendor { get; set; }

        public List<Project> Projects { get; set; } = new();
    }
}
