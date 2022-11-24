using AutoMapper;
using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.ApplicationLayer.DTOModel.Login;
using E_Commerce.core.ApplicationLayer.Examples;
using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.DomainLayer;
using E_Commerce.infrastructure.RepositoryLayer;
using E_Commerce.infrastructure.RepositoryLayer.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;


namespace E_Commerce.api.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json",MediaTypeNames.Application.Xml)]
    [Produces("application/json", MediaTypeNames.Application.Xml)]
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
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
       // [ProducesResponseType(typeof(LoginResponseDTO), StatusCodes.Status400BadRequest)]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Authenticate : false")]
        [ProducesResponseType(typeof(LoginResponseDTO), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(DefaultApiConventions), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get 2 values", Description = "Get Email and Password")]
       
        public IActionResult loginCheck(string Emailid, string Password)
        {
         
            LoginDTO login = new LoginDTO();
            
            login.EmailId = Emailid;
            login.Password = Password;
            LoginResponseDTO temp = _login.loginCheck(login);

            return Ok(temp);
        }

    }
}
