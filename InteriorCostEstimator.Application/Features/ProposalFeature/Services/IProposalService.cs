using InteriorCostEstimator.Application.Features.ProposalFeature.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProposalFeature.Services
{
    public interface IProposalService
    {
        Task<ProposalDto> CreateProposalAsync(
            string userId,
            CreateProposalRequest request);

        Task<ProposalDto> GetProposalByIdAsync(
            string userId,
            Guid proposalId);
    }
}
