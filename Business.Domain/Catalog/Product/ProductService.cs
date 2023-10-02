using Business.Domain.Common;
using Business.Models;
using Data.BaseRepository;
using Data.Entity.Models;
using System.Drawing.Printing;
using Utilities.ServiceResult;

namespace Business.Domain.Catalog.ProductNP
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageService _storageService;

        public ProductService(IUnitOfWork unitOfWork, IStorageService storageService)
        {
            _unitOfWork = unitOfWork;
            _storageService = storageService;
        }

        public async Task<ServiceResult<int>> CreateProductAsync(CreateProductRequestEntity request)
        {
            try
            {
                var product = new Product()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Stock = request.Stock,
                    ProductImagePath = request.ProductImagePath,
                    DiscountPercent = request.DiscountPercent,
                    IsFeatured = request.IsFeatured,
                };
                await _unitOfWork.ProductRepository.AddAsync(product);
                await _unitOfWork.SaveChanageAsync();

                return new ServiceSuccessResult<int>(product.Id);
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<int>(ex.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<ProductResponseEntity>>> GetAllProductAsync()
        {
            try
            {
                var a = await _unitOfWork.ProductRepository.GetAllAsync();
                var b = a.Select(x => new ProductResponseEntity()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    DiscountPercent = x.DiscountPercent,
                    IsFeatured = x.IsFeatured,
                    ProductImagePath = x.ProductImagePath,
                    Stock = x.Stock,
                });
                return new ServiceSuccessResult<IEnumerable<ProductResponseEntity>>(b);
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<IEnumerable<ProductResponseEntity>>(ex.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<CategoryResponseEntity>>> GetCategoryAsync()
        {
            try
            {
                var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
                var parent = categories.Where(x => x.ParentId == 0);
                var children = categories.Where(x => x.ParentId != 0).Select(x => new CategoryResponseEntity()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId,
                });
                var result = parent.Select(x => new CategoryResponseEntity()
                {
                    Id=x.Id,
                    Name = x.Name,
                    SubCategory = children.Where(z=>z.ParentId == x.Id)
                });

                return new ServiceSuccessResult<IEnumerable<CategoryResponseEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<IEnumerable<CategoryResponseEntity>>(ex.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<ProductResponseEntity>>> GetFeaturedProductAsync(int take = 6)
        {
            try
            {
                var query = await _unitOfWork.ProductRepository.GetAllAsync();
                int totalRow = query.Count();
                var b = query.Where(x=>x.IsFeatured == true).OrderByDescending(x => x.CreateAt).Take(take)
                .Select(x => new ProductResponseEntity()
                {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        DiscountPercent = x.DiscountPercent,
                        IsFeatured = x.IsFeatured,
                        ProductImagePath = x.ProductImagePath,
                        Stock = x.Stock
                    });
                return new ServiceSuccessResult<IEnumerable<ProductResponseEntity>>(b);
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<IEnumerable<ProductResponseEntity>>(ex.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<ProductResponseEntity>>> GetProductPagingByCategoryId(int categoryId, int page, int pageSize)
        {
            try
            {
                var products = await _unitOfWork.ProductRepository.GetAllAsync();
                var productInCategories = await _unitOfWork.ProductInCategoryRepository.GetAllAsync();

                var query = from p in products
                            join pic in productInCategories on p.Id equals pic.ProductId
                            where pic.CategoryId == categoryId
                            select p;

                int totalRow = query.Count();

                var b = query.Skip((page - 1) * pageSize).Take(pageSize)
                    .Select(x => new ProductResponseEntity()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        DiscountPercent = x.DiscountPercent,
                        IsFeatured = x.IsFeatured,
                        ProductImagePath = x.ProductImagePath,
                        Stock = x.Stock,
                    });
                return new ServiceSuccessResult<IEnumerable<ProductResponseEntity>>(b);
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<IEnumerable<ProductResponseEntity>>(ex.Message);
            }
        }

        public async Task<ServiceResult<PagedResultEntity<IEnumerable<ProductResponseEntity>>>> GetProductPagingAsync(int page, int pageSize)
        {

            try
            {
                var query = await _unitOfWork.ProductRepository.GetAllAsync();

                int totalRow = query.Count();

                var products = query.Skip((page-1)*pageSize).Take(pageSize)
                    .Select(x => new ProductResponseEntity()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    DiscountPercent = x.DiscountPercent,
                    IsFeatured = x.IsFeatured,
                    ProductImagePath = x.ProductImagePath,
                    Stock= x.Stock,
                });

                var result = new PagedResultEntity<IEnumerable<ProductResponseEntity>>()
                {
                    PageIndex = page,
                    PageSize = pageSize,
                    TotalRecords = totalRow,
                    Items = products
                };
                return new ServiceSuccessResult<PagedResultEntity<IEnumerable<ProductResponseEntity>>>(result);
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<PagedResultEntity<IEnumerable<ProductResponseEntity>>>(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> UpdateProductAsync(UpdateProductRequestEntity request)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);
                if (product == null)
                {
                    return new ServiceErrorResult<bool>();
                }
                if(product.ProductImagePath != request.ProductImagePath)
                {
                    if (!string.IsNullOrEmpty(product.ProductImagePath))
                    {
                        await _storageService.DeleteFileAsync(product.ProductImagePath);
                    }
                    product.ProductImagePath = request.ProductImagePath;
                }

                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;
                product.Stock = request.Stock;
                product.DiscountPercent = request.DiscountPercent;
                product.IsFeatured = request.IsFeatured;

                await _unitOfWork.SaveChanageAsync();

                return new ServiceSuccessResult<bool>(true);
            }catch (Exception ex)
            {
                return new ServiceErrorResult<bool>(ex.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<ProductResponseEntity>>> GetAllProductByCategoryId(int categoryId)
        {
            try
            {
                var products = await _unitOfWork.ProductRepository.GetAllAsync();
                var productInCategories = await _unitOfWork.ProductInCategoryRepository.GetAllAsync();

                var query = from p in products
                            join pic in productInCategories on p.Id equals pic.ProductId
                            where pic.CategoryId == categoryId
                            select p;

                int totalRow = query.Count();
                var b = query.Select(x => new ProductResponseEntity()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        DiscountPercent = x.DiscountPercent,
                        IsFeatured = x.IsFeatured,
                        ProductImagePath = x.ProductImagePath,
                        Stock = x.Stock,
                    });
                return new ServiceSuccessResult<IEnumerable<ProductResponseEntity>>(b);
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<IEnumerable<ProductResponseEntity>>(ex.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<SubCategoryResponseEntity>>> GetSubCategoryAsync(int productId)
        {
            try
            {
                var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
                var parent = categories.Where(x => x.ParentId == 0);
                var children = categories.Where(x => x.ParentId != 0);
                var productInCategory = await _unitOfWork.ProductInCategoryRepository.GetManyAsync(x => x.ProductId == productId);

                var result = children.Select(x => new SubCategoryResponseEntity()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentName = parent.FirstOrDefault(a=> a.Id == x.ParentId).Name,
                    Selected = productInCategory.Any(z=>z.CategoryId == x.Id)
                });
                return new ServiceSuccessResult<IEnumerable<SubCategoryResponseEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<IEnumerable<SubCategoryResponseEntity>>(ex.Message);
            }
        }

        public async Task<ServiceResult<ProductResponseEntity>> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
                var result = new ProductResponseEntity()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    DiscountPercent = product.DiscountPercent,
                    IsFeatured = product.IsFeatured,
                    ProductImagePath = product.ProductImagePath,
                    Stock = product.Stock,
                };
                return new ServiceSuccessResult<ProductResponseEntity>(result);
            }
            catch(Exception ex)
            {
                return new ServiceErrorResult<ProductResponseEntity>(ex.Message);
            }



            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> CategoryAssignAsync(int productId, List<int> categoryIdList)
        {
            try
            {
                var categoryOfProduct = await _unitOfWork.ProductInCategoryRepository.GetManyAsync(x => x.ProductId == productId);

                _unitOfWork.ProductInCategoryRepository.RemoveRange(categoryOfProduct);

                await _unitOfWork.SaveChanageAsync();

                if(categoryIdList != null)
                {
                    var addList = new List<ProductInCategory>();

                    foreach (var cateId in categoryIdList)
                    {
                        var a = new ProductInCategory()
                        {
                            ProductId = productId,
                            CategoryId = cateId
                        };
                        addList.Add(a);
                    }

                    await _unitOfWork.ProductInCategoryRepository.AddRangeAsync(addList);
                    await _unitOfWork.SaveChanageAsync();
                }
                return new ServiceSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<bool>(ex.Message);
            }



            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
                if(product.ProductImagePath != null)
                {
                    await _storageService.DeleteFileAsync(product.ProductImagePath);
                }
                _unitOfWork.ProductRepository.Remove(product);
                await _unitOfWork.SaveChanageAsync(); 
                return new ServiceSuccessResult<bool>();
            }
            catch(Exception ex)
            {
                return new ServiceErrorResult<bool>(ex.Message);
            }
        }
    }
}
