using AutoMapper;
using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.ApplicationLayer.DTOModel.Login;
using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.DomainLayer;
using E_Commerce.infrastructure.RepositoryLayer;
using E_Commerce.infrastructure.RepositoryLayer.services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.api.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _login;
        private readonly IMapper _mapper;

        public LoginController(ILogin login, IMapper mapper)
        {
            _login = login;
            _mapper = mapper;

        }
        [HttpPost("AdminLogin")]
        [ProducesResponseType(typeof(List<LoginResponseDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get 2 values", Description = "Get Email and Password")]
        public IActionResult loginCheck([FromBody] LoginDTO login)
        {
            LoginResponseDTO temp = _login.loginCheck(login);
            
            return Ok(temp);
        }

    }
}
