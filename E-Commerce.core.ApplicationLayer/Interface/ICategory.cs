using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.ApplicationLayer.Interface
{
    public interface ICategory
    {
        //List<CategoryModel> Get();
        public List<CategoryDTO> Get();
    }
}
