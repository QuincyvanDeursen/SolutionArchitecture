﻿using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Inventory
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; } 
    }
}
