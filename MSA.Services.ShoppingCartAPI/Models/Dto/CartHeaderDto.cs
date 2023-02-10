﻿using System.ComponentModel.DataAnnotations;

namespace MSA.Services.ShoppingCartAPI.Models.Dto
{
    public class CartHeaderDto
    {
        [Key]
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }        
    }
}
