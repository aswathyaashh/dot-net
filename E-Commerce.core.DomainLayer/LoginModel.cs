using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace E_Commerce.core.DomainLayer
{
    public class LoginModel
    {
        [Key]       
        [EmailAddress(ErrorMessage = "*EmailId should be in the format adc@gmail.com")]
        public string EmailId { get; set; }
        
        public string password { get; set; }
        //public int id { get; set; }

        
        public DateTime? createdDate { get; set; }
       
        public DateTime? modifiedDate { get; set; }
        public int Status { get; set; }
        public LoginModel()
        {
            this.createdDate = DateTime.UtcNow;
            this.modifiedDate = DateTime.UtcNow;
        }

    }
}