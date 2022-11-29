using E_Commerce.core.ApplicationLayer.DTOModel.Generic_Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.ApplicationLayer.DTOModel
{
    public class CategoryDTO:ApiResponse
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
