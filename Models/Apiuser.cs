using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Apiuser
    {
    
            [Required(ErrorMessage = "Name is required")]
            public string Name { get; set; }

            [Required, EmailAddress(ErrorMessage = "Invalid Email")]
            public string Email { get; set; }

            [Required, Phone(ErrorMessage = "Invalid Mobile Number")]
            public string Mobile { get; set; }

            [Required, DataType(DataType.Date)]
            public DateTime Dob { get; set; }

            [Required(ErrorMessage = "File is required")]
            public IFormFile FileUpload { get; set; }
        
    }

}

