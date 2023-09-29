using AutoMapper;
using Business.Domain.Catalog.ProductNP;
using Business.Domain.Common;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Net.Http.Headers;

namespace Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;

        public ProductController(IProductService productService, IMapper mapper, IStorageService storageService)
        {
            _productService = productService;
            _mapper = mapper;
            _storageService = storageService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductModel model)
        {
            var createProductEntity = _mapper.Map<CreateProductModel, CreateProductRequestEntity>(model);

            if (model.Image != null)
            {
                var fileName = await this.SaveFile(model.Image, "product-image");
                createProductEntity.ProductImagePath = fileName;
            }

            var result = await _productService.CreateProductAsync(createProductEntity);
            if(model.SelectedSubCategoryIds != null)
            {
                await _productService.CategoryAssignAsync(result.Result, model.SelectedSubCategoryIds);
            }
            if (result.IsSuccessed) { return StatusCode(StatusCodes.Status201Created, result.Result); }
            return BadRequest(result.Message);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteProduct([FromForm]int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (result.IsSuccessed)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductModel model)
        {
            var product = _mapper.Map<UpdateProductModel, UpdateProductRequestEntity>(model);

            if(model.Image != null)
            {
                var fileName = await this.SaveFile(model.Image, "product-image");
                product.ProductImagePath = fileName;
            }
            await _productService.UpdateProductAsync(product);
            await _productService.CategoryAssignAsync(model.Id ,model.SelectedSubCategoryIds);

            return StatusCode(StatusCodes.Status202Accepted);
        }

        private async Task<string> SaveFile(IFormFile file, string folderName)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), folderName, fileName);
            var da = Path.Combine(folderName, fileName);
            return da;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _productService.GetAllProductAsync();
            if (result.IsSuccessed) { return Ok(_mapper.Map<IEnumerable<ProductResponseEntity>, IEnumerable<ProductModel>>(result.Result)); }
            return BadRequest(result.Message);
        }

        [HttpGet()]
        public async Task<IActionResult> GetProductPaging([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 8)
        {
            var result = await _productService.GetProductPagingAsync(pageIndex, pageSize);
            
            
            if (result.IsSuccessed) {
                var product = _mapper.Map<IEnumerable<ProductResponseEntity>, IEnumerable<ProductModel>>(result.Result.Items);
                var res = new PagedResultModel<IEnumerable<ProductModel>>()
                {
                    PageIndex = result.Result.PageIndex,
                    PageSize = result.Result.PageSize,
                    TotalRecords = result.Result.TotalRecords,
                    Items = product
                };

                return Ok(res); 
            
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            if (result.IsSuccessed)
            {
                return Ok(_mapper.Map<ProductResponseEntity, ProductModel>(result.Result));
            }
            return BadRequest(result.Message);  
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedProduct(int take = 6)
        {
            var result = await _productService.GetFeaturedProductAsync(take);
            if (result.IsSuccessed) { return Ok(_mapper.Map<IEnumerable<ProductResponseEntity>, IEnumerable<ProductModel>>(result.Result)); }
            return BadRequest(result.Message);
        }

        [HttpGet("category")]
        public async Task<IActionResult> GetCategory()
        {
            var result = await _productService.GetCategoryAsync();
            if (result.IsSuccessed) { return Ok(_mapper.Map<IEnumerable<CategoryResponseEntity>, IEnumerable<CategoryModel>>(result.Result)); }
            return BadRequest(result.Message);
        }

        [HttpGet("subcategory/{productId}")]
        public async Task<IActionResult> GetSubCategory(int productId)
        {
            var result = await _productService.GetSubCategoryAsync(productId);
            if (result.IsSuccessed) { return Ok(_mapper.Map<IEnumerable<SubCategoryResponseEntity>, IEnumerable<SubCategoryModel>>(result.Result)); }
            return BadRequest(result.Message);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductPagingByCategoryId([FromRoute] int categoryId, [FromQuery] int page = 1, [FromQuery] int pageSize = 12)
        {
            var result = await _productService.GetProductPagingByCategoryId(categoryId,page ,pageSize);
            if (result.IsSuccessed) { return Ok(_mapper.Map<IEnumerable<ProductResponseEntity>, IEnumerable<ProductModel>>(result.Result)); }
            return BadRequest(result.Message);
        }


        [HttpGet("category/all/{categoryId}")]
        public async Task<IActionResult> GetAllProductCategoryId([FromRoute] int categoryId)
        {
            var result = await _productService.GetAllProductByCategoryId(categoryId);
            if (result.IsSuccessed) { return Ok(_mapper.Map<IEnumerable<ProductResponseEntity>, IEnumerable<ProductModel>>(result.Result)); }
            return BadRequest(result.Message);
        }
    }
}
