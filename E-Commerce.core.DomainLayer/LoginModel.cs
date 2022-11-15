using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace E_Commerce.core.DomainLayer
{
    public class LoginModel
    {
        [Key]
      
        [Required(ErrorMessage = "*EmailId is required")]
        [Display(Name = "EmailId")]
        [EmailAddress(ErrorMessage = "*EmailId should be in the format adc@gmail.com")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "*Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [RegularExpression("[^ ]{8,10}", ErrorMessage = "Password should contain a minimum of 8 characters and a capital letter")]
        public string password { get; set; }

        //[DefaultValue(value:DateTime.Now)]
        //[DefaultValue(DateTime.UtcNow)]
        //[DefaultValue(typeof(DateTime),DateTime.Now.ToString("yyyyy-MM-dd")]
        //public DateTime createdDate { get; set; }
       
        //public DateTime modifiedDate { get; set; }
        //public int Status { get; set; }

    }
}