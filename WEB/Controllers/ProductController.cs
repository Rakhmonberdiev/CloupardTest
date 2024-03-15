﻿using Microsoft.AspNetCore.Mvc;
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
            var rs = await productService.GetAllAsync<APIResponse>(filter);
            if (rs != null && rs.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(rs.Result));
            }
            return View(products);
        }

        public  IActionResult ProductCreate()
        {
      
            return PartialView("ProductCreate");
        }
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var rs = await productService.CreateAsync<APIResponse>(model);
                if (rs != null && rs.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return PartialView("ProductCreate", model);
        }
    }
}
