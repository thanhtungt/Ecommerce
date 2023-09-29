using AutoMapper;
using Business.Models;
using Presentation.Models;

namespace Presentation.WebApi.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<ProductInCartResponseEntity, ProductInCartModel>();
        }
    }
}
