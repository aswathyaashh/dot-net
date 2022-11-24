using E_Commerce.core.ApplicationLayer.DTOModel.Login;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.ApplicationLayer.Examples
{
    public class LoginResponseDTOExample : IExamplesProvider<LoginResponseDTO>
    {







        public LoginResponseDTO GetExamples()
        {
            return new LoginResponseDTO()
            {
                Token = "QwesfyWRtydwer3yu",
                Authenticate = true,
                ExpiryDate = DateTime.Now,
                Message = "Success"
            };
        }

    }
}
