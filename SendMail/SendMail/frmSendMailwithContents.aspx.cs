using DevExpress.Web;
using Quartz;
using Quartz.Impl;
using SendMail.Models;
using SendMail.ServiceMail;
using SendMail.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Web.Security;
using System.Linq;
using SendMail.Business;

namespace SendMail
{
    public partial class frmSendMailwithContents : System.Web.UI.Page
    {
        #region Data Member
        CreateMail createEmail;
        #endregion

        #region Data structure
        private static class DataField
        {
            public const string STT = "STT";
            public const string Email = "Email";
            public const string Subject = "Subject";
            public const string Content = "ContentEmail";
        }
        #endregion

        #region Private Method
        private void saveTempScheduleSendEmail()
        {
            ListEditItem cmbEmailOwnselectedItem = cmbEmailOwn.SelectedItem;
            ListEditItem cmbcmbCampaignselectedItem = cmbCampaign.SelectedItem;
            for (int i = 0; i < gridView.VisibleRowCount; i++)
            {
                if (gridView.GetRowLevel(i) == gridView.GroupCount)
                {
                    object keyValue = gridView.GetRowValues(i, new string[] { DataField.STT, DataField.Email, DataField.Subject, DataField.Content });
                    if (keyValue != null)
                    {
                        using (SendMailEntities db = new SendMailEntities())
                        {
                            Array arr = (Array)keyValue;
                            TempScheduleSendEmail temp = new TempScheduleSendEmail();
                            temp.STT = Int64.Parse(arr.GetValue(0).ToString());
                            temp.Email = arr.GetValue(1).ToString();
                            temp.Subject = arr.GetValue(2).ToString();
                            temp.ContentEmail = arr.GetValue(3).ToString();
                            temp.IDEmailOwn = Int64.Parse(cmbEmailOwnselectedItem.GetValue("ID").ToString());
                            if (cmbCampaign.SelectedIndex > 0)
                            {
                                temp.IDCampaign = Int64.Parse(cmbcmbCampaignselectedItem.GetValue("CampaignID").ToString());
                            }
                            temp.TimeSchedule = DateTime.Parse(Request.Form[txt_date_schedule.UniqueID]);
                            db.TempScheduleSendEmails.Add(temp);
                            db.SaveChanges();
                        }
                    }

                }
            }
        }

        #endregion

        #region Public Method
        #endregion

        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            createEmail = new CreateMail();
            GoogleMailService.initService();
            radio_service_stpm.Checked = true;

            cmbCampaign.DataBind();
            cmbCampaign.Items.Insert(0, new ListEditItem("None"));


            cmbEmailOwn.DataBind();

            if (!IsPostBack && !IsCallback)
            {
                cmbCampaign.SelectedIndex = 0;
                cmbEmailOwn.SelectedIndex = 1;
            }
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    txtNameFileUpload.Text = FileName;
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    FileUpload1.SaveAs(FilePath);

                }
                else
                {
                    String message = "Bạn chưa chọn file hoặc file này đang được mở!";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
                }
            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
                Debugger.Log(1, "Send Mail", "Failed: " + v_e);
            }
        }

        protected void btnImportList_Click(object sender, EventArgs e)
        {
            try
            {
                ListEditItem cmbEmailOwnselectedItem = cmbEmailOwn.SelectedItem;
                ListEditItem cmbcmbCampaignselectedItem = cmbCampaign.SelectedItem;
                SendMailEntities db = new SendMailEntities();
                List<TempSendEmail> listTemp = new List<TempSendEmail>();
                List<Contact> lst_contact = new List<Contact>();

                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FileName = txtNameFileUpload.Text;
                string Extension = Path.GetExtension(FileName);
                string FilePath = Server.MapPath(FolderPath + FileName);
                if (FileName != "")
                {
                    //delete data in table temporary
                    db.Database.ExecuteSqlCommand("Delete from TempSendEmails where IDUser = " + mGlobal.UserID);
                    DataTable dt = ImportExcel.ImportExcel2DataTable(FilePath, Extension);
                    foreach (DataRow dr in dt.Rows)
                    {
                        TempSendEmail temp = new TempSendEmail();
                        temp.STT = Int64.Parse(dr["STT"].ToString());
                        temp.Subject = dr["Subject"].ToString();
                        temp.ContentEmail = dr["Content"].ToString();
                        temp.Email = dr["Email"].ToString();
                        temp.IDUser = mGlobal.UserID;
                        temp.TimeSend = DateTime.Now;
                        temp.IDEmailOwn = Int64.Parse(cmbEmailOwnselectedItem.GetValue("ID").ToString());
                         if (cmbCampaign.SelectedIndex > 0)
                         {
                                temp.IDCampaign = Int64.Parse(cmbcmbCampaignselectedItem.GetValue("CampaignID").ToString());
                          }
                        listTemp.Add(temp);
                        //check contact, if is not exist then save to db
                        if (!ContactBusiness.checkContactIsExist(temp.Email))
                        {
                            Contact contact = new Contact();
                            contact.Email = temp.Email;
                            lst_contact.Add(contact);
                        }
                    }

                    db.TempSendEmails.AddRange(listTemp);
                    if (lst_contact.Count > 0)
                    {
                        db.Contacts.AddRange(lst_contact);
                    }
                    db.SaveChanges();
                    gridView.DataBind();
                }
                else
                {
                    String message = "Bạn chưa chọn file hoặc file này đang được mở!";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
                }
            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
                //Debugger.Log(1, "Send Mail", "Failed: " + v_e);
            }
        }

        protected void btnSendListMail_Click(object sender, EventArgs e)
        {
            try
            {
                SendMailEntities db = new SendMailEntities();
                List<EmailContent> lst_emailContent = new List<EmailContent>();
                List<LogSendEmail> lst_logEndEmail = new List<LogSendEmail>();
                List<TempSendEmail> lst_TempSendEmail = new List<TempSendEmail>();
                ListEditItem selectedItem = cmbEmailOwn.SelectedItem;
                EmailOwn emailOwn = db.EmailOwns.FirstOrDefault(x => x.ID == Int64.Parse(selectedItem.GetValue("ID").ToString()));
                lst_TempSendEmail = db.TempSendEmails.Where(x=>x.IDUser == mGlobal.UserID).ToList();
                
               
                //email.fromEmail = cmbEmailOwn.Text;
                //email.passWordSendMail = Cryption.Decrypt(selectedItem.GetValue("Password").ToString());

                if (radio_service_google.Checked)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Hiện chưa hỗ trợ service này!" + "');", true);
                }
                else if (radio_service_stpm.Checked)
                {
                    foreach (TempSendEmail item in lst_TempSendEmail)
                    {

                        EmailContent emailContent = new EmailContent();

                        emailContent.Subject = item.Subject;
                        emailContent.ContentEmail = item.ContentEmail;
                        lst_emailContent.Add(emailContent);

                        //get contact
                        Contact contact = db.Contacts.FirstOrDefault(x => x.Email == item.Email);

                        //get UserID

                        //User user = db.Users.FirstOrDefault(x => x.AccountName == userName);

                        //save log send email
                            LogSendEmail log = new LogSendEmail();
                            if (item.IDCampaign != null)
                            {
                                log.CampaignID = item.IDCampaign;
                            }

                            log.ContactID = contact.ContactID;
                            log.EmailID = emailContent.EmailID;
                            log.StatusSend = true;
                            log.IDEmailOwn = Int64.Parse(item.IDEmailOwn.ToString());
                            log.TypeServiceUsed = mGlobal.STPM;
                            log.UserID = mGlobal.UserID;

                            lst_logEndEmail.Add(log);


                            STPMService.SendMail(emailOwn.Email
                               , Cryption.Decrypt(emailOwn.Password)
                               , item.Email
                               , item.Subject
                               , item.ContentEmail);
                        
                    }

                    db.EmailContents.AddRange(lst_emailContent);
                    db.LogSendEmails.AddRange(lst_logEndEmail);
                    db.SaveChanges();
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Gửi thành công!" + "');", true);
                }
            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
                Debugger.Log(1, "Send Mail", "Failed: " + v_e);
            }
        }

        protected void btnSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                saveTempScheduleSendEmail();
                DateTime date = DateTime.Parse(Request.Form[txt_date_schedule.UniqueID]);

                // Grab the Scheduler instance from the Factory 
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                // and start it off
                scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<SendEmailJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                // trigger builder creates simple trigger by default, actually an ITrigger is returned
                ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartAt(date) // some Date 
                .ForJob("job1", "group1") // identify job with name, group strings
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

        protected void CheckedChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
                Debugger.Log(1, "Send Mail", "Failed: " + v_e);
            }
        }

        public class SendEmailJob : IJob
        {
            public void Execute(IJobExecutionContext context)
            {
                SendMailService proxy = new SendMailService();
                proxy.SendEmailSchedule();
            }
        }

        #endregion

        protected void cmbEmailOwn_ValueChanged(object sender, EventArgs e)
        {

        }

        protected void gridView_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            try
            {
                using (SendMailEntities db = new SendMailEntities())
                {
                    if (ContactBusiness.checkContactIsExist(e.NewValues["Email"].ToString().Trim()))
                    {
                        Contact contact = new Contact();
                        contact.Email = e.NewValues["Email"].ToString().Trim();
                        db.Contacts.Add(contact);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}