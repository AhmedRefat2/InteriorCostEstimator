using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProposalFeature.DTOs
{
    public class SelectedProductDto
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
