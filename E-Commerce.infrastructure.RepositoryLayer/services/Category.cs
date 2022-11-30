using AutoMapper;
using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.ApplicationLayer.DTOModel.Category;
using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;
using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;

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
           var data = _mapper.Map<List<CategoryModel>, List<CategoryDTO>>(_adminDbContext.Category.Where(e => e.Status == 0).ToList());
           return data;                         
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
        public ApiResponse GetByCategoryName(string Name)
        {           
            var entity = _adminDbContext.Category.FirstOrDefault(e => e.CategoryName == Name);
            ApiResponse check = new ApiResponse();
            if (entity == null)
            {
                check.Success = true;
                check.Message = "Category doesnt exists";
                return check;
            }
            else
            {
                check.Success = false;
                check.Message = "Category exists";
                return check;

            }
        }
        #region(post)
        public ApiResponse Post([FromBody] CategoryAddDTO categoryDTO)
        {
            var categoryModel = new CategoryModel()
            {
                CategoryName = categoryDTO.CategoryName
            };
            categoryModel.UpdatedDate = null;
            _adminDbContext.Category.Add(categoryModel);
            _adminDbContext.SaveChanges();

            var entity = _adminDbContext.Category.FirstOrDefault(e => e.CategoryName == categoryModel.CategoryName);
            ApiResponse check = new ApiResponse();
            if (entity == null)
            {
                check.Success = false;
                check.Message = "categoryname is not added";
                return check;
            }
            else
            {
                check.Success = true;
                check.Message = "Category is added";
                return check;

            }

        }
        #endregion
        #region(Update)
        public ApiResponse Update(int id, [FromBody] CategoryAddDTO categoryDTO)
        {

            var entity = _adminDbContext.Category.FirstOrDefault(e => e.CategoryId == id);
            ApiResponse check = new ApiResponse();
            if (entity == null)
            {
                check.Success = false;
                check.Message = "The category doesnt exist";
                return check;
            }
            else
            {
                check.Success = true;
                check.Message = "The category is updated";
                entity.CategoryName = categoryDTO.CategoryName;
                entity.UpdatedDate = DateTime.Now;
                _adminDbContext.Update(entity);
                _adminDbContext.SaveChanges();
                return check;
            }

        }
        #endregion
    }
}
