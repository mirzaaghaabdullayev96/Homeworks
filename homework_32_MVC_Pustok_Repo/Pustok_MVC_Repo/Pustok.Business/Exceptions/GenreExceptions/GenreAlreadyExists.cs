using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Exceptions.GenreExceptions
{
    public class GenreAlreadyExists : Exception
    {
        public string PropertyName { get; set; }
        public GenreAlreadyExists()
        {
        }

        public GenreAlreadyExists(string? message) : base(message)
        {
        }
        public GenreAlreadyExists(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }

       
    }
}
