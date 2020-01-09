using Mail.Data;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailReceiver
{
    class SendMail
    {
        public static void Send(Email email, string mailPersonale, string nomePersonale, string passwordPersonale)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(nomePersonale, mailPersonale));
            message.To.Add(new MailboxAddress(email.email));
            message.Subject = email.oggetto;

            message.Body = new TextPart("plain")
            {
                Text = email.testo
            };

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 465, true);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(mailPersonale, passwordPersonale);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
