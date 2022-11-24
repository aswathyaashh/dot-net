﻿using AutoMapper;
using E_Commerce.core.ApplicationLayer.DTOModel.Login;
using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.core.DomainLayer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.infrastructure.RepositoryLayer.services
{
    public class Login : ILogin
    {
        private readonly AdminDbContext _admincontext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public Login(AdminDbContext adminDbContext, IConfiguration iconfiguration, IMapper mapper)
        {
            _admincontext = adminDbContext;
            _configuration = iconfiguration;
            _mapper = mapper;
        }
        public LoginResponseDto LoginCheck(LoginDto login)
        {
            try
            {
                LoginDto loginModel = _mapper.Map<LoginModel, LoginDto>(_admincontext.Login.FirstOrDefault(i => i.EmailId == login.EmailId));

                if (loginModel.Password == login.Password)
                {
                    var check = _mapper.Map<LoginModel, LoginDto>(_admincontext.Login.FirstOrDefault(i => i.EmailId == login.EmailId));
                    var modifyDate = _mapper.Map<LoginDto, LoginModel>(check);
                    modifyDate.ModifiedDate = DateTime.Now;
                    _admincontext.SaveChanges();
                    return new LoginResponseDto()
                    {
                        Success = true,
                        Message = "Success",
                        ExpiryDate = DateTime.UtcNow.AddMinutes(60),
                        Token = CreateToken(login)
                    };
                }
                else
                {
                    return new LoginResponseDto()
                    {
                        Success = false,
                        Message = "Password is incorrect"
                    };
                }                
            }
            catch (Exception)
            {
                return new LoginResponseDto()
                {
                    Success = false,
                    Message = "Something went wrong"

                };
            }
        }
        
#region
        /// <summary>
        /// Token creation 
        /// Here we use email, role and expiryDate for generating token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string CreateToken(LoginDto user)
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
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds);
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
#endregion
    }
}

