using AutoMapper;
using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.ApplicationLayer.DTOModel.Brand;
using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;
using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.infrastructure.RepositoryLayer.services
{
    public class Brand:IBrand
    {
        private readonly AdminDbContext _adminDbContext;
        private readonly IMapper _mapper;
        public Brand(AdminDbContext adminDbContext, IMapper mapper)
        {
            _adminDbContext = adminDbContext;
            _mapper = mapper;
        }

#region
        /// <summary>  
        ///  Create brand  
        /// </summary>  
        /// <param Create brand name and logo path</param>
        public BrandDTO Post(BrandDTO Brand)  
        {           
            var user = _mapper.Map<BrandDTO, BrandModel>(Brand);
            user.CreatedDate = DateTime.Now;
            _adminDbContext.Brand.Add(user);
            _adminDbContext.SaveChanges();           
            var Brand1 = _mapper.Map<BrandModel,BrandDTO>(user);
            Brand1.Success=true;
            Brand1.Message = "created";           
            return Brand1;
        }
        #endregion

        #region
        /// <summary>  
        ///  Get Brand  
        /// </summary>  
        /// <param Get Brand by name</param>
        public ApiResponse GetByBrandName(string Name)
        {
            var entity = _adminDbContext.Brand.FirstOrDefault(e => e.BrandName == Name);
            ApiResponse check = new ApiResponse();
            if (entity == null)
            {
                check.Success = true;
                check.Message = "Brand doesnt exists";
                return check;
            }
            else
            {
                check.Success = false;
                check.Message = "Brand exists";
                return check;

            }

        }
        #endregion
    }
}
