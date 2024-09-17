using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Core.Entities
{
    public class Comment : BaseEntity
    {
        public int MovieId { get; set; }
        public string AppUserId { get; set; }
        public string Content { get; set; }
        public Movie Movie { get; set; }
        public AppUser AppUser { get; set; }
    }
}
