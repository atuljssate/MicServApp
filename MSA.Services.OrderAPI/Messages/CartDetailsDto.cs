﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MSA.Services.OrderAPI.Messages
{
    public class CartDetailsDto
    {
        public int CartDetailsId { get; set; }
        
        public int CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]

        public virtual ProductDto Product { get; set; }
        
        public int Count { get; set; }
    }
}
