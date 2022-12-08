﻿using AutoMapper;
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
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<ApiResponse<bool>> Update(BrandDTO BrandId)
        {
            BrandModel updateData = _adminDbContext.Brand.FirstOrDefault(i => i.BrandId == BrandId.BrandId);
            ApiResponse<bool> updateResponse = new ApiResponse<bool>();
            if (updateData != null)
            {
                if (updateData.Status == 0)
                {

                    updateData.BrandName = BrandId.BrandName;
                    updateData.LogoPath = await SaveLogo(updateData.Logo);
                   // updateData.LogoPath = BrandId.LogoPath;
                    updateResponse.Success = true;
                    updateResponse.Message = "updated";                    
                    updateData.LogoPath = await SaveLogo(BrandId.Logo);
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
