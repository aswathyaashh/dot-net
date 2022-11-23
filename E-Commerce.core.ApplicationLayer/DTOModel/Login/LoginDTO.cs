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
        [EmailAddress(ErrorMessage = "*EmailId should be in the format adc@gmail.com")]
        public string EmailId { get; set; }
        public string password { get; set; }
    }
}
