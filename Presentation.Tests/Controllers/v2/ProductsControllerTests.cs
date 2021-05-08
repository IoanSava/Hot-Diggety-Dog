﻿using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using WebApi.Controllers.v2;
using Xunit;

namespace Presentation.Tests.Controllers.v2
{
    public class ProductsControllerTests : DatabaseBaseTest
    {
        private readonly Mock<IMediator> Mediator;

        public ProductsControllerTests()
        {
            Mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Mediatr_GetProducts_ShouldReturn_OK()
        {
            Mediator.Setup(x => x.Send(It.IsAny<GetProductsQuery>(), new System.Threading.CancellationToken()));
            var productsController = new ProductsController(Mediator.Object);

            //Action
            var result = productsController.Get().Result;

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Mediatr_GetProductBy_NonExisting_Id_ShouldReturn_NotFound()
        {
            //Arrange
            Guid productId = Guid.Parse("27da19e4-e1a5-4713-8643-8441630249cf");
            Mediator.Setup(x => x.Send(It.IsAny<GetProductByIdQuery>(), new System.Threading.CancellationToken()));
            var productsController = new ProductsController(Mediator.Object);

            //Action
            var result = productsController.GetById(productId).Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Mediatr_Create_Null_Product_ShouldReturn_BadRequest()
        {
            //Arrange
            CreateProductCommand createdProduct = null;

            Mediator.Setup(x => x.Send(It.IsAny<CreateProductCommand>(), new System.Threading.CancellationToken()));
            var productsController = new ProductsController(Mediator.Object);

            //Action
            var result = productsController.Create(createdProduct).Result;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Mediatr_Create_New_Product_ShouldReturn_OK()
        {
            //Arrange
            CreateProductCommand createdProduct = new CreateProductCommand
            {
                Name = "Test",
                Description = "Test Description",
                Category = "Test",
                Price = 25
            };

            Mediator.Setup(x => x.Send(It.IsAny<CreateProductCommand>(), new System.Threading.CancellationToken()));
            var productsController = new ProductsController(Mediator.Object);

            //Action
            var result = productsController.Create(createdProduct).Result;

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Mediatr_Update_NonExisting_Product_ShouldReturn_NotFound()
        {
            //Arrange
            Guid productId = Guid.Parse("86a2006d-4f8a-4e74-b969-9ffa9563efd1");
            UpdateProductCommand updatedProduct = new UpdateProductCommand
            {
                Id = productId,
                Name = "Test",
                Description = "Test",
                Price = 25,
                Category = "Test"
            };

            Mediator.Setup(x => x.Send(It.IsAny<UpdateProductCommand>(), new System.Threading.CancellationToken()));
            var productsController = new ProductsController(Mediator.Object);

            //Action
            var result = productsController.Update(productId, updatedProduct).Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Mediatr_Remove_NonExisting_Product_ShouldReturn_NotFound()
        {
            //Arrange
            Guid productId = Guid.Parse("2ef4b146-9d1d-403a-8ab2-6cf043b8c449");

            Mediator.Setup(x => x.Send(It.IsAny<UpdateProductCommand>(), new System.Threading.CancellationToken()));
            var productsController = new ProductsController(Mediator.Object);

            //Action
            var result = productsController.Remove(productId).Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
