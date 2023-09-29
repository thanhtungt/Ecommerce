﻿namespace Presentation.Models
{
    public class OrderProductResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ProductImagePath { get; set; }
    }
}
