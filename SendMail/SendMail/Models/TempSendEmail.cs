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
    
    public partial class TempSendEmail
    {
        public long ID { get; set; }
        public Nullable<long> STT { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string ContentEmail { get; set; }
        public Nullable<long> IDEmailOwn { get; set; }
        public Nullable<System.DateTime> TimeSend { get; set; }
        public Nullable<long> IDCampaign { get; set; }
        public Nullable<long> IDUser { get; set; }
    }
}
