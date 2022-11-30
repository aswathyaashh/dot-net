using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.ApplicationLayer.DTOModel.Brand;
using E_Commerce.core.ApplicationLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.api.APILayer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BrandController : ControllerBase
    {
        // GET: api/<BrandController>

        // POST api/<BrandController>
        private readonly IBrand _brand;

        public BrandController(IBrand brand)
        {
            _brand = brand;
        }

#region
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BrandDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Create brand", Description = "Add Brand name and brand logo path")]
               
        public BrandDTO Post(BrandRequestDTO Brand)
        {
            var brand = new BrandDTO();
            brand.BrandName = Brand.BrandName;
            brand.LogoPath = Brand.LogoPath;
            return _brand.Post(brand);
        }
        #endregion

        #region
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BrandDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get brand by name", Description = "success true if brand not exist")]

        public IActionResult GetCategoryByName(string Name)
        {
            var category = _brand.GetByBrandName(Name);
            return Ok(category);
        }

        #endregion


    }
}
