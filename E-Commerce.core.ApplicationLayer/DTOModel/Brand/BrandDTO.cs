using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.ApplicationLayer.DTOModel.Brand
{
    public class BrandDTO
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string LogoPath { get; set; }
        public IFormFile Logo { get; set; }

    }
}
