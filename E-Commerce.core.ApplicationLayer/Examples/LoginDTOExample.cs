using E_Commerce.core.ApplicationLayer.DTOModel.Login;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.ApplicationLayer.Examples
{
    public class LoginDTOExample : IExamplesProvider<LoginDTO>
    {
        public LoginDTO GetExamples()
        {
            return new LoginDTO()
            {
                EmailId="email@gmail.com",
                Password="dfghjdds"
            };
        }

    }
}
