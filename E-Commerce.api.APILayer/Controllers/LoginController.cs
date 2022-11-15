using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.DomainLayer;
using E_Commerce.infrastructure.RepositoryLayer;
using E_Commerce.infrastructure.RepositoryLayer.services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.api.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _login;

        public LoginController(ILogin login)
        {
            _login = login;
           
        }
        [HttpPost("AdminLogin")]
        public async Task<IActionResult> loginCheck([FromBody] LoginModel login)
        {
            var temp = _login.loginCheck(login);
            if(temp == null)
            {
                return NotFound();
            }
            return Ok(temp);
        }

    }
}
