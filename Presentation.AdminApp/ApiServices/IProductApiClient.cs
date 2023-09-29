using Presentation.Models;

namespace Presentation.AdminApp.ApiServices
{
    public interface IProductApiClient
    {
        Task<ProductModel> GetById(int id);
        Task<IEnumerable<SubCategoryModel>> GetSubCategories(int id);
        Task<bool> UpdateProduct(UpdateProductModel model);
        Task<bool> CreateProduct(CreateProductModel model);

    }
}
