using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.ApplicationLayer.DTOModel.Login
{
    public class LoginResponseDTO 
    {
        public string Token { get; set; }
   
        public bool Authenticate { get; set; }
        
        public DateTime ExpiryDate { get; set; }
        public  string Message { get; set; }
       

    }
}
