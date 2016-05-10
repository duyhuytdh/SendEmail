using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendMail.Models
{
    public class EmailSend
    {
        public string fromEmail { get; set; }
        public string toEmail { get; set; }
        public string passWordSendMail { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
}