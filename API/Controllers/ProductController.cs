﻿using API.DTOs;
using API.Logging;
using API.Models;
using API.Repositories.Interface;
using API.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repository;
        private readonly ILogging logger;
        private readonly IMapper mapper;
        protected APIResponse response;
        public ProductController(IProductRepository repository, ILogging logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
            response = new();
        }
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAll(string? filter)
        {
            try
            {
                logger.Log("Getting all products", "");
                IEnumerable<Product> productList;
                if (string.IsNullOrEmpty(filter))
                {
                    productList = await repository.GetAllAsync();
                }
                else
                {
                    productList = await repository.GetAllAsync(v => v.Name.Contains(filter));
                }
                response.Result = mapper.Map<List<ProductDto>>(productList);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.ToString() };
            }
            return response;
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Create([FromBody] ProductCreateDto productCreateDto)
        {
            try
            {
                if (productCreateDto == null)
                {
                    return BadRequest();
                }
                Product model = mapper.Map<Product>(productCreateDto);
                await repository.CreateAsync(model);
                response.Result = mapper.Map<ProductDto>(model);
                response.StatusCode = HttpStatusCode.Created;
                return Ok();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.ToString() };
            }
            return response;
        }
    }
}
