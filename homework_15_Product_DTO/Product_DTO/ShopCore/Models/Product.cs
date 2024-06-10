﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCore.Models
{
    public class Product
    {
        private static int _counter;
        public int Id { get; }
        public string Name { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public Product(string name, double costPrice, double salePrice)
        {
            Name = name;
            CostPrice = costPrice;
            SalePrice = salePrice;
            Id = ++_counter;
        }
    }
}
