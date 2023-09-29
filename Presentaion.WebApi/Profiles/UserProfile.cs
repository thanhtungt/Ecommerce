using AutoMapper;
using Business.Models;
using Presentation.Models;

namespace Presentation.WebApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterModel, RegisterRequestEntity>();
            CreateMap<LoginModel, LoginRequestEntity>();
        }
    }
}
