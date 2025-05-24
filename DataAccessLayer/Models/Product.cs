﻿using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Part> Parts { get; set; } = new List<Part>();
    }


}
