using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.DomainLayer.Entities;
using NPOI.OpenXmlFormats.Dml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.infrastructure.RepositoryLayer.services
{
    public class Category : ICategory
    {
        private readonly AdminDbContext _adminDbContext;
        private readonly IMapper _mapper;
        public Category(AdminDbContext adminDbContext, IMapper mapper)
        {
            _adminDbContext = adminDbContext;
            _mapper = mapper;
           
        }

        public List<CategoryDTO> Get()
        {
            try{

                var data = _mapper.Map<List<CategoryModel>, List<CategoryDTO>>(_adminDbContext.Category.ToList());
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception("Error");
            }
        }
       

    }
}
