using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SendMail.ServiceMail
{
    public class CreateMail
    {

        public string fromMail;
        public string toMail;
        public string subject;
        public string body;

        public Message createMessage(string subject
                                            , string body
                                            , string fromMail
                                            , string toMail)
        {
            var msg = new AE.Net.Mail.MailMessage
            {
                Subject = subject,
                Body = body,
                From = new MailAddress(fromMail)
            };
            msg.To.Add(new MailAddress(toMail));

            msg.ReplyTo.Add(msg.From); // Bounces without this!!
            var msgStr = new StringWriter();
            msg.Save(msgStr);
            return new Message { Raw = Base64UrlEncode(msgStr.ToString()) };
        }

        private string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            // Special "url-safe" base64 encode.
            return Convert.ToBase64String(inputBytes)
              .Replace('+', '-')
              .Replace('/', '_')
              .Replace("=", "");
        }
    }
}