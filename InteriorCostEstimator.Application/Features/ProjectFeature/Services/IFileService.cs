using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProjectFeature.Services
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(
            IFormFile file,
            string folderName);
    }
}
