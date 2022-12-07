using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.ApplicationLayer.DTOModel.Brand;
//using E_Commerce.core.ApplicationLayer.DTOModel.Category;
using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;
using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.infrastructure.RepositoryLayer.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Annotations;

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

#region
        [HttpPost]
        [AllowAnonymous]
        [Route("post")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BrandDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Create brand", Description = "Add Brand name and brand logo path")]

        public async Task<ApiResponse<BrandDTO>> Post(string BrandName, IFormFile Logo)
        {
            var brand = new BrandDTO();
            brand.BrandName = BrandName;
            brand.Logo = Logo;
            return await _brand.Post(brand);
        }
        //public async Task<BrandDTO> Post(BrandAddRequestDTO brandDTO)
        //{
        //    BrandDTO brand = new BrandDTO();
        //    brand.BrandName = brandDTO.BrandName;
        //    brand.Logo = brandDTO.Logo;
        //    return await _brand.Post(brand);
        //}
        #endregion

        #region
        [HttpGet]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BrandDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get brand by name", Description = "success true if brand not exist")]
        public IActionResult GetByBrandId(string BrandName)
        {
            var brand = _brand.GetByBrandName(BrandName);
            return Ok(brand);
        }

        #endregion

        #region
        [HttpPut]
        [Route("Edit")]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BrandDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Update brand by Id", Description = "success true if brand not exist")]
        public IActionResult Update( string hi, IFormFile logo)
        {
            //BrandDTO updateDetails;
            var updateDetails = new BrandDTO();
             updateDetails.Logo=logo;
              var ResponseBrand = _brand.Update(updateDetails);
            return Ok(ResponseBrand);
        }
        #endregion

    }

}
