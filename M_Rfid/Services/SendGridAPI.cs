using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M_Rfid.Services
{
    public class SendGridAPI
    {
        public static async Task<bool> Execute(string userEmail, string userName, string plainTextContent,
             string htmlContent, string subject)
        {
            var apiKey = "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";

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com", "AboRam");
            var to = new EmailAddress(userEmail, userName);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return await Task.FromResult(true);
        }
    }
}
