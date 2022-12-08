using AutoMapper;
using E_Commerce.core.DomainLayer.Entities;
using E_Commerce.core.ApplicationLayer.DTOModel.Brand;
using E_Commerce.core.ApplicationLayer.DTOModel.Login;


namespace E_Commerce.core.ApplicationLayer.DTOModel.Helpers
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            CreateMap<LoginModel, LoginDTO>().ReverseMap();

            CreateMap<BrandModel, BrandDTO>().ReverseMap();

            CreateMap<CategoryModel, CategoryDTO>().ReverseMap();    
            
        }
    }
}
