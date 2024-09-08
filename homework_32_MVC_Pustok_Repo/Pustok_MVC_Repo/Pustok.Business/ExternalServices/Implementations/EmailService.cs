using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Pustok.Business.ExternalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.ExternalServices.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;


        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMailAsync(string to, string subject, string name, string text)
        {
            string mail = _configuration.GetSection("EmailService:Mail").Value;
            string password = _configuration.GetSection("EmailService:Password").Value;
            string bodyOfMail = GetBody(name, text);

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password)
            };

            await client.SendMailAsync(new MailMessage(mail, to, subject, bodyOfMail) { IsBodyHtml = true });
        }


        private string GetBody(string name, string text)
        {

            string body = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            color: #333;
            margin: 20px;
        }}
        .header {{
            background-color: #f4f4f4;
            padding: 10px;
            text-align: center;
            border-bottom: 2px solid #ccc;
        }}
        .content {{
            padding: 20px;
            background-color: #ffffff;
            border: 1px solid #ddd;
            border-radius: 4px;
        }}
        .footer {{
            background-color: #f4f4f4;
            padding: 10px;
            text-align: center;
            border-top: 2px solid #ccc;
            margin-top: 20px;
        }}
        a {{
            color: #1a73e8;
            text-decoration: none;
        }}
        a:hover {{
            text-decoration: underline;
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h1>Welcome to Our Service!</h1>
    </div>
    <div class='content'>
        <h2>Hello {name},</h2>
        <p>{text}</p>
    </div>
    <div class='footer'>
        <p>&copy; 2024 Your Company Name</p>
    </div>
</body>
</html>
";
            return body;
        }
    }
}
