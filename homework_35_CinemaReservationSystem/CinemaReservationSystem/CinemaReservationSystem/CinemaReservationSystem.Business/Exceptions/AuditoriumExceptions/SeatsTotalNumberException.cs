using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.Exceptions.AuditoriumExceptions
{
    public class SeatsTotalNumberException : Exception
    {
        public int StatusCode { get; set; }
        public string PropertyName { get; set; }
        public SeatsTotalNumberException()
        {
        }

        public SeatsTotalNumberException(string? message) : base(message)
        {
        }
        public SeatsTotalNumberException(int statusCode, string propertyName, string? message) : base(message)
        {
            StatusCode = statusCode;
            PropertyName = propertyName;
        }
    }
}
