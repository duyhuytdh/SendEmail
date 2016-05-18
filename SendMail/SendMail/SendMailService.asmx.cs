using SendMail.Models;
using SendMail.ServiceMail;
using SendMail.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SendMail
{
    /// <summary>
    /// Summary description for SendMailService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SendMailService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void SendEmailSchedule()
        {
            try
            {
                using (SendMailEntities db = new SendMailEntities())
                {
                    List<TempScheduleSendEmail> list_temp = new List<TempScheduleSendEmail>();
                    EmailSend email = new EmailSend();
                    TempScheduleSendEmail latest = db.TempScheduleSendEmails.OrderBy(m => m.TimeSchedule).FirstOrDefault();
                    list_temp = db.TempScheduleSendEmails.Where(x => x.TimeSchedule == latest.TimeSchedule).ToList();
                    Int64 idEmailOwn = Int64.Parse(list_temp[0].IDEmailOwn.ToString());
                    EmailOwn emailOwn = db.EmailOwns.FirstOrDefault(x => x.ID == idEmailOwn);
                    foreach (var item in list_temp)
                    {
                        email.toEmail = item.Email;
                        email.subject = item.Subject;
                        email.body = item.ContentEmail;

                        STPMService.SendMail(emailOwn.Email
                               , Cryption.Decrypt(emailOwn.Password)
                               , email.toEmail
                               , email.subject
                               , email.body);
                    }

                    db.TempScheduleSendEmails.RemoveRange(list_temp);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
         
        }

    }
}
