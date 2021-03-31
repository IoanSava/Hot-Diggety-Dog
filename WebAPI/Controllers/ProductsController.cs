﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Resources;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _repository;

        public ProductsController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(Guid id)
        {
            Product product = _repository.GetById(id);

            if (product == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.ProductEntity, id));
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest(Messages.InvalidData);
            }

            _repository.Create(product);
            return CreatedAtAction("GetProductById", new { id = product.Id }, product);
        }

        [HttpPut]
        public ActionResult UpdateProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest(Messages.InvalidData);
            }

            if (!_repository.Exists(product))
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.ProductEntity, product.Id));
            }

            _repository.Update(product);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult RemoveProduct(Guid id)
        {
            Product product = _repository.GetById(id);

            if (product == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.ProductEntity, id));
            }

            _repository.Remove(product);
            return NoContent();
        }
    }
}
