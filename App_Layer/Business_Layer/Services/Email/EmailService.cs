using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Xml.Linq;
using System.IO;

namespace Business_Layer.Services.Email
{
    public class EmailService
    {
        public static void SendEmail(string toEmail, string subject, string templatePath, Dictionary<string, string> placeholders)
        {
            try
            {
                var fromAddress = new MailAddress("email@gmail.com", "Music Player App");
                var toAddress = new MailAddress(toEmail);
                const string fromPassword = "your app password";

                string htmlTemplate = File.ReadAllText(templatePath); // Read Template
  
                foreach (var placeholder in placeholders) // Replace placeholders
                {
                    htmlTemplate = htmlTemplate.Replace(placeholder.Key, placeholder.Value);
                }

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = htmlTemplate,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
