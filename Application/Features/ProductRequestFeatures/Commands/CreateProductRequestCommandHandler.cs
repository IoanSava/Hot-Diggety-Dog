﻿using Application.Interfaces;
using Domain.Dtos;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    class CreateProductRequestCommandHandler : IRequestHandler<CreateProductRequestCommand, Guid>
    {
        private readonly IRepository<ProductRequest> productRequestRepository;

        public CreateProductRequestCommandHandler(IRepository<ProductRequest> productRequestRepository, IInventoryProductsRepository inventoryProductsRepository)
        {
            this.productRequestRepository = productRequestRepository;
        }

        public async Task<Guid> Handle(CreateProductRequestCommand request, CancellationToken cancellationToken)
        {
            ProductRequest productRequest = new()
            {
                ProductId = request.ProductId,
                Product = request.Product,
                Quantity = request.Quantity
            };

            await productRequestRepository.CreateAsync(productRequest);
            return productRequest.Id;

        }
    }
}