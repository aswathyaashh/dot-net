using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using E_Commerce.core.DomainLayer.Entities;
using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.ApplicationLayer.DTOModel.Brand;
using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;
using E_Commerce.core.ApplicationLayer.DTOModel;

namespace E_Commerce.infrastructure.RepositoryLayer.services
{
    public class Brand : IBrand
    {
        private readonly AdminDbContext _adminDbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public Brand(AdminDbContext adminDbContext, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _adminDbContext = adminDbContext;
            _mapper = mapper;
            this._hostEnvironment = hostEnvironment;
        }

        #region(Get Brand)
        /// <summary>  
        /// Gets all data  
        /// </summary>  
        /// <returns>collection of brands.</returns>  
        public ApiResponse<List<BrandDTO>> Get()
        {
            ApiResponse<List<BrandDTO>> response = new ApiResponse<List<BrandDTO>>();
            var value = _mapper.Map<List<BrandModel>, List<BrandDTO>>(_adminDbContext.Brand.Where(e => e.Status == 0).ToList());
            if (value != null && value.Count > 0)
            {
                response.Message = "Listed";
                response.Success = true;
                response.Data = value;
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

        #region(Post)
        /// <summary>  
        ///  Create brand  
        /// </summary>  
        /// <param Create brand name and logo path</param>
        public async Task<ApiResponse<bool>> Post(BrandDTO brandName)
        {
            var brand = _mapper.Map<BrandDTO, BrandModel>(brandName);
            ApiResponse<bool> addResponse = new ApiResponse<bool>();
            if (brand != null)
            {
                brand.LogoPath = await SaveLogo(brand.Logo);
                brand.CreatedDate = DateTime.Now;
                _adminDbContext.Brand.Add(brand);
                _adminDbContext.SaveChanges();
                addResponse.Success = true;
                addResponse.Message = "New Brand created";
                return addResponse;
            }
            else
            {
                addResponse.Success = false;
                addResponse.Message = "Not created";
                return addResponse;
            }

        }
        #endregion

        #region(Function for Uploading logo)
        /// <summary>  
        /// Function for upload brand logo  
        /// </summary>  
        /// <param Upload a brand logo with current datetime in logopath</param>
        public async Task<string> SaveLogo(IFormFile logo)
        {

            string logoPath = new String(Path.GetFileNameWithoutExtension(logo.FileName).Take(10).ToArray()).Replace(' ', '-');
            logoPath = logoPath + DateTime.Now.ToString("yyyyMMddhhmmfff") + Path.GetExtension(logo.FileName);
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "Images", logoPath);
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                await logo.CopyToAsync(filestream);
            }
            return logoPath;
        }
        #endregion

        #region(Get Brand by Name)
        /// <summary>  
        /// Get brand by name  
        /// </summary>  
        /// <param Display brand exist if it is in database otherwise doesnt exist</param>
        public ApiResponse<string> GetByBrandName(string name)
        {
            var brandName = _adminDbContext.Brand.FirstOrDefault(e => e.BrandName == name);
            ApiResponse<string> response = new ApiResponse<string>();
            if (brandName != null)
            {
                if (brandName.Status == 0)
                {
                    response.Success = true;
                    response.Message = "Brand exists";
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Brand doesnt exists";
                    return response;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Brand doesnt exists";
                return response;

            }

        }
        #endregion

        #region(Update)
        /// <summary>  
        ///  Update brand  
        /// </summary>  
        /// <param Edit brand name and logo path</param>
        public async Task<ApiResponse<bool>> Update(BrandDTO BrandId)
        {
            var updateData = _adminDbContext.Brand.FirstOrDefault(i => i.BrandId == BrandId.BrandId);
            ApiResponse<bool> updateResponse = new ApiResponse<bool>();
            if (updateData != null)
            {
                if (updateData.Status == 0)
                {
                    
                    if (BrandId.BrandName == null)
                    {
                        updateData.BrandName = updateData.BrandName;
                    }
                    else
                    {
                        updateData.BrandName = BrandId.BrandName;

                    }
                    if (BrandId.Logo==null)
                    {
                        updateData.LogoPath = updateData.LogoPath;
                    }
                    else
                    {
                        updateData.LogoPath = await SaveLogo(BrandId.Logo);

                    }
                    updateResponse.Success = true;
                    updateResponse.Message = "updated";
                    updateData.UpdatedDate = DateTime.Now;                    
                    _adminDbContext.Brand.Update(updateData);
                    _adminDbContext.SaveChanges();
                    return updateResponse;
                }
                else
                {
                    updateResponse.Success = false;
                    updateResponse.Message = " Not updated";
                    return updateResponse;
                }
            }
            else
            {
                updateResponse.Success = false;
                updateResponse.Message = "Null";
                return updateResponse;
            }
        }
        #endregion
    }
}
