using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.Exceptions.CommonExceptions
{
    public class AlreadyExistsException : Exception
    {
        public int StatusCode { get; set; }
        public string PropertyName { get; set; }
        public AlreadyExistsException()
        {
        }

        public AlreadyExistsException(string? message) : base(message)
        {
        }
        public AlreadyExistsException(int statusCode, string propertyName, string? message) : base(message)
        {
            StatusCode = statusCode;
            PropertyName = propertyName;
        }
    }
}
