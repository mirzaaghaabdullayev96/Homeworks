﻿using MVC_Pronia_Template.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_Pronia_Template.Areas.ProniaAdmin.ViewModels
{
    public class UpdateProductVM
    {


        public IFormFile? MainPhoto { get; set; }
        public IFormFile? HoverPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        public List<int>? TagIds { get; set; } //choosing
        public List<int>? ImagesIds { get; set; }// choosing
        public List<int>? ColorIds { get; set; } //choosing
        public List<int>? SizeIds { get; set; } //choosing
        [Required]
        public decimal? Price { get; set; }
        public string SKU { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Tag>? Tags { get; set; }
        public List<ProductImage>? Images { get; set; }
        public List<Color>? Colors { get; set; }
        public List<Size>? Sizes { get; set; }
    }
}
