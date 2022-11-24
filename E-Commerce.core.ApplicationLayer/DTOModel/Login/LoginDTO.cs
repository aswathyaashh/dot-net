using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.ApplicationLayer.DTOModel.Login
{
    public class LoginDTO
    {
        //[Required]
        //[EmailAddress(ErrorMessage = "*EmailId should be in the format adc@gmail.com")]
        public string GetString([Required, MaxLength(20)] string EmailId) =>$"Enter email, {EmailId}.";
        public string EmailId { get; set; }

        public string Password { get; set; }
    }
}
