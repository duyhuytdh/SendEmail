using SendMail.Models;
using SendMail.ServiceMail;
using SendMail.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Quartz;
using Quartz.Impl;

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
        #region Data Member
        public static IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
        public static List<JobKey> lstJobkey = new List<JobKey>();
        #endregion

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void SendEmailSchedule(string jobName, string groupJob, string triggerName, string triggerGroup, DateTime timeSchelue)
        {
            try
            {
                //DateTime date = DateTime.Parse(Request.Form[txt_date_schedule.UniqueID]);
                JobKey jobkey = new JobKey(jobName, groupJob);
                lstJobkey.Add(jobkey);
                DateTime date = timeSchelue;

                // Grab the Scheduler instance from the Factory 
                scheduler = StdSchedulerFactory.GetDefaultScheduler();

                // and start it off
                scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<SendEmailJob>()
                    .WithIdentity(jobName, groupJob)
                    .Build();

                // trigger builder creates simple trigger by default, actually an ITrigger is returned

                ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
                .WithIdentity(triggerName, triggerGroup)
                .StartAt(date) // some Date 
                .ForJob(jobName, groupJob) // identify job with name, group strings
                .Build();

                // Tell quartz to schedule the job using our trigger
                scheduler.ScheduleJob(job, trigger);

                // and last shut down the scheduler when you are ready to close your program
                //scheduler.Shutdown();
            }
            catch (Exception)
            {
                
                throw;
            }
         
        }

        [WebMethod]
        public void stopSchedule(string jobName, string groupName)
        {
            try
            {
                foreach (var item in lstJobkey)
                {
                    if (item.Name == jobName && item.Group == groupName)
                    {
                        scheduler.DeleteJob(item);
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        #region private method
        private void setupSchedule(string jobName, string groupJob, string triggerName, string triggerGroup,DateTime timeSchelue)
        {
            //DateTime date = DateTime.Parse(Request.Form[txt_date_schedule.UniqueID]);
            JobKey jobkey = new JobKey(jobName,groupJob);
            mSchedule.lstJobkey.Add(jobkey);
            DateTime date = timeSchelue;

            // Grab the Scheduler instance from the Factory 
            //mSchedule.scheduler = StdSchedulerFactory.GetDefaultScheduler();

            // and start it off
            mSchedule.scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<SendEmailJob>()
                .WithIdentity(jobName, groupJob)
                .Build();

            // trigger builder creates simple trigger by default, actually an ITrigger is returned

            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
            .WithIdentity(triggerName, triggerGroup)
            .StartAt(date) // some Date 
            .ForJob(jobName, groupJob) // identify job with name, group strings
            .Build();

            // Tell quartz to schedule the job using our trigger
            mSchedule.scheduler.ScheduleJob(job, trigger);

            // and last shut down the scheduler when you are ready to close your program
            //scheduler.Shutdown();
        }

        private static void sendEmailSchedule()
        {
            using (SendMailEntities db = new SendMailEntities())
            {
                List<TempScheduleSendEmail> list_temp = new List<TempScheduleSendEmail>();
                List<LogSendEmail> lst_logEndEmail = new List<LogSendEmail>();

                TempScheduleSendEmail latest = db.TempScheduleSendEmails.Where(x => x.IDUser == mGlobal.UserID).ToList().OrderBy(m => m.TimeSchedule).FirstOrDefault();
                list_temp = db.TempScheduleSendEmails.Where(x => x.TimeSchedule == latest.TimeSchedule && x.IDUser == mGlobal.UserID).ToList();
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
                    log.TimeSend = (DateTime)item.TimeSchedule;

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

        #endregion

        #region Ijob
        public class SendEmailJob : IJob
        {
            public void Execute(IJobExecutionContext context)
            {
                sendEmailSchedule();
            }
        }
        #endregion

    }
}
