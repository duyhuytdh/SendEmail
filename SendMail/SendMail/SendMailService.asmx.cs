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
                    List<LogSendEmail> lst_logEndEmail = new List<LogSendEmail>();
                    
                    TempScheduleSendEmail latest = db.TempScheduleSendEmails.OrderBy(m => m.TimeSchedule).FirstOrDefault();
                    list_temp = db.TempScheduleSendEmails.Where(x => x.TimeSchedule == latest.TimeSchedule).ToList();
                    Int64 idEmailOwn = Int64.Parse(list_temp[0].IDEmailOwn.ToString());
                    EmailOwn emailOwn = db.EmailOwns.FirstOrDefault(x => x.ID == idEmailOwn);
                    foreach (var item in list_temp)
                    {

                        //get contact
                        Contact contact = db.Contacts.FirstOrDefault(x => x.Email == item.Email);

                        //save log send email
                        LogSendEmail log = new LogSendEmail();
                        if (item.IDCampaign != null)
                        {
                            log.CampaignID = item.IDCampaign;
                        }

                        log.ContactID = contact.ContactID;
                        log.StatusSend = true;
                        log.IDEmailOwn = emailOwn.ID;
                        log.TypeServiceUsed = "STPM";
                        log.UserID = mGlobal.UserID;
                        log.Subject = item.Subject;
                        log.Body = item.ContentEmail;

                        lst_logEndEmail.Add(log);

                        STPMService.SendMail(emailOwn.Email
                               , Cryption.Decrypt(emailOwn.Password)
                               , item.Email
                               , item.Subject
                               , item.ContentEmail);
                    }
                    //db.EmailContents.AddRange(lst_emailContent);
                    db.LogSendEmails.AddRange(lst_logEndEmail);
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
