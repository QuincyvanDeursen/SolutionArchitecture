using System.ComponentModel.DataAnnotations;

namespace InventoryService.Dto
{
    public class ProductUpdateDto
    {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public decimal? Price { get; set; }
            [Required]
            public int Quantity { get; set; }
    }
}
