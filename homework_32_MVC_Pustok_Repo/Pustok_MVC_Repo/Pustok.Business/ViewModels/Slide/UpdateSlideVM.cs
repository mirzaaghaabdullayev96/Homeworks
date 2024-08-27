using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.ViewModels
{
    public class UpdateSlideVM
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public IFormFile? SlidePhoto { get; set; }
        public string ExistedSlidePhoto { get; set; }
    }
}
