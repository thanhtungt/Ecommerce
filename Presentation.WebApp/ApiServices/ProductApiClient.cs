using Presentation.Models;

namespace Presentation.WebApp.ApiServices
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration, httpContextAccessor)
        {
        }

        public async Task<PagedResultModel<IEnumerable<ProductModel>>> GetPagingProduct(int pageIndex, int pageSize)
        {
            var data = await GetAsync<PagedResultModel<IEnumerable<ProductModel>>>($"api/Product?pageIndex={pageIndex}&pageSize={pageSize}");
            return data;
        }
    }
}
