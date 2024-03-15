
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
            var rs = await productService.GetAllAsync<APIResponse>(filter);
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
                var rs = await productService.CreateAsync<APIResponse>(model);
                if (rs != null && rs.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return PartialView("ProductCreate", model);
        }
        public async Task<IActionResult> Edit(Guid Id)
        {
            var rs = await productService.GetAsync<APIResponse>(Id);
            if(rs != null && rs.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(rs.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var rs = await productService.UpdateAsync<APIResponse>(model);
                if(rs != null && rs.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return PartialView("_Edit", model);
        }
        
    }
}
