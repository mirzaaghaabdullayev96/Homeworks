﻿using MVC_Pronia_Template.Models.Base;

namespace MVC_Pronia_Template.Models
{
    public class Slide :BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Order { get; set; }
    }
}
