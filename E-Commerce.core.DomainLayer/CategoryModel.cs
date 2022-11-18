using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.DomainLayer
{
    public class CategoryModel
    {
        public int Id { get; set; } = 0;
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        public string Status { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }

    }
}
