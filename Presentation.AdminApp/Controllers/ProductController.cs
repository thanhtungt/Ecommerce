using Microsoft.AspNetCore.Mvc;
using Presentation.AdminApp.ApiServices;
using Presentation.AdminApp.Models;
using Presentation.Models;
using Utilities.Constants;

namespace Presentation.AdminApp.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        public ProductController(IProductApiClient productApiClient, IConfiguration configuration)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
        }

        [HttpGet("Product")]
        public IActionResult Index()
        {
            var sessions = HttpContext.Session.GetString("Token");
            ViewBag.Token = sessions;
            ViewBag.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            return View();
        }
        [HttpGet("Product/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productApiClient.GetById(id);
            var category = await _productApiClient.GetSubCategories(id);
            ViewBag.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);

            var model = new EditProductViewModel()
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DiscountPercent = product.DiscountPercent,
                IsFeatured = product.IsFeatured,
                ProductImagePath = product.ProductImagePath,
                SubCategories = category,
                Stock = product.Stock,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            var updateModel = new UpdateProductModel()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                DiscountPercent = model.DiscountPercent,
                IsFeatured = model.IsFeatured,
                ProductImagePath = model.ProductImagePath,
                Image = model.Image,
                SelectedSubCategoryIds = model.SelectedSubCategoryIds,
                Stock = model.Stock,
            };


            var a = await _productApiClient.UpdateProduct(updateModel);


            return RedirectToAction("Index");
        }

        [HttpGet("Product/create")]
        public async Task<IActionResult> Create()
        {
            var category = await _productApiClient.GetSubCategories(0);
            /*var model = new CreateProductViewModel()
            {
                SubCategories = category
            };*/
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (ModelState.ContainsKey("IsFeatured"))
            {
                ModelState.Remove("IsFeatured"); // Remove it from ModelState
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var createModel = new CreateProductModel()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                DiscountPercent = model.DiscountPercent,
                Image = model.Image,
                IsFeatured = model.IsFeatured,
                SelectedSubCategoryIds = model.SelectedSubCategoryIds,
            };

            await _productApiClient.CreateProduct(createModel);

            return RedirectToAction("Index");
        }

    }
}
