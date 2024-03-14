using API.DTOs;
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

        [HttpGet("{id:guid}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetProduct(Guid id)
        {
            try
            {
                var prodcut = await repository.GetAsync(v => v.Id == id);
                if (prodcut == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(response);
                }
                response.Result = mapper.Map<ProductDto>(prodcut);
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
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


        [HttpPut("{id:guid}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Update(Guid id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            try
            {
                if (productUpdateDto == null || id != productUpdateDto.Id)
                {
                    return BadRequest();
                }
                Product model = mapper.Map<Product>(productUpdateDto);
                await repository.UpdateAsync(model);
                response.StatusCode = HttpStatusCode.NoContent;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.ToString() };
            }
            return response;
        }
        [HttpDelete("{id:guid}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Delete(Guid id)
        {
            try
            {
                var product = await repository.GetAsync(u => u.Id == id);
                if (product == null)
                {
                    return NotFound();
                }
                await repository.DeleteAsync(product);
                response.StatusCode = HttpStatusCode.NoContent;
                return Ok(response);
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
