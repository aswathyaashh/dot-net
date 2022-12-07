using AutoMapper;
using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.ApplicationLayer.DTOModel.Brand;
using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;
using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.DomainLayer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.infrastructure.RepositoryLayer.services
{
    public class Brand:IBrand
    {
        private readonly AdminDbContext _adminDbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        ApiResponse<BrandDTO> check = new ApiResponse<BrandDTO>();
        public Brand(AdminDbContext adminDbContext, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _adminDbContext = adminDbContext;
            _mapper = mapper;
            this._hostEnvironment = hostEnvironment;
        }

#region
        /// <summary>  
        ///  Create brand  
        /// </summary>  
        /// <param Create brand name and logo path</param>
        public  async Task<ApiResponse<BrandDTO>> Post(BrandDTO Brand)  
        {           
            var brand = _mapper.Map<BrandDTO, BrandModel>(Brand);
            brand.LogoPath =await SaveLogo(brand.Logo);
            brand.CreatedDate = DateTime.Now;
            _adminDbContext.Brand.Add(brand);
            _adminDbContext.SaveChanges();           
            var Brand1 = _mapper.Map<BrandModel,BrandDTO>(brand);
            //Brand1.Success=true;
            //Brand1.Message = "created";           
            //return Brand1;
            check.Success = true;
            check.Message = "created";
            return check;
        }
#endregion
#region
        /// <summary>  
        /// Function for upload brand logo  
        /// </summary>  
        /// <param Upload a brand logo with current datetime in logopath</param>
        public async Task<string> SaveLogo(IFormFile logo)
        {
           
            string logopath = new String(Path.GetFileNameWithoutExtension(logo.FileName).Take(10).ToArray()).Replace(' ', '-');
            logopath = logopath + DateTime.Now.ToString("yyyyMMmmfff") + Path.GetExtension(logo.FileName);
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "Images", logopath);
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                await logo.CopyToAsync(filestream);
            }
            return logopath;
        }
#endregion

#region
        /// <summary>  
        ///  Get Brand  
        /// </summary>  
        /// <param Get Brand by name</param>
        public ApiResponse<BrandDTO> GetByBrandName(string BrandName)
        {
            var brandname = _adminDbContext.Brand.FirstOrDefault(e => e.BrandName == BrandName);
            //ApiResponse check = new ApiResponse();
            if (brandname != null)
            {
                if (brandname.Status == 0)
                {
                    check.Success = true;
                    check.Message = "Brand exists";
                    return check;
                }
                else
                {
                    check.Success = false;
                    check.Message = "Brand doesnt exists";
                    return check;
                }
            }
            else
            {
                check.Success = false;
                check.Message = "Brand doesnt exists";
                return check;

            }

        }
        #endregion



        #region

        public async Task<ApiResponse<BrandDTO>> Update(BrandDTO BrandId)
        {
            BrandModel brand = _adminDbContext.Brand.FirstOrDefault(i => i.BrandId == BrandId.BrandId);
            //ApiResponse check = new ApiResponse();
            if (brand != null)
            {
                if (brand.Status == 0)
                {

                    brand.BrandName = BrandId.BrandName;
                    brand.LogoPath = BrandId.LogoPath;
                    check.Success = true;
                    check.Message = "updated";
                    brand.LogoPath = await SaveLogo(BrandId.Logo);
                    _adminDbContext.Brand.Update(brand);
                    _adminDbContext.SaveChanges();
                    return check;

                }
                else
                {
                    check.Success = false;
                    check.Message = " Not updated";
                    return check;
                }
            }
            else
            {
                check.Success = false;
                check.Message = "Null";
                return check;
            }
        }

        #endregion
    }
}
