using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Exceptions.CommonExceptions
{
    public class FileValidationException: Exception
    {
        public string PropertyName { get; set; }
        public FileValidationException()
        {
        }

        public FileValidationException(string? message) : base(message)
        {
        }
        public FileValidationException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
