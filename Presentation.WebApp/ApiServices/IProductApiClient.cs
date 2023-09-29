using Presentation.Models;

namespace Presentation.WebApp.ApiServices
{
    public interface IProductApiClient
    {
        Task<PagedResultModel<IEnumerable<ProductModel>>> GetPagingProduct(int pageIndex, int pageSize);
    }
}
