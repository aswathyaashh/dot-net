using E_Commerce.core.ApplicationLayer.DTOModel;
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
      public List<CategoryDTO> Get();
      public CategoryDTO Delete(int CategoryId);
        
    }
}
