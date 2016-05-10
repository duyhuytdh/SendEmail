//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SendMail.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SendMail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SendMail()
        {
            this.Campaigns = new HashSet<Campaign>();
        }
    
        public long SendMailID { get; set; }
        public long UserID { get; set; }
        public long ContactID { get; set; }
        public System.DateTime TimeSend { get; set; }
        public bool StatusSend { get; set; }
        public string EmailUsed { get; set; }
        public string TypeServiceUsed { get; set; }
        public string Note { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual User User { get; set; }
    }
}