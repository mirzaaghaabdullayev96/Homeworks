using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Exceptions.AuthorExceptions
{
    public class AuthorFullNameException : Exception
    {
        public string PropertyName {  get; set; }
        public AuthorFullNameException()
        {
        }

        public AuthorFullNameException(string? message) : base(message)
        {
        }

        public AuthorFullNameException(string propertyName,string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
