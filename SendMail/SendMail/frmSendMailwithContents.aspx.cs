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
            ListEditItem selectedItem = cmbEmailOwn.SelectedItem;
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
                            temp.IDEmailOwn = Int64.Parse(selectedItem.GetValue("ID").ToString());
                            temp.TimeSchedule = DateTime.Parse(Request.Form[txt_date_schedule.UniqueID]);
                            db.TempScheduleSendEmails.Add(temp);
                            db.SaveChanges();
                        }
                    }

                }
            }
        }

        private void sendEmailSchedule()
        {
            SendMailEntities db = new SendMailEntities();
            List<TempScheduleSendEmail> list_temp = new List<TempScheduleSendEmail>();
            EmailSend email = new EmailSend();
            //list_temp = db.TempScheduleSendEmails.ToList();
            EmailOwn emailOwn = db.EmailOwns.FirstOrDefault(x => x.ID == list_temp[0].IDEmailOwn);
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
            cmbCampaign.SelectedIndex = 0;

            cmbEmailOwn.DataBind();
            cmbEmailOwn.SelectedIndex = 0;
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

                SendMailEntities db = new SendMailEntities();

                List<TempSendEmail> listTemp = new List<TempSendEmail>();

                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FileName = txtNameFileUpload.Text;
                string Extension = Path.GetExtension(FileName);
                string FilePath = Server.MapPath(FolderPath + FileName);
                if (FileName != "")
                {
                    //delete data in table temporary
                    db.Database.ExecuteSqlCommand("TRUNCATE TABLE TempSendEmails");
                    DataTable dt = ImportExcel.ImportExcel2DataTable(FilePath, Extension);
                    foreach (DataRow dr in dt.Rows)
                    {
                        TempSendEmail temp = new TempSendEmail();
                        temp.STT = Int64.Parse(dr["STT"].ToString());
                        temp.Subject = dr["Subject"].ToString();
                        temp.ContentEmail = dr["Content"].ToString();
                        temp.Email = dr["Email"].ToString();
                        listTemp.Add(temp);
                    }

                    db.TempSendEmails.AddRange(listTemp);
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
                Debugger.Log(1, "Send Mail", "Failed: " + v_e);
            }
        }

        protected void btnSendListMail_Click(object sender, EventArgs e)
        {
            try
            {
                EmailSend email = new EmailSend();
                ListEditItem selectedItem = cmbEmailOwn.SelectedItem;
                email.fromEmail = cmbEmailOwn.Text;
                email.passWordSendMail = Cryption.Decrypt(selectedItem.GetValue("Password").ToString());

                if (radio_service_google.Checked)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Hiện chưa hỗ trợ service này!" + "');", true);
                }
                else if (radio_service_stpm.Checked)
                {
                    for (int i = 0; i < gridView.VisibleRowCount; i++)
                    {
                        if (gridView.GetRowLevel(i) == gridView.GroupCount)
                        {
                            object keyValue = gridView.GetRowValues(i, new string[] { DataField.Email, DataField.Subject, DataField.Content });
                            if (keyValue != null)
                            {
                                Array arr = (Array)keyValue;
                                email.toEmail = arr.GetValue(0).ToString();
                                email.subject = arr.GetValue(1).ToString();
                                email.body = arr.GetValue(2).ToString();

                                STPMService.SendMail(email.fromEmail
                                       , email.passWordSendMail
                                       , email.toEmail
                                       , email.subject
                                       , email.body);
                            }

                        }
                    }

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
                frmSendMailwithContents frm = new frmSendMailwithContents();
                frm.sendEmailSchedule();
            }
        }

        #endregion
    }
}