using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.ApplicationLayer.UIModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.api.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
       private readonly ICategory _category;
        public CategoryController(ICategory category)
        {
            _category = category;
        }
        [HttpGet]
        public ActionResult<List<CategoryModel>> Get()
        {
            return _category.Get();
        }
    }
}
