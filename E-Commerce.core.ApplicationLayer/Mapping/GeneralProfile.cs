using AutoMapper;
using E_Commerce.core.ApplicationLayer.DTOModel.Brand;
using E_Commerce.core.ApplicationLayer.DTOModel.Login;
using E_Commerce.core.DomainLayer.Entities;

namespace E_Commerce.core.ApplicationLayer.DTOModel.Helpers
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            CreateMap<CategoryModel,CategoryDTO>().ReverseMap();
            CreateMap<LoginModel, LoginDto>().ReverseMap();
            CreateMap<BrandModel, BrandDTO>().ReverseMap();
        }
    }
}
