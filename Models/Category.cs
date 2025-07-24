using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        [DisplayName("Catagory Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Display Order is required.")]
        [Range(1, 50, ErrorMessage = "Display order must be between 1 and 50.")]
        public int? DisplayOrder { get; set; }
    }
}
