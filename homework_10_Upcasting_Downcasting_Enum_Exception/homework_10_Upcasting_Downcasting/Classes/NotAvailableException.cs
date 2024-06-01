using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_10_Upcasting_Downcasting.Classes
{
    public class NotAvailableException : Exception
    {
        public NotAvailableException()
        {

        }

        public NotAvailableException(string? message) : base(message)
        {
        }
    }
}
