using Business.Models;
using Utilities.ServiceResult;

namespace Business.Domain.Catalog.ProductNP
{
    public interface IProductService
    {
        Task<ServiceResult<PagedResultEntity<IEnumerable<ProductResponseEntity>>>> GetProductPagingAsync(int page, int pageSize);
        Task<ServiceResult<IEnumerable<ProductResponseEntity>>> GetProductPagingByCategoryId(int categoryId, int page, int pageSize);
        Task<ServiceResult<IEnumerable<ProductResponseEntity>>> GetAllProductByCategoryId(int categoryId);
        Task<ServiceResult<IEnumerable<ProductResponseEntity>>> GetAllProductAsync();
        Task<ServiceResult<ProductResponseEntity>> GetProductByIdAsync(int id);

        Task<ServiceResult<IEnumerable<ProductResponseEntity>>> GetFeaturedProductAsync(int take = 6);

        Task<ServiceResult<int>> CreateProductAsync(CreateProductRequestEntity request);
        Task<ServiceResult<bool>> DeleteProductAsync(int id);
        Task<ServiceResult<bool>> UpdateProductAsync(UpdateProductRequestEntity request);

        Task<ServiceResult<IEnumerable<CategoryResponseEntity>>> GetCategoryAsync();
        Task<ServiceResult<IEnumerable<SubCategoryResponseEntity>>> GetSubCategoryAsync(int productId);

        Task<ServiceResult<bool>> CategoryAssignAsync(int productId, List<int> categoryIdList);

    }
}
