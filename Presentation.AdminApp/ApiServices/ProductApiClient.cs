using Microsoft.AspNetCore.Http;
using Presentation.Models;
using System.Net.Http.Headers;
using Utilities.Constants;

namespace Presentation.AdminApp.ApiServices
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration, httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateProduct(CreateProductModel model)
        {
            var sessions = _httpContextAccessor
               .HttpContext
               .Session
               .GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var formData = new MultipartFormDataContent();

            if (model.Image != null)
            {
                using (var imageStream = new MemoryStream())
                {
                    await model.Image.CopyToAsync(imageStream);
                    var imageContent = new ByteArrayContent(imageStream.ToArray());
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg"); // Adjust content type as needed
                    formData.Add(imageContent, "Image", model.Image.FileName);
                }
            }

            formData.Add(new StringContent(model.Name), "Name");
            formData.Add(new StringContent(model.Description), "Description");
            formData.Add(new StringContent(model.Price.ToString()), "Price");
            formData.Add(new StringContent(model.DiscountPercent.ToString()), "DiscountPercent");
            formData.Add(new StringContent(model.IsFeatured.ToString()), "IsFeatured");
            formData.Add(new StringContent(model.Stock.ToString() ?? ""), "Stock");

            if (model.SelectedSubCategoryIds != null)
            {
                foreach (var subCategoryId in model.SelectedSubCategoryIds)
                {
                    formData.Add(new StringContent(subCategoryId.ToString()), "SelectedSubCategoryIds");
                }
            }

            var response = await client.PostAsync($"/api/Product", formData);


            return response.IsSuccessStatusCode;

            throw new NotImplementedException();
        }

        public async Task<ProductModel> GetById(int id)
        {
            var data = await GetAsync<ProductModel>($"api/Product/{id}");
            return data;
        }

        public async Task<IEnumerable<SubCategoryModel>> GetSubCategories(int id)
        {
            var data = await GetListAsync<SubCategoryModel>($"api/Product/subcategory/{id}");

            return data;
        }

        public async Task<bool> UpdateProduct(UpdateProductModel model)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);


            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var formData = new MultipartFormDataContent();

            if (model.Image != null)
            {
                using (var imageStream = new MemoryStream())
                {
                    await model.Image.CopyToAsync(imageStream);
                    var imageContent = new ByteArrayContent(imageStream.ToArray());
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg"); // Adjust content type as needed
                    formData.Add(imageContent, "Image", model.Image.FileName);
                }
            }

            formData.Add(new StringContent(model.Id.ToString()), "Id");
            formData.Add(new StringContent(model.Name), "Name");
            formData.Add(new StringContent(model.Description), "Description");
            formData.Add(new StringContent(model.Price.ToString()), "Price");
            formData.Add(new StringContent(model.DiscountPercent.ToString()), "DiscountPercent");
            formData.Add(new StringContent(model.IsFeatured.ToString()), "IsFeatured");
            formData.Add(new StringContent(model.ProductImagePath ?? ""), "ProductImagePath");
            formData.Add(new StringContent(model.Stock.ToString() ?? ""), "Stock");

            if (model.SelectedSubCategoryIds != null)
            {
                foreach (var subCategoryId in model.SelectedSubCategoryIds)
                {
                    formData.Add(new StringContent(subCategoryId.ToString()), "SelectedSubCategoryIds");
                }
            }

            var response = await client.PutAsync($"/api/Product", formData);


            return response.IsSuccessStatusCode;
        }
    }
}
