using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.infrastructure.RepositoryLayer.services
{
    public class Category:ICategory
    {
        private readonly AdminDbContext _adminDbContext;
        public Category(AdminDbContext adminDbContext)
        {
            _adminDbContext = adminDbContext;
        }
        public List<CategoryModel> Get()
        {
            return _adminDbContext.Category.ToList();
        }
    }
}
