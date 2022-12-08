using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.core.DomainLayer.Entities;
using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;

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
        #region(Get Category)
        /// <summary>  
        /// Gets all data  
        /// </summary>  
        /// <returns>collection of categories.</returns>  
        public ApiResponse<List<CategoryDTO>> Get()
        {
            ApiResponse<List<CategoryDTO>> response = new ApiResponse<List<CategoryDTO>>();
            var data = _mapper.Map<List<CategoryModel>, List<CategoryDTO>>(_adminDbContext.Category.Where(e => e.Status == 0).ToList());
            if (data != null && data.Count > 0)
            {
                response.Message = "Listed";
                response.Success = true;
                response.Data = data;
                return response;
            }
            else
            {
                response.Message = "Null";
                response.Success = false;
                return response;
            }

        }
        #endregion

        #region(get CategoryName By Name)
        /// <summary>  
        ///  Get Category by name  
        /// </summary>  
        /// <param Display category exist if it is category existing otherwise Category doesnt exists </param> 
        public ApiResponse<string> GetByCategoryName(string name)
        {
            var entity = _adminDbContext.Category.FirstOrDefault(e => e.CategoryName == name);
            ApiResponse<string> response = new ApiResponse<string>();

            if (entity != null)
            {
                if (entity.Status == 0)
                {
                    response.Success = true;
                    response.Message = "Category exists";
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Category doesnt exists";
                    return response;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Category doesnt exists";
                return response;
            }


        }
        #endregion

        #region(post)
        /// <summary>  
        ///  Edit Category by id  
        /// </summary>  
        /// <param add category name in database</param> 
        public ApiResponse<bool> Post([FromBody] CategoryDTO categoryDTO)
        {
            var categoryModel = new CategoryModel()
            {
                CategoryName = categoryDTO.CategoryName
            };

            categoryModel.UpdatedDate = null;
            _adminDbContext.Category.Add(categoryModel);
            _adminDbContext.SaveChanges();

            var add = _adminDbContext.Category.FirstOrDefault(e => e.CategoryName == categoryModel.CategoryName);
            ApiResponse<bool> addResponse = new ApiResponse<bool>();
            if (add == null)
            {
                addResponse.Success = false;
                addResponse.Message = "category is not added";
                return addResponse;
            }
            else
            {
                addResponse.Success = true;
                addResponse.Message = "Category is added";
                return addResponse;

            }

        }
        #endregion

        #region(Delete Category)
        /// <summary>  
        ///  Delete Category by id  
        /// </summary>  
        /// <param set status field in database as one when category deleted</param> 
        public ApiResponse<bool> Delete(int categoryId)
        {
            CategoryModel category = _adminDbContext.Category.FirstOrDefault(i => i.CategoryId == categoryId);
            ApiResponse<bool> response = new ApiResponse<bool>();
            if (category != null)
            {
                if (category.Status == 0)
                {
                    category.Status = 1;
                    category.UpdatedDate = DateTime.Now;
                    _adminDbContext.Category.Update(category);
                    _adminDbContext.SaveChanges();
                    response.Success = true;
                    response.Message = "Deleted";
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Already Deleted";
                    return response;
                }
            }

            response.Success = false;
            response.Message = "ID doesn't exist.";
            return response;
        }

        #endregion

        #region(Put)
        /// <summary>  
        ///  Edit Category by id  
        /// </summary>  
        /// <param edit category name in database</param> 
        public ApiResponse<bool> Update(int id, [FromBody] CategoryDTO categoryDTO)
        {
            var update = _adminDbContext.Category.FirstOrDefault(e => e.CategoryId == id);
            ApiResponse<bool> updateResponse = new ApiResponse<bool>();
            if (update == null)
            {
                updateResponse.Success = false;
                updateResponse.Message = "category doesnt exist";
                return updateResponse;
            }
            else
            {
                updateResponse.Success = true;
                updateResponse.Message = "category is updated";
                update.CategoryName = categoryDTO.CategoryName;
                update.UpdatedDate = DateTime.Now;
                _adminDbContext.Update(update);
                _adminDbContext.SaveChanges();
                return updateResponse;
            }

        }
        #endregion
    }
}
