using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1.Services
{
    internal interface IEmailService
    {
        // Represents method to send email
        Task SendEmailAsync(string email, string subject, string msg);

    }
}
