using E_Commerce.core.ApplicationLayer.DTOModel;
//using E_Commerce.core.ApplicationLayer.DTOModel.Category;
using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;
using E_Commerce.core.DomainLayer;
using E_Commerce.core.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.ApplicationLayer.Interface
{
    public interface ICategory
    {
      public ApiResponse<List<CategoryDTO>> Get();
      public ApiResponse<bool> Delete(int CategoryId);
      public ApiResponse<bool> GetByCategoryName(string Name);
      public ApiResponse<bool> Post(CategoryDTO categoryDTO);
      public ApiResponse<bool> Update(int id, CategoryDTO categoryDTO);

    }
}
