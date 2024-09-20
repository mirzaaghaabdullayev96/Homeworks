using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.Exceptions.CommonExceptions
{
    public class IdIsNotValidException : Exception
    {
        public string PropertyName { get; set; }
        public int StatusCode { get; set; }
        public IdIsNotValidException()
        {
        }

        public IdIsNotValidException(string? message) : base(message)
        {
        }

        public IdIsNotValidException(int statusCode, string propertyName, string? message) : base(message)
        {
            StatusCode = statusCode;
            PropertyName = propertyName;
        }
    }
}
