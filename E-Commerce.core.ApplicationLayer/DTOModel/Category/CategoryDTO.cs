using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;

namespace E_Commerce.core.ApplicationLayer.DTOModel
{
    public class CategoryDTO:ApiResponse
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
