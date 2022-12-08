using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.ApplicationLayer.DTOModel.Brand;
using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.api.APILayer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BrandController : ControllerBase
    {
        
        private readonly IBrand _brand;       
        public BrandController(IBrand brand)
        {
            _brand = brand;
           
        }

        #region(Post)
        /// <summary>  
        ///  API for Adding Brand   
        /// </summary>  
        /// <param API to add brand name in database</param> 
        [HttpPost]
        [AllowAnonymous]
        [Route("post")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BrandDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Create brand", Description = "Add Brand name and brand logo path")]

        public async Task<ApiResponse<bool>> Post([FromForm] string brandName, IFormFile logo)
        {
            var brand = new BrandDTO();
            brand.BrandName = brandName;
            brand.Logo = logo;
            return await _brand.Post(brand);
        }

        //USING OBJECT
        //public async Task<ApiResponse<bool>> Post(BrandDTO brandDTO)
        //{
        //    BrandDTO brand = new BrandDTO();
        //    brand.BrandName = brandDTO.BrandName;
        //    brand.Logo = brandDTO.Logo;
        //    return await _brand.Post(brandDTO);
        //}
        #endregion

        #region(Brand by name)
        /// <summary>  
        /// API to get brand by name  
        /// </summary>  
        /// <param API to display existing brand name</param>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BrandDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get brand by name", Description = "success true if brand not exist")]
        public ApiResponse<string> GetByBrandName(string brandName)
        {
            return _brand.GetByBrandName(brandName);
        }

        #endregion

        #region(put)
        /// <summary>  
        ///  API for Update brand  
        /// </summary>  
        /// <param API to Edit brand name and logo path in database</param>
        [HttpPut]
        [Route("Edit")]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BrandDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Update brand by Id", Description = "success true if brand not exist")]
        public Task<ApiResponse<bool>> Update(int id,string brandName, IFormFile logo)
        {
            var updateDetails = new BrandDTO();
            updateDetails.BrandName = brandName;
            updateDetails.Logo = logo;
            updateDetails.BrandId = id;
            return _brand.Update(updateDetails);
        }
        #endregion

    }

}
