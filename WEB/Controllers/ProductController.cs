﻿
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEB.Models.DTOs;
using WEB.Services;
using WEB.Utilities;

namespace WEB.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
 
        public ProductController(IProductService productService)
        {
            this.productService = productService;
    
        }
        public async Task<IActionResult> Index(string filter)
        {
            List<ProductDto> products = new();
            var rs = await productService.GetAllAsync(filter);
            if (rs != null && rs.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(rs.Result));
            }
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var rs = await productService.CreateAsync(model);
                if (rs != null && rs.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var rs = await productService.UpdateAsync(model);
                if(rs != null && rs.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var rs = await productService.DeleteAsync(Id);
            if(rs != null && rs.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();  
        }
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await productService.GetAsync(id);
            if (product != null && product.IsSuccess)
            {
                var productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(product.Result));
                return PartialView("_Edit", productDto);
            }
            return BadRequest();
        }
    }
}
