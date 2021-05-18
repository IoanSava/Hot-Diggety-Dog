﻿using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Queries;
using Application.Features.ProductRequestFeatures.Commands;
using Application.Features.ProductRequestFeatures.Queries;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Resources;
using WebAPI.Controllers;

namespace WebApi.Controllers.v2
{
    [ApiVersion("2.0")]
    public class ProductsRequestsController : BaseApiController
    {
        public ProductsRequestsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetProductsRequestsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            ProductRequest productRequest = await mediator.Send(new GetProductRequestByIdQuery { Id = id });

            if (productRequest == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.ProductRequestEntity, id));
            }

            return Ok(productRequest);
        }

        //[RoleAuthorize("OPERATOR")]
        [HttpPost]
        public async Task<ActionResult> CreateProductsRequests(CreateProductsRequestDto command)
        {
            if (command == null)
            {
                return BadRequest(Messages.InvalidData);
            }

            //User operatorUser = await mediator.Send(new GetUserByIdQuery() { Id = command.OperatorId });

            //if (operatorUser == null)
            //{
            //    return NotFound(Messages.NotFoundMessage(EntitiesConstants.UserEntity, command.OperatorId));
            //}

            //if (operatorUser.Role != Role.OPERATOR)
            //{
            //    return BadRequest(Messages.InvalidData);
            //}

            Guid productsRequestId = await mediator.Send(new CreateProductsRequestCommand() { OperatorId = command.OperatorId, Timestamp = command.Timestamp });

            foreach (CreateProductRequestDto productRequest in command.Products)
            {
                Product product = await mediator.Send(new GetProductByIdQuery() { Id = productRequest.ProductId });

                if (product == null)
                {
                    return NotFound(Messages.NotFoundMessage(EntitiesConstants.ProductRequestEntity, productRequest.ProductId));
                }

                await mediator.Send(new CreateProductRequestCommand() { ProductId = productRequest.ProductId, Quantity = productRequest.Quantity });
            }

            return CreatedAtAction("GetProductsRequestById", new { id = productsRequestId }, await mediator.Send(new GetProductsRequestByIdQuery() { Id = productsRequestId }));
        }

        //private async Task CreateProductRequest(Guid productRequestId, CreateProductsRequestDto request)
        //{
        //    foreach (CreateProductRequestDto createProductRequestDto in request.Products)
        //    {
        //        await mediator.Send(new CreateProductRequestCommand()
        //        {
        //            ProductId = createProductRequestDto.ProductId,
        //            Quantity = createProductRequestDto.Quantity
        //        });
        //    }
        //}
    }
}
