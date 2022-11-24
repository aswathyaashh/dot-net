using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.core.DomainLayer.Entities
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; } = 0;
        [StringLength(30, MinimumLength = 3)]
        public string CategoryName { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public CategoryModel()
        {
            CreatedDate = DateTime.UtcNow;
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
