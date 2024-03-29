﻿using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public int QuantityInCart { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
