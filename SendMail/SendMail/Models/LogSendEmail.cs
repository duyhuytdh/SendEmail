//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SendMail.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LogSendEmail
    {
        public long SendMailID { get; set; }
        public long UserID { get; set; }
        public long ContactID { get; set; }
        public System.DateTime TimeSend { get; set; }
        public bool StatusSend { get; set; }
        public long IDEmailOwn { get; set; }
        public string TypeServiceUsed { get; set; }
        public string Note { get; set; }
        public Nullable<long> CampaignID { get; set; }
        public Nullable<long> EmailID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    
        public virtual Contact Contact { get; set; }
        public virtual User User { get; set; }
    }
}
