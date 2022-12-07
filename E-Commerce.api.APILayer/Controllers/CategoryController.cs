using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.DomainLayer;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using E_Commerce.infrastructure.RepositoryLayer.services;
using E_Commerce.infrastructure.RepositoryLayer;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;
using E_Commerce.core.ApplicationLayer.DTOModel.Login;
using Swashbuckle.AspNetCore.Annotations;
//using E_Commerce.core.ApplicationLayer.DTOModel.Category;
using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.api.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Consumes("application/json", MediaTypeNames.Application.Xml)]
    [Produces("application/json", MediaTypeNames.Application.Xml)]
    public class CategoryController : ControllerBase
    {
       private readonly ICategory _category;
      
        public CategoryController(ICategory category)
        {
            _category = category;                       
        }

        #region(GEt)
        [HttpGet]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get all List", Description = "Get Category List")]
        public  ApiResponse<List<CategoryDTO>> Get()
        {                      
            return _category.Get();
        }
        #endregion

#region
        [HttpDelete]
        [AllowAnonymous]
        [Route("delete/{CategoryId}")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Delete Category", Description = "Delete specified category")]
        public ActionResult Delete(int CategoryId)
        {
            var temp = _category.Delete(CategoryId);
            return Ok(temp);
        }
#endregion

#region

        [HttpGet("CategoryName")]
        [AllowAnonymous]
        [Route("GetCategoryByName/{CategoryName}")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<CategoryDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Gets Category By Name", Description = "Checks if category exists")]
        public IActionResult GetCategoryByName(string Name)
        {
            var category = _category.GetByCategoryName(Name);
            return Ok(category);

        }
#endregion

#region

        [HttpPost("Add")]
        [Route("Add")]
        [AllowAnonymous]

        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<CategoryDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Posts new category", Description = "Adds a new Category")]
        public IActionResult AddCategory(CategoryDTO categoryDTO)
        {
            var response = _category.Post(categoryDTO);
            return Ok(response);
        }
#endregion

#region
        [HttpPut("{id}")]
        [Route("Edit/{CategoryId}")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<CategoryDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Edits new category", Description = "Edits a new Category")]
        public IActionResult EditCategory(int id, CategoryDTO categoryDTO)
        {

            var Updating = _category.Update(id, categoryDTO);
            return Ok(Updating);

        }
#endregion

    }
}
