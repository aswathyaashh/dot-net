using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;
using E_Commerce.core.ApplicationLayer.DTOModel.Login;
using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.DomainLayer.Entities;
using NPOI.OpenXmlFormats.Dml.Diagram;
using NPOI.SS.Formula.Functions;
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

#region
        /// <summary>  
        /// Gets all data  
        /// </summary>  
        /// <returns>collection of categories.</returns>  
        public List<CategoryDTO> Get()
        {
            try
            {
                var data = _mapper.Map<List<CategoryModel>, List<CategoryDTO>>(_adminDbContext.Category.Where(e => e.Status == 0).ToList());
                return data;               
            }
            catch(Exception ex)
            {
                throw new Exception("Error");
            }
        }
#endregion

#region
        /// <summary>  
        ///  Delete Category by id  
        /// </summary>  
        /// <param set status field as 1 </param> 
        public CategoryDTO Delete(int CategoryId)
        {
            CategoryModel Category = _adminDbContext.Category.FirstOrDefault(i => i.CategoryId == CategoryId);
            
                if (Category != null)
                {
                    if (Category.Status == 0)
                    {
                        Category.Status = 1;
                        Category.UpdatedDate = DateTime.Now;
                        _adminDbContext.Category.Update(Category);
                        _adminDbContext.SaveChanges();
                        return new CategoryDTO()
                        {
                            CategoryId = Category.CategoryId,
                            Success = true,
                            Message = "Deleted",
                            CategoryName = Category.CategoryName
                        };
                    }
                    else
                    {
                        return new CategoryDTO()
                        {
                            Success = false,
                            Message = "Already Deleted",
                            CategoryName = null
                        };
                    }
            }
                return new CategoryDTO()
                {
                    Success = false,
                    Message = "ID doesn't exist.",
                    CategoryName = null
                };                    
        }
#endregion

    }
}
