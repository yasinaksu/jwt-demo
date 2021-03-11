using JwtDemo.AuthServer.Core.Dtos;
using JwtDemo.AuthServer.Core.Models;
using JwtDemo.AuthServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtDemo.AuthServer.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IGenericService<Product, ProductDto> _productService;
        public ProductsController(IGenericService<Product, ProductDto> productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productService.GetAllAsync();
            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductDto productDto)
        {
            var result = await _productService.AddAsync(productDto);
            return ActionResultInstance(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            var result = await _productService.UpdateAsync(productDto, productDto.Id);
            return ActionResultInstance(response: result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.RemoveAsync(id);
            return ActionResultInstance(response: result);
        }
    }
}
