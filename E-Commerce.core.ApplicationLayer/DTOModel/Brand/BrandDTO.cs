using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.ApplicationLayer.DTOModel.Brand
{
    public class BrandDTO:ApiResponse
    {
        public string BrandName { get; set; }
        public string LogoPath { get; set; }
        //public IFormFile? BrandLogo { get; set; }
    }
}
