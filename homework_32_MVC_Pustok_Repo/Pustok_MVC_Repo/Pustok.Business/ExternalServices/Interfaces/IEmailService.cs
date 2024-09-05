using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.ExternalServices.Interfaces
{
    public interface IEmailService
    {
        Task SendMailAsync(string to, string subject, string name, string text);
    }
}
