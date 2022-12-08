using E_Commerce.core.ApplicationLayer.DTOModel.Brand;
using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;


namespace E_Commerce.core.ApplicationLayer.Interface
{
    public interface IBrand
    {
        
        public  Task<ApiResponse<bool>> Post(BrandDTO brandName);
        public ApiResponse<string> GetByBrandName(string name);
        public Task<ApiResponse<bool>> Update(BrandDTO brandId);
    }
}
