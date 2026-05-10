using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProposalFeature.DTOs
{
    public class CreateProposalRequest
    {
        public Guid ProjectId { get; set; }

        public List<SelectedProductDto> SelectedProducts
        { get; set; } = new();
    }
}
