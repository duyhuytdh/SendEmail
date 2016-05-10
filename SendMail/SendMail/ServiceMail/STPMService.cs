using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using SendMail;
using SendMail.ServiceMail;
using System.Net;

namespace SendMail.ServiceMail
{
    public static class STPMService
    {
        public static void SendMail(string fromMail
                                    , string password 
                                    , string toMail
                                    , string subject
                                    , string body)
        {

            var fromAddress = new MailAddress(fromMail, "From Mobilink");
            var toAddress = new MailAddress(toMail, "To Name");

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, password)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}