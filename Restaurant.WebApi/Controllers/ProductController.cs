using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApi.Dtos.ProductDto;
using Restaurant.WebApi.Models.Context;
using Restaurant.WebApi.Models.Entities;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IValidator<Product> _validator;

        public ProductController(IValidator<Product> validator, ApiContext apiContext)
        {
            _validator = validator;
            _apiContext = apiContext;
        }

        private readonly ApiContext _apiContext;


        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _apiContext.Products.Include(p => p.Category).Select(p => new
            {
                p.ProductId,
                p.ProductName,
                p.ProductDescription,
                p.Price,
                p.ImageUrl,
                p.Category.CategoryName

            }).ToListAsync();
            return Ok(products);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetProductById(int id)
        {
           var product= await _apiContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);
           return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var product = new Product
            {
                ProductName=createProductDto.ProductName,
                ProductDescription=createProductDto.ProductDescription,
                Price=createProductDto.Price,
                ImageUrl=createProductDto.ImageUrl,
                CategoryId=createProductDto.CategoryId
                
            };

            var validationresult = _validator.Validate(product);

            if (!validationresult.IsValid)
            {
               return BadRequest(validationresult.Errors.Select(x=>x.ErrorMessage));
            }
          
               await _apiContext.Products.AddAsync(product);
               await _apiContext.SaveChangesAsync();
               return Ok("Melumat elave olundu");
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,UpdateProductDto updateProductDto)
        {
           var product=await _apiContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            product.ProductName = updateProductDto.ProductName;
            product.ProductDescription = updateProductDto.ProductDescription;
            product.Price = updateProductDto.Price;
            product.ImageUrl = updateProductDto.ImageUrl;
            product.CategoryId = updateProductDto.CategoryId;

            var validationresult = _validator.Validate(product);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => x.ErrorMessage));
            }

            await _apiContext.SaveChangesAsync();
            return Ok("Melumat deyisdirildi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _apiContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            _apiContext.Products.Remove(product);
            await _apiContext.SaveChangesAsync();
            return Ok("Melumat silindi");
        }
    }
}
