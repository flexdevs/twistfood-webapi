﻿using OnlineMarket.Domain.Common;

namespace TwistFood.Api.Models;

public class Product : BaseEntity
{
    public int CategoryId { get; set; } 
    public virtual Category Category { get; set; } = default!;
    public string ProductName { get; set; } = string.Empty; 
    public string ProductDescription { get; set; }= string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public double Price { get; set; }   

}
