using System.ComponentModel.DataAnnotations;

namespace WEB.Models.DTOs
{
    public class ProductDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
