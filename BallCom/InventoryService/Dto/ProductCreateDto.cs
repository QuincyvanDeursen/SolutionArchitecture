using System.ComponentModel.DataAnnotations;

namespace InventoryService.Dto
{
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
