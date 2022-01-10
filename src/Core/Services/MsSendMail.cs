using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace Core.Services
{

    public class MsSendMail : IMsSendMail
    {
        private readonly AppSettings _appSettings;
        private readonly EnvironmentVariables _environmentVariables;

        public MsSendMail(IOptions<AppSettings> appSettings, EnvironmentVariables environmentVariables)
        {
            _appSettings = appSettings.Value;
            _environmentVariables = environmentVariables;
        }

        public async Task<bool> SendMailAsync(
            string to,
            string subject,
            string message,
            string entity,
            string entityId,
            int priority,
            byte[]? file = null,
            string? fileMimetype = null,
            string? fileName = null
        )
        {

            string smtpMail = _appSettings.SmtpMailAccount;
            string smtpPassword = _appSettings.SmtpMailPassword;
            string smtpDisplayName = _appSettings.SmtpMailDisplayName;
            string smtpHost = _appSettings.SmtpMailHost;
            int smtpPort = Convert.ToInt32(_appSettings.SmtpMailPort);

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(smtpMail, smtpDisplayName)
            };

            IEnumerable<string> emails = to.Split(",");
            foreach (var item in emails) mail.To.Add(new MailAddress(item));
            //mail.To.Add(new MailAddress(to));

            if (!String.IsNullOrEmpty(fileMimetype) && !String.IsNullOrEmpty(fileName))
            {
                //save the data to a memory stream
                System.IO.MemoryStream ms = new System.IO.MemoryStream(file);

                //create the attachment from a stream. Be sure to name the data with a file and
                //media type that is respective of the data
                mail.Attachments.Add(new System.Net.Mail.Attachment(ms, fileName, fileMimetype));
            }

            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(smtpHost, smtpPort))
            {
                smtp.Credentials = new NetworkCredential(smtpMail, smtpPassword);
                smtp.EnableSsl = false;
                await smtp.SendMailAsync(mail);
            }

            return true;

        }

    }

    public class MailRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public int Priority { get; set; }
    }
}