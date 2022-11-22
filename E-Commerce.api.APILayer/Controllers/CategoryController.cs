using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.DomainLayer;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using E_Commerce.infrastructure.RepositoryLayer.services;
using E_Commerce.infrastructure.RepositoryLayer;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.api.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
       private readonly ICategory _category;
      
        public CategoryController(ICategory category)
        {
            _category = category;
            
           
        }
        [HttpGet]
        public ActionResult<List<CategoryDTO>> Get()
        {
                      
            return _category.Get();
        }
    }
}
