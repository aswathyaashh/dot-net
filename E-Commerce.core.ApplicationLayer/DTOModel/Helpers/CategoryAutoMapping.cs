using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.ApplicationLayer.DTOModel.Login;
using E_Commerce.core.DomainLayer;

namespace E_Commerce.core.ApplicationLayer.DTOModel.Helpers
{
    public class CategoryAutoMapping:Profile
    {
        public CategoryAutoMapping()
        {
            CreateMap<CategoryModel,CategoryDTO>().ReverseMap();
            CreateMap<LoginModel, LoginDTO>().ReverseMap();
        }
    }
}
