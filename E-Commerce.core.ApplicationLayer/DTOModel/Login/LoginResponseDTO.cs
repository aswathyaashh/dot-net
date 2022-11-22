using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.ApplicationLayer.DTOModel.Login
{
    public class LoginResponseDTO
    {
        public string IsTocken { get; set; }
        //public string IsAuthorized { get; set; }
        public bool IsAuthenticate { get; set; }
        public string IsSuccess { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public  string Message { get; set; }
       

    }
}
