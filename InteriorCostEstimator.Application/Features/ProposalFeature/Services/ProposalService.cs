using InteriorCostEstimator.Application.Features.ProposalFeature.DTOs;
using InteriorCostEstimator.Domain.Entities;
using InteriorCostEstimator.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace InteriorCostEstimator.Application.Features.ProposalFeature.Services
{
    public class ProposalService : IProposalService
    {
        private readonly AppDbContext _context;

        public ProposalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProposalDto> CreateProposalAsync(
            string userId,
            CreateProposalRequest request)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p =>
             p.Id == request.ProjectId &&
             p.UserId == userId);

            if (project is null)
                throw new Exception("Project not found");

            var productIds = request.SelectedProducts
           .Select(x => x.ProductId)
          .ToList();

            var products = await _context.Products.Include(p => p.Category).Include(p => p.Vendor)
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            List<ProposalItem> items = new();

            decimal subtotal = 0;

            foreach (var selected in request.SelectedProducts)
            {
                var product = products
                    .FirstOrDefault(p => p.Id == selected.ProductId);

                if (product is null)
                    throw new Exception("Invalid product");

                decimal total =
                    product.Price * selected.Quantity;

                subtotal += total;

                items.Add(new ProposalItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Quantity = selected.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = total,
                    Product = product,
                });
            }

            decimal vat = subtotal * 0.14m;

            decimal delivery = 250m;

            decimal totalCost =
                subtotal + vat + delivery;

            Proposal proposal = new()
            {
                Id = Guid.NewGuid(),
                ProjectId = project.Id,
                SubTotal = subtotal,
                VAT = vat,
                Delivery = delivery,
                TotalCost = totalCost,
                Items = items
            };

            _context.Proposals.Add(proposal);

            await _context.SaveChangesAsync();


            return new ProposalDto
            {
                ProposalId = proposal.Id,
                ProjectId = proposal.ProjectId,
                SubTotal = proposal.SubTotal,
                VAT = proposal.VAT,
                DeliveryFee = proposal.Delivery,
                TotalCost = proposal.TotalCost,
                RoomType = project.RoomType,
                NumberItems = proposal.Items.Count(),


                Items = proposal.Items.Select(i =>
                    new ProposalItemDto
                    {
                        ProductName = products
                            .First(p => p.Id == i.ProductId)
                            .Name,

                        ImageUrl = products
                            .First(p => p.Id == i.ProductId)
                            .ImageUrl,

                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        TotalPrice = i.TotalPrice,
                        Dimensions = $"{i.Product.Length}x{i.Product.Width}x{i.Product.Height}cm",
                        Category = i.Product.Category.Name,
                        VendorName = i.Product.Vendor.CompanyName,
                        Material = i.Product.Material
                    }).ToList()
            };
        }


        public async Task<ProposalDto> GetProposalByIdAsync(
             string userId,
             Guid proposalId)
        {
            var proposal = await _context.Proposals
                .Include(p => p.Project)
                .Include(p => p.Items)
                    .ThenInclude(i => i.Product).ThenInclude(i => i.Category).Include(p => p.Items)
                    .ThenInclude(i => i.Product).ThenInclude(i => i.Vendor)
                .FirstOrDefaultAsync(p =>
                    p.Id == proposalId &&
                    p.Project.UserId == userId);

            if (proposal is null)
                throw new Exception("Proposal not found");

            return new ProposalDto
            {
                ProposalId = proposal.Id,
                ProjectId = proposal.ProjectId,
                SubTotal = proposal.SubTotal,
                VAT = proposal.VAT,
                DeliveryFee = proposal.Delivery,
                TotalCost = proposal.TotalCost,
                RoomType = proposal.Project.RoomType,
                NumberItems = proposal.Items.Count(),

                Items = proposal.Items
                    .Select(i => new ProposalItemDto
                    {
                        ProductName = i.Product.Name,
                        ImageUrl = i.Product.ImageUrl,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        TotalPrice = i.TotalPrice,
                        Dimensions = $"{i.Product.Length}x{i.Product.Width}x{i.Product.Height}cm",
                        Category = i.Product.Category.Name,
                        VendorName = i.Product.Vendor.CompanyName,
                        Material = i.Product.Material
                    })
                    .ToList()
            };
        }
    }
}
