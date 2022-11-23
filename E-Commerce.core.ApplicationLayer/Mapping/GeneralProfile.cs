using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.ApplicationLayer.DTOModel.Login;
using E_Commerce.core.DomainLayer.Entities;

namespace E_Commerce.core.ApplicationLayer.DTOModel.Helpers
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            CreateMap<CategoryModel,CategoryDTO>().ReverseMap();
            CreateMap<LoginModel, LoginDTO>().ReverseMap();
        }
    }
}
