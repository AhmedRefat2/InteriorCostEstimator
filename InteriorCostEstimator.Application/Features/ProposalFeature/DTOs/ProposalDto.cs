using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProposalFeature.DTOs
{
    public class ProposalDto
    {
        public Guid ProposalId { get; set; }

        public Guid ProjectId { get; set; }

        public decimal SubTotal { get; set; }

        public decimal VAT { get; set; }

        public decimal DeliveryFee { get; set; }

        public decimal TotalCost { get; set; }
        
        public List<ProposalItemDto> Items { get; set; }
            = new();
        public string RoomType { get; internal set; }
        public int NumberItems { get; internal set; }
    }
}
