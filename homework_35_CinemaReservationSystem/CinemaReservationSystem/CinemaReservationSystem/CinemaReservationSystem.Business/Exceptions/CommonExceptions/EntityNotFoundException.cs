using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.Exceptions.CommonExceptions
{
    public class EntityNotFoundException : Exception
    {
        public int StatusCode { get; set; }
        public string PropertyName { get; set; }
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string? message) : base(message)
        {
        }
        public EntityNotFoundException(int statusCode, string propertyName, string? message) : base(message)
        {
            StatusCode = statusCode;
            PropertyName = propertyName;
        }
    }
}
