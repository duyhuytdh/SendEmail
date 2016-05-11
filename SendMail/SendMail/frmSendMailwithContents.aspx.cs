using System;
using System.IO;
using System.Diagnostics;
using System.Configuration;

using SendMail.Models;
using SendMail.ServiceMail;
using SendMail.Util;
using System.Data;
using DevExpress.Web;
using SendMail.Models;
using System.Collections.Generic;
using Quartz;
using Quartz.Impl;


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
            public const string Email = "Email";
            public const string Subject = "Subject";
            public const string Content = "ContentEmail";
        }
        #endregion

        #region Private Method
        #endregion

        #region Public Method
        #endregion

        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            createEmail = new CreateMail();
            GoogleMailService.initService();
            radio_service_stpm.Checked = true;

            cmbCampaign.DataBind();
            cmbCampaign.Items.Insert(0,new ListEditItem("None"));
            cmbCampaign.SelectedIndex = 0;
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
                email.fromEmail = ip_txt_from_email.Value;
                email.passWordSendMail = ip_txt_pass_email.Value;

                if (radio_service_google.Checked)
                {

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
                DateTime date = DateTime.Parse(Request.Form[txt_date_schedule.UniqueID]);

                // Grab the Scheduler instance from the Factory 
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                // and start it off
                scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                // trigger builder creates simple trigger by default, actually an ITrigger is returned
                ISimpleTrigger trigger = (ISimpleTrigger) TriggerBuilder.Create()
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

        public class HelloJob : IJob
        {
            public void Execute(IJobExecutionContext context)
            {
                Console.WriteLine("Greetings from HelloJob!");               
            }
        }

        #endregion
    }
}