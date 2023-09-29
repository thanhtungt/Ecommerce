using AutoMapper;
using Business.Models;
using Presentation.Models;

namespace Presentation.WebApi.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductModel, CreateProductRequestEntity>();
            CreateMap<ProductModel, ProductResponseEntity>();
            CreateMap<ProductResponseEntity, ProductModel>();
            CreateMap<CategoryResponseEntity, CategoryModel>();
            CreateMap<SubCategoryResponseEntity, SubCategoryModel>();
            CreateMap<UpdateProductModel, UpdateProductRequestEntity>();
        }
    }
}
