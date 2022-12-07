using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.ApplicationLayer.DTOModel.Brand;
using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;
using E_Commerce.core.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.ApplicationLayer.Interface
{
    public interface IBrand
    {
        public Task<ApiResponse<BrandDTO>> Post(BrandDTO BrandName);
        public ApiResponse<BrandDTO> GetByBrandName(string BrandName);
        public Task<ApiResponse<BrandDTO>> Update(BrandDTO BrandId);
    }
}
