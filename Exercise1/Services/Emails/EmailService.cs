using Exercise1.Databases;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private string _smtpSettings = "SmtpSettings";

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Represents method to send email
        public Task SendEmailAsync(string email, string subject, string msg)
        {
            //Assumps that we have settings of email service to send notification
            var smtpSettings = _configuration.GetSection(_smtpSettings);
            Console.WriteLine($"Sending email to: {email}\nSubject: {subject}\nBody: {msg}");
            return Task.CompletedTask;
        }
    }
}
