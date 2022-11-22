using AutoMapper;
using E_Commerce.core.ApplicationLayer.DTOModel;
using E_Commerce.core.ApplicationLayer.DTOModel.Login;
using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.DomainLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.infrastructure.RepositoryLayer.services
{
    public class Login: ILogin
    {
        private readonly AdminDbContext _admincontext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public Login(AdminDbContext adminDbContext,IConfiguration iconfiguration,IMapper mapper)
        {
            _admincontext = adminDbContext;
            _configuration = iconfiguration;
            _mapper = mapper;
        }
        public LoginResponseDTO loginCheck(LoginDTO login)
        {

            LoginDTO? loginModel = _mapper.Map<LoginModel, LoginDTO>(_admincontext.Login.FirstOrDefault(i => i.EmailId == login.EmailId));
            var temp = _mapper.Map<LoginModel, LoginDTO>(_admincontext.Login.FirstOrDefault(i => i.EmailId == login.EmailId));
            var temp1 = _mapper.Map<LoginDTO, LoginModel>(temp);
            temp1.modifiedDate= DateTime.Now;
            if ((loginModel.EmailId== login.EmailId) && (loginModel.password == login.password))
            {
                return new LoginResponseDTO()
                {
                    Authenticate = true,
                    Message = "Success",
                    ExpiryDate = DateTime.Now.AddDays(1),
                    Token= CreateToken(login)
                   
                };
              
               // return "Username not found";
            }
            else 
            {

                return new LoginResponseDTO()
                {
                    Authenticate = false,
                    Message = "Failed"
                };

            }
            //var data = _mapper.Map<List<LoginModel>, List<LoginDTO>>(loginModel);
            //return data;
            //return "";

          
        }

        private string CreateToken(LoginDTO user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.EmailId),
                new Claim(ClaimTypes.Role, "Admin")
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
